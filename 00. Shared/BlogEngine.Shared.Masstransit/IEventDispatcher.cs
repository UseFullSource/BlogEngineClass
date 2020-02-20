namespace BlogEngine.Shared.Masstransit
{

    public interface IEventDispatcher
    {
        void Dispatch<T>(params T[] events);
    }
}
