using Entities.Slide;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shopWeb.Controllers
{
    //http://hamid.ir/api/app/SlideList
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        public AppController() { }



        [HttpGet]
        public List<Slide> SlideList(string id)
        {
            List<Slide> slides = new List<Slide>();
            slides.Add(new Slide { });

            return slides;
        }


        // GET: api/<AppController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AppController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AppController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AppController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
