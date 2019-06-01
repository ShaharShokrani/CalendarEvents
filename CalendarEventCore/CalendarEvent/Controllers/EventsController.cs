using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarEvents.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalendarEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        //TODO: remove this dependency into IService, IRepository using autofac.
        private CalendarDbContext _dbContext;        

        public EventsController(CalendarDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<EventModel>> Get()
        {
            return Ok(this._dbContext.Events); 
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GET")]
        public ActionResult<EventModel> Get(Guid id)
        {
            var @event = this._dbContext.Events.Find(id);
            return Ok(@event);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] EventModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _dbContext.Events.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction("Post", new { Id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] EventModel item)
        {
            var entity = this._dbContext.Events.Find(id);

            if (entity == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            entity.Title = item.Title;
            entity.IsAllDay = item.IsAllDay;
            entity.Start = item.Start;
            entity.End = item.End;

            this._dbContext.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var entity = this._dbContext.Events.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _dbContext.Events.Remove(entity);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
