namespace CalendarEvents.Services
{
    public interface IAddService<T>
    {
        ResultService<T> Add(T obj);
    }
}
