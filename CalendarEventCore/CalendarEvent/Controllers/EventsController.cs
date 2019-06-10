using System;
using System.Collections.Generic;
using CalendarEvents.Models;
using CalendarEvents.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalendarEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {        
        private readonly IGenericService<EventModel> _eventsService;        

        public EventsController(IGenericService<EventModel> eventsService)
        {
            this._eventsService = eventsService;
        }

        // GET api/events
        [HttpGet]
        public ActionResult<IEnumerable<EventModel>> Get()
        {
            ResultService<IEnumerable<EventModel>> result = this._eventsService.Get();
            if (result.Success)
            {
                IEnumerable<EventModel> list = result.Value as IEnumerable<EventModel>;
                return Ok(list);
            }
            else
            {
                return StatusCode(500, result.ErrorCode);
            }            
        }

        // GET api/events/c4df7159-2402-4f49-922c-1a2caef02de2
        [HttpGet("{id}", Name = "GET")]
        public ActionResult<EventModel> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            ResultService<EventModel> result = this._eventsService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(500, result.ErrorCode);
            }
        }

        // POST api/events
        [HttpPost]
        public ActionResult Post([FromBody] EventModel item)
        {
            item.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ResultService result = this._eventsService.Insert(item);
            if (result.Success)
            {
                return CreatedAtAction("Post", new { item.Id }, item);
            }
            else
            {
                return StatusCode(500, result.ErrorCode);
            }
        }

        // PUT api/events/c4df7159-2402-4f49-922c-1a2caef02de2
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] EventModel item)
        {
            if (item.Id == Guid.Empty || !ModelState.IsValid)
            {
                return BadRequest();
            }

            ResultService result = this._eventsService.Update(item);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, result.ErrorCode);
            }
        }

        // DELETE api/events/c4df7159-2402-4f49-922c-1a2caef02de2
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            ResultService result = this._eventsService.Delete(id);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, result.ErrorCode);
            }
        }
    }
}
