namespace ALGON.DataStructures.LinkedLists
{
    internal class ALinkedListNode<T>
    {
        public ALinkedListNode(T value)
        {
            Value = value;
        }

        public T Value { get; internal set; }
        public ALinkedListNode<T> Next { get; internal set; }
    }
}
