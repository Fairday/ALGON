namespace ALGON.DataStructures.Details
{
    public interface IRAS<T>
    {
        bool Contains(T item);
        void Add(T item);
        bool Remove(T item);
    }
}
