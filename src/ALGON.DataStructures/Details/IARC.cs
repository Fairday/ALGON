namespace ALGON.DataStructures.Details
{
    public interface IARC<T>
    {
        void Add(T item);
        bool Remove(T item);
        bool Contains(T item);
    }
}
