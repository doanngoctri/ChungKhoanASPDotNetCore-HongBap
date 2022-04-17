using ChungKhoanASPDotNetCore.Applications.Interfaces;
using ChungKhoanASPDotNetCore.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChungKhoanASPDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LenhDatController : ControllerBase
    {
        ILenhDatService _service;
        public LenhDatController(ILenhDatService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var lenhDat = await _service.Get(id);
            if(lenhDat == null)
            {
                return BadRequest($"Does not found Id = {id}");
            }
            return Ok(await _service.Get(id));
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LenhDatRequest request)
        {
            var res = await _service.Post(request);
            if(res == false)
            {
                return BadRequest("Something went wrong");
            }
            return Ok("Susscess");
        }
    }
}
