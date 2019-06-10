using CalendarEvents.Models;
using CalendarEvents.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CalendarEvents.Services
{
    public interface IGenericService<T> : IGetService<T>, 
                                      IUpdateService<T>,
                                      IInsertService<T>,
                                      IDeleteService
    {        
    }

    public class GenericService<T> : IGenericService<T> where T : IBaseModel
    {
        private IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            this._repository = repository;
        }

        public ResultService Insert(T obj)
        {
            try
            {
                obj.Id = Guid.NewGuid();
                obj.CreateDate = DateTime.UtcNow;
                this._repository.Insert(obj);

                return ResultService.Ok();
            }
            catch (Exception ex)
            {
                return ResultService.Fail(ex);
            }
        }

        public ResultService<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            try
            {
                var result = this._repository.Get(filter, orderBy, includeProperties);
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

        public ResultService Delete(object id)
        {
            try
            {
                var entity = this._repository.GetById(id);
                if (entity == null)
                {
                    return ResultService.Fail(ErrorCode.NotFound);
                }
                this._repository.Remove(entity);

                return ResultService.Ok();
            }
            catch (Exception ex)
            {
                return ResultService.Fail(ex);
            }
        }

        public ResultService Update(T obj)
        {
            try
            {
                var entity = this._repository.GetById(obj.Id);
                if (entity == null)
                {
                    return ResultService.Fail<T>(ErrorCode.NotFound);
                }
                this._repository.Update(obj);
                return ResultService.Ok(entity);
            }
            catch (Exception ex)
            {
                return ResultService.Fail(ex);
            }
        }        
    }
}
