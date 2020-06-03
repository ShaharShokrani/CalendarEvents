using System.Threading.Tasks;

namespace CalendarEvents.DataAccess
{
    public interface IInsertRepository<TEntity>
    {
        Task Insert(TEntity entity);
    }
}
