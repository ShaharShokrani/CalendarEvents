using CalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.Services
{
    public interface IEventsService : IGetService<EventModel, Guid>, 
                                      IUpdateService<EventModel,Guid>,
                                      IAddService<EventModel>,
                                      IRemoveService<Guid>
    {
    }

    public class EventsService : IEventsService
    {
        private CalendarDbContext _dbContext;

        public EventsService(CalendarDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public ResultService<EventModel> Add(EventModel item)
        {
            try
            {                
                _dbContext.Events.Add(item);
                _dbContext.SaveChanges();

                return ResultService.Ok<EventModel>(item);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<EventModel>(ex);
            }
        }

        public ResultService<IEnumerable<EventModel>> GetAllItems()
        {
            try
            {
                var result = this._dbContext.Events;
                if (result == null)
                {
                    return ResultService.Fail<IEnumerable<EventModel>>(ErrorCode.NotFound);
                }
                return ResultService.Ok<IEnumerable<EventModel>>(this._dbContext.Events);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<IEnumerable<EventModel>>(ex);
            }
        }

        public ResultService<EventModel> GetById(Guid id)
        {
            try
            {
                var entity = this._dbContext.Events.Find(id);
                if (entity == null)
                {
                    return ResultService.Fail<EventModel>(ErrorCode.NotFound);
                }

                return ResultService.Ok<EventModel>(entity);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<EventModel>(ex);
            }
        }

        public ResultService Remove(Guid id)
        {
            try
            {
                var entity = this._dbContext.Events.Find(id);
                if (entity == null)
                {
                    return ResultService.Fail(ErrorCode.NotFound);
                }
                this._dbContext.Events.Remove(entity);
                this._dbContext.SaveChanges();

                return ResultService.Ok();
            }
            catch (Exception ex)
            {
                return ResultService.Fail(ex);
            }
        }

        public ResultService Update(Guid id, EventModel item)
        {
            var entity = this._dbContext.Events.Find(id);

            if (entity == null)
            {
                return ResultService.Fail(ErrorCode.NotFound);
            }

            //TODO: add AutoMapper
            entity.Title = item.Title;
            entity.IsAllDay = item.IsAllDay;
            entity.Start = item.Start;
            entity.End = item.End;

            this._dbContext.SaveChanges();

            return ResultService.Ok();
        }
    }
}
