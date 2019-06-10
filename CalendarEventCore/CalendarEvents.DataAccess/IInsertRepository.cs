namespace CalendarEvents.DataAccess
{
    //TODO: Add async method.
    public interface IInsertRepository<TEntity>
    {
        void Insert(TEntity entity);
    }
}
