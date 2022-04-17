using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ChungKhoanASPDotNetCore.Data;
using Microsoft.EntityFrameworkCore;
using ChungKhoanASPDotNetCore.Hubs;
using ChungKhoanASPDotNetCore.Applications.Interfaces;
using ChungKhoanASPDotNetCore.Applications.Services;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace ChungKhoanASPDotNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed(origin => true)
                   .AllowCredentials();
            }));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddMemoryCache();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "v1", Version = "v1" });
            });

            services.AddTransient<IBangDienService, BangDienService>();
            services.AddTransient<ILenhDatService, LenhDatService>();
            services.AddTransient<IHubSender, HubSender>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(routes =>
            {
                routes.MapHub<LiveInf>("hubs/signalr");
                routes.MapControllers();
            });

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                //var databaseName = Configuration["ConnectionStrings:DatabaseName"];
                //var enableBrokerScript = $"ALTER DATABASE {databaseName} SET ENABLE_BROKER";
                //context.Database.ExecuteSqlRaw(enableBrokerScript);

                var directoryInfo = new DirectoryInfo(".\\SqlScripts");

                if (directoryInfo.Exists)
                {
                    var histories = context.Histories;
                    var historyDic = histories?.ToDictionary(x => x.FileName, x => x.Script) ?? new Dictionary<string, string>();

                    foreach (var file in directoryInfo.GetFiles().OrderBy(f => f.FullName))
                    {
                        var script = File.ReadAllText(file.FullName);
                        if (historyDic.ContainsKey(file.FullName))
                        {
                            if (!historyDic[file.FullName].Equals(script))
                            {
                                context.Database.ExecuteSqlRaw(script);
                                var fileScrip = histories.First(x => x.FileName.Equals(file.FullName));
                                fileScrip.Script = script;
                            }
                        }
                        else
                        {
                            histories.Add(new Entities.History()
                            {
                                FileName = file.FullName,
                                Script = script,
                            });
                            context.Database.ExecuteSqlRaw(script);
                        }
                    }

                    context.SaveChanges();
                }
                
            }
        }
    }
}
