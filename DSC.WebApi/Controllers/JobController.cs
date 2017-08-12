using System.Linq;
using DSC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSC.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private DSCContext _context;

        public JobController(DSCContext context)
        {
            _context = context;

            if (!_context.Jobs.Any())
            {
                _context.Jobs.AddRange(
                    new Job {Name = "Job1", IsCompleted = false},
                    new Job {Name = "Job2", IsCompleted = true}
                );
                _context.SaveChanges();
            }
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var items = _context.Jobs;
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
            var item = _context.Find<Job>(id);
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

            _context.Add(item);
            _context.SaveChanges();

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

            var existingJob = _context.Find<Job>(id);

            if (existingJob == null)
            {
                return NotFound();
            }

            existingJob.Name = item.Name;
            existingJob.IsCompleted = item.IsCompleted;

            _context.Update(existingJob);
            _context.SaveChanges();

            return new NoContentResult();
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingJob = _context.Find<Job>(id);

            if (existingJob == null)
            {
                return NotFound();
            }

            _context.Remove(existingJob);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
