using System.Linq;
using DSC.Database.Domain;
using DSC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSC.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private IJobRepository _repository;

        public JobController(IJobRepository repository)
        {
            _repository = repository;

       }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var items = _repository.GetList();
            if (!items.Any())
            {
                return NotFound();
            }

            return new ObjectResult(items);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetJob")]
        public IActionResult Get(int id)
        {
            var item = _repository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Job item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _repository.Save(item);
           
            return CreatedAtRoute(routeName: "GetJob", routeValues: new { id = item.Id }, value: item);
        }

        // PUT api/job/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Job item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var existingJob = _repository.GetById(id);

            if (existingJob == null)
            {
                return NotFound();
            }

            _repository.Save(item);
  
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingJob = _repository.GetById(id);

            if (existingJob == null)
            {
                return NotFound();
            }

            _repository.Delete(_repository.GetById(id));
           
            return new NoContentResult();
        }
    }
}
