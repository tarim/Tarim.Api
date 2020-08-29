using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Name;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarim.Api.Controllers
{

   // [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    public class NamesController : Controller
    {
        private readonly INameRepository _nameRepository;

        public NamesController(INameRepository nameRepository)
        {
            _nameRepository = nameRepository;
        }
        // GET: api/values
        [HttpGet("{pageNumber:int}")]
        public async Task<IActionResult> GetAsync(int pageNumber)
        {
            var uyghurNames = await _nameRepository.GetUyghurName(pageNumber);
            return Ok(uyghurNames.Object);
        }

        
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var result = await _nameRepository.GetUyghurName(name);
            return Ok(result.Object);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UyghurName uyghurName)
        {
            if (ModelState.IsValid)
            {
                var result = await _nameRepository.AddUyghurName(uyghurName);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        [HttpPut("name/{nameId:int}")]
        public async Task<IActionResult> PutAsync([FromBody]UyghurName uyghurName,int nameId)
        {
            if (ModelState.IsValid && uyghurName.Id>0 && uyghurName.Id==nameId)
            {
                var result = await _nameRepository.UpdateUyghurName(uyghurName);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result =await _nameRepository.DeleteUyghurName(id);
            return Ok(result);
        }

        [HttpPost("action/{nameId:int}")]
        public async Task<IActionResult> PostNameAction([FromBody]NameAction name,int nameId)
        {
            if (ModelState.IsValid && name.NameId==nameId)
            {
                var result = await _nameRepository.AddNameAction(name);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("top10")]
        public async Task<IActionResult> GetTopNames()
        {
            var result = await _nameRepository.GetTopNames();
            return Ok(result.Object);
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetNameStatistics()
        {
            var result = await _nameRepository.GetNameStatistics();
            return Ok(result.Object);
        }
    }
}
