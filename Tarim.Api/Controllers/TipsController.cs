using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Tips;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarim.Api.Controllers
{

    // [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    public class TipsController : ControllerBase
    {
        private readonly ITipsRepository _tipsRepository;

        public TipsController(ITipsRepository tipsRepository)
        {
            _tipsRepository = tipsRepository;
        }
        // GET: api/values
        [HttpGet("{pageNumber:int}")]
        public async Task<IActionResult> GetAsync(int pageNumber)
        {
            var result = await _tipsRepository.GetTips(pageNumber);
            return Ok(result.Object);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _tipsRepository.GetTips();
            return Ok(result.Object);
        }


        [HttpGet("tip/{id:int}")]
        public async Task<IActionResult> GetTip(int id)
        {
            var result = await _tipsRepository.GetTip(id);
            return Ok(result.Object);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Tip tip)
        {
            if (ModelState.IsValid)
            {
                var result = await _tipsRepository.AddTip(tip);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        [HttpPut("tip/{id:int}")]
        public async Task<IActionResult> PutAsync([FromBody] Tip tip, int id)
        {
            if (ModelState.IsValid && tip.Id > 0 && tip.Id == id)
            {
                var result = await _tipsRepository.UpdateTip(tip);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _tipsRepository.DeleteTip(id);
            return Ok(result);
        }

    }
}
