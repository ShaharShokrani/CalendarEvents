using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CalendarEvents.DataAccess;
using CalendarEvents.Models;
using CalendarEvents.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {        
        private readonly IGenericService<EventModel> _eventsService;
        private readonly IMapper _mapper;

        public EventsController(IGenericService<EventModel> eventsService, IMapper mapper)
        {
            this._eventsService = eventsService;
            this._mapper = mapper;
        }

        // GET api/events
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetRequest<EventModelDTO> genericRequestDTO = null)
        {
            try
            {
                if (genericRequestDTO == null)
                    genericRequestDTO = new GetRequest<EventModelDTO>();

                GetRequest<EventModel> genericRequest = _mapper.Map<GetRequest<EventModel>>(genericRequestDTO);
                ResultHandler<IEnumerable<EventModel>> result = await this._eventsService.Get(genericRequest.Filters, genericRequest.OrderBy, genericRequest.IncludeProperties);
                if (result.Success)
                {
                    IEnumerable<EventModel> list = result.Value as IEnumerable<EventModel>;
                    IEnumerable<EventModelDTO> listDTO = _mapper.Map<IEnumerable<EventModelDTO>>(list);
                    return Ok(listDTO);
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
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GET")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest();
                }

                ResultHandler<EventModel> result = await this._eventsService.GetById(id);
                if (result.Success)
                {
                    EventModel eventModel = result.Value as EventModel;
                    EventModelDTO eventModelDTO = _mapper.Map<EventModelDTO>(eventModel);

                    return Ok(eventModelDTO);
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
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]        
        public async Task<IActionResult> Post([FromBody] EventPostRequest request = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                EventModel item = _mapper.Map<EventModel>(request);
                item.Id = Guid.NewGuid();
                item.CreateDate = DateTime.UtcNow;
                item.UpdateDate = DateTime.UtcNow;
                ResultHandler rh = await this._eventsService.Insert(item);
                if (rh.Success)
                {                    
                    return CreatedAtAction("Post", new { item.Id }, item.Id);
                }
                else
                {
                    return StatusCode(500, rh.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorCode.Unknown);
                //TODO: Log the Exception.
            }
        }

        // PUT api/events/
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EventPutRequest request)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                ResultHandler<EventModel> getByIdResult = await this._eventsService.GetById(request.Id);
                if (!getByIdResult.Success)
                {
                    return StatusCode(500, getByIdResult.ErrorCode);
                }

                EventModel item = getByIdResult.Value as EventModel;
                item.End = request.End;
                item.IsAllDay = request.IsAllDay;
                item.Name = request.Name;
                item.Start = request.Start;
                item.URL = request.URL;
                item.UpdateDate = DateTime.UtcNow;
                
                ResultHandler result = await this._eventsService.Update(item);
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
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest();
                }

                ResultHandler rh = await this._eventsService.Delete(id);
                if (rh.Success)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, rh.ErrorCode);
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
