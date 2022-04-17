using ChungKhoanASPDotNetCore.Applications.Interfaces;
using ChungKhoanASPDotNetCore.Constants;
using ChungKhoanASPDotNetCore.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ChungKhoanASPDotNetCore.Applications.Services
{
    public class BangDienService : IBangDienService
    {
        private readonly IConfiguration _configuration;
        static bool firsttime = true;
        static SqlDependency sqlDependency;
        static string commandStr = "SELECT [Id],[MaCK],[GiaMua1],[SoLuongMua1],[GiaMua2],[SoLuongMua2],[GiaMua3] ,[SoLuongMua3],[GiaKhop],[SoLuongKhop],[GiaBan1] ,[SoLuongBan1] ,[GiaBan2],[SoLuongBan2] ,[GiaBan3],[SoLuongBan3],[Tongso] FROM[dbo].[BangGiaTrucTuyen]";
        static SqlConnection conn;
        static SqlCommand command;
        string url = "";
        private IHubSender _hubSender;
        private IMemoryCache _cache;
        public BangDienService(IConfiguration configuration,
            IHubSender hubSender,
            IMemoryCache cache
            )
        {
            _configuration = configuration;
            _hubSender = hubSender;
            url = _configuration["ConnectionStrings:DefaultConnection"];
            _cache = cache;
            SqlDependency.Start(url);
        }

        public void init()
        {
            conn = new SqlConnection();
            command = new SqlCommand(commandStr, conn);
            sqlDependency = new SqlDependency(command);
            sqlDependency.OnChange += new OnChangeEventHandler(DependencyOnChange);
        }

        private void DependencyOnChange(object sender, SqlNotificationEventArgs e)
        {

            if (e.Type == SqlNotificationType.Change)
            {
                sqlDependency.OnChange -= DependencyOnChange;
                init();
                getDependency();
            }
        }
        public void getDependency()
        {
            if (firsttime)
                init();
            firsttime = false;
            List<BangGiaTrucTuyen> list = new List<BangGiaTrucTuyen>();

            if (conn != null && conn.State == ConnectionState.Open) conn.Close();
            try
            {
                conn.ConnectionString = url;
                conn.Open();
            }
            catch (Exception e)
            {
                return;
            }
            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                BangGiaTrucTuyen item = new BangGiaTrucTuyen();
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    var colName = sdr.GetName(i);
                    var colVal = sdr.GetValue(i);
                    var property = item.GetType().GetProperty(colName);
                    if (property != null && !(colVal is System.DBNull))
                    {
                        property.SetValue(item, colVal);
                    }
                }
                if (CheckObject(item))
                {
                    SendChange(item);
                }
            }
        }
        private bool CheckObject(BangGiaTrucTuyen vm)
        {
            List<BangGiaTrucTuyen> list;
            if (_cache.TryGetValue(BangDienConstant.BangDienCacheKey, out list))
            {
                foreach (var e in list)
                {
                    if (e.MaCK.Trim().Equals(vm.MaCK.Trim()))
                    {
                        if (e.GiaMua3 != vm.GiaMua3
                            || e.GiaMua2 != vm.GiaMua2
                            || e.GiaMua1 != vm.GiaMua1
                            || e.GiaBan1 != vm.GiaBan1
                            || e.GiaBan2 != vm.GiaBan2
                            || e.GiaBan3 != vm.GiaBan3
                            || e.GiaKhop != vm.GiaKhop
                            || e.SoLuongKhop != vm.SoLuongKhop
                            || e.SoLuongBan1 != vm.SoLuongBan1
                            || e.SoLuongBan2 != vm.SoLuongBan2
                            || e.SoLuongBan3 != vm.SoLuongBan3
                            || e.SoLuongMua1 != vm.SoLuongMua1
                            || e.SoLuongMua2 != vm.SoLuongMua2
                            || e.SoLuongMua3 != vm.SoLuongMua3)
                        {
                            e.GiaMua3 = vm.GiaMua3;
                            e.GiaMua2 = vm.GiaMua2;
                            e.GiaMua1 = vm.GiaMua1;
                            e.GiaBan1 = vm.GiaBan1;
                            e.GiaBan2 = vm.GiaBan2;
                            e.GiaBan3 = vm.GiaBan3;
                            e.GiaKhop = vm.GiaKhop;
                            e.SoLuongKhop = vm.SoLuongKhop;
                            e.SoLuongBan1 = vm.SoLuongBan1;
                            e.SoLuongBan2 = vm.SoLuongBan2;
                            e.SoLuongBan3 = vm.SoLuongBan3;
                            e.SoLuongMua1 = vm.SoLuongMua1;
                            e.SoLuongMua2 = vm.SoLuongMua2;
                            e.SoLuongMua3 = vm.SoLuongMua3;
                            return true;
                        }
                        else
                            return false;
                    }
                }
                list.Add(vm);
                _cache.Set(BangDienConstant.BangDienCacheKey, list);
                return false;
            }
            _cache.Set(BangDienConstant.BangDienCacheKey, new List<BangGiaTrucTuyen>() { vm });
            return false;
        }
        private void SendChange(BangGiaTrucTuyen item)
        {
            _hubSender.SendChange(item);
        }
        public List<BangGiaTrucTuyen> Get()
        {
            if (firsttime)
                getDependency();
            List<BangGiaTrucTuyen> list;
            if (_cache.TryGetValue(BangDienConstant.BangDienCacheKey, out list))
                return list;
            return null;
        }
    }
}
