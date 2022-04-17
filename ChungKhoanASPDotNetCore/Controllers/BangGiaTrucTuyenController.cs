using ChungKhoanASPDotNetCore.Applications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChungKhoanASPDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BangGiaTrucTuyenController : ControllerBase
    {
        IBangDienService _service;

        public BangGiaTrucTuyenController(IBangDienService service)
        {
            _service = service;

        }
        [HttpGet]
        public IActionResult Get()
        {
            var bangGia = _service.Get();

            return Ok(bangGia);
        }
    }
}
