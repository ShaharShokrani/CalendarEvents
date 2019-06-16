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
        public ActionResult<IEnumerable<EventModel>> Get([FromBody]GenericRequest<EventModel> genericRequest = null)
        {
            try
            {
                if (genericRequest == null)
                    genericRequest = new GenericRequest<EventModel>();

                ResultService<IEnumerable<EventModel>> result = this._eventsService.Get(genericRequest.Filters, genericRequest.OrderBy, genericRequest.IncludeProperties);
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
            catch (Exception ex)
            {
                return StatusCode(500, ErrorCode.Unknown);
                //TODO: Log the Exception.
            }
        }

        // GET api/events/c4df7159-2402-4f49-922c-1a2caef02de2
        [HttpGet("{id}", Name = "GET")]
        public ActionResult<EventModel> Get(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ErrorCode.Unknown);
                //TODO: Log the Exception.
            }
        }

        // POST api/events
        [HttpPost]
        public ActionResult Post([FromBody] EventModel item)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ErrorCode.Unknown);
                //TODO: Log the Exception.
            }
        }

        // PUT api/events/c4df7159-2402-4f49-922c-1a2caef02de2
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] EventModel item)
        {
            try
            {
                //TODO: move this item.Id == Guid.Empty to ModelState.IsValid.
                if (!ModelState.IsValid)
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
            catch (Exception ex)
            {
                return StatusCode(500, ErrorCode.Unknown);
                //TODO: Log the Exception.
            }
        }

        // DELETE api/events/c4df7159-2402-4f49-922c-1a2caef02de2
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ErrorCode.Unknown);
                //TODO: Log the Exception.
            }
        }
    }
}
