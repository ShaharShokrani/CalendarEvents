using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarEvents.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalendarEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private static IEnumerable<Event> _eventList = new List<Event>()
        {
            new Event() {Id = Guid.NewGuid() , Title = "Title1", Start = new DateTime(2018, 12, 1), End = new DateTime(2018, 12, 10)}           
        };

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Event>> Get()
        {
            return Ok(_eventList); 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Event> Get(Guid id)
        {
            return Ok(_eventList.Where(e => e.Id == id).FirstOrDefault());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
