namespace ALGON.DataStructures.LinkedLists
{
    public class ALinkedListNode<T>
    {
        public ALinkedListNode(T value)
        {
            Value = value;
        }

        public T Value { get; internal set; }
        public ALinkedListNode<T> Previous { get; internal set; }
        public ALinkedListNode<T> Next { get; internal set; }
    }
}
