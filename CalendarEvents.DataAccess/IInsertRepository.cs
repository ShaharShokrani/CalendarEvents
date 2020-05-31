namespace CalendarEvents.DataAccess
{
    public interface IInsertRepository<TEntity>
    {
        void Insert(TEntity entity);
    }
}
