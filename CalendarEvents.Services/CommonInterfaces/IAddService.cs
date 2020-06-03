using System.Threading.Tasks;

namespace CalendarEvents.Services
{
    public interface IInsertService<T>
    {
        Task<ResultHandler> Insert(T obj);
    }
}
