using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Proverbs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarim.Api.Controllers
{

    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    public class ProverbsController : Controller
    {
        private readonly IProverbRepository _proverbRepository;

        public ProverbsController(IProverbRepository proverbRepository)
        {
            _proverbRepository = proverbRepository;
        }
        // GET: api/values
        [HttpGet("{pageNumber:int}")]
        public async Task<IActionResult> GetAsync(int pageNumber)
        {
            var result = await _proverbRepository.GetProverbs(pageNumber);
            return Ok(result.Object);
        }

         [HttpGet("daily/{pageSize:int}")]
        public async Task<IActionResult> GetDailyProverb(int pageSize)
        {
            var result = await _proverbRepository.GetDailyProverb(pageSize);
            return Ok(result.Object);
        }

        
        [HttpGet("proverb/{id:int}")]
        public async Task<IActionResult> GetProverb(int id)
        {
            var result = await _proverbRepository.GetProverb(id);
            return Ok(result.Object);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Proverb proverb)
        {
            if (ModelState.IsValid)
            {
                var result = await _proverbRepository.AddProverb(proverb);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        [HttpPut("proverb/{id:int}")]
        public async Task<IActionResult> PutAsync([FromBody]Proverb proverb,int id)
        {
            if (ModelState.IsValid && proverb.Id>0 && proverb.Id==id)
            {
                var result = await _proverbRepository.UpdateProverb(proverb);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result =await _proverbRepository.DeleteProverb(id);
            return Ok(result);
        }

    }
}
