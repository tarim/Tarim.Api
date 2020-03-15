using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.names;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarim.Api.Controllers
{

    [Route("api/[controller]")]
    public class NameController : Controller
    {
        private readonly INameRepository _nameRepository;

        public NameController(INameRepository nameRepository)
        {
            _nameRepository = nameRepository;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var uyghurNames = await _nameRepository.GetUyghurName();
            return Ok(uyghurNames.Object);
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var result = await _nameRepository.GetUyghurName(name);
            return Ok(result);
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
        [HttpPut]
        public async Task<IActionResult> PutAsync( [FromBody]UyghurName uyghurName)
        {
            if (ModelState.IsValid && uyghurName.Id>0)
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
    }
}
