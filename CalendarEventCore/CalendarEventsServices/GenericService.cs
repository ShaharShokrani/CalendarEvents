using CalendarEvents.Models;
using CalendarEvents.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.Services
{
    public interface IService<T> : IGetService<T>, 
                                      IUpdateService<T>,
                                      IAddService<T>,
                                      IRemoveService
    {        
    }

    public class GenericService<T> : IService<T>
    {
        private IRepository<T> _repository;

        public GenericService(IRepository<T> repository)
        {
            this._repository = repository;
        }

        public ResultService<T> Add(T obj)
        {
            try
            {
                T newItem = this._repository.Insert(obj);
                //_dbContext.Events.Add(item);
                //_dbContext.SaveChanges();

                return ResultService.Ok<T>(newItem);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<T>(ex);
            }
        }

        public ResultService<IEnumerable<T>> GetAllItems()
        {
            try
            {
                var result = this._repository.GetAllItems();
                if (result == null)
                {
                    return ResultService.Fail<IEnumerable<T>>(ErrorCode.NotFound);
                }
                return ResultService.Ok<IEnumerable<T>>(result);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<IEnumerable<T>>(ex);
            }
        }

        public ResultService<T> GetById(object id)
        {
            try
            {
                var entity = this._repository.GetById(id);
                if (entity == null)
                {
                    return ResultService.Fail<T>(ErrorCode.NotFound);
                }

                return ResultService.Ok<T>(entity);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<T>(ex);
            }
        }

        public ResultService Remove(object id)
        {
            try
            {
                var entity = this._repository.GetById(id);
                if (entity == null)
                {
                    return ResultService.Fail(ErrorCode.NotFound);
                }
                var isDeleted = this._repository.Delete(id);

                return ResultService.Ok();
            }
            catch (Exception ex)
            {
                return ResultService.Fail(ex);
            }
        }

        public ResultService<T> Update(T obj)
        {
            try
            {
                var entity = this._repository.Update(obj);
                if (entity == null)
                {
                    return ResultService.Fail<T>(ErrorCode.NotFound);
                }
                return ResultService.Ok<T>(entity);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<T>(ex);
            }
        }
    }
}
