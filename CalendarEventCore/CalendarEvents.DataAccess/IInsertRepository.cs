namespace CalendarEvents.DataAccess
{
    public interface IInsertRepository<T>
    {
        T Insert(T item);
    }
}
