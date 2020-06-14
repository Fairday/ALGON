using System;
using System.Collections;
using System.Collections.Generic;

namespace ALGON.DataStructures.LinkedLists
{
    /// <summary>
    /// Реализация кольцевого односвязного списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularALinkedList<T> : ICollection<T>
    {
        /// <summary>
        /// Ссылка на первый элемент
        /// </summary>
        ALinkedListNode<T> _Head;
        /// <summary>
        /// Ссылка на последний элемент
        /// </summary>
        ALinkedListNode<T> _Tail;
        /// <summary>
        /// Количество элементов в списке
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Данная коллекция не может использоваться только для чтения
        /// </summary>
        public bool IsReadOnly => false;
        /// <summary>
        /// Добавление элемента в конец списка
        /// Сложность: O(1), если есть ссылка на последний узел
        /// Сложность: O(n), если требуется выполнять последовательный доступ к последнему элементу
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            var node = new ALinkedListNode<T>(item);

            if (Count == 0)
            {
                _Head = node;
                _Tail = node;
                _Tail.Next = _Head;
            }
            else
            {
                _Tail.Next = node;
                node.Next = _Head;
                _Tail = node;
            }

            Count++;
        }
        /// <summary>
        /// Удаление элемента 
        /// Сложность: O(n)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            ALinkedListNode<T> previous = null;
            ALinkedListNode<T> current = _Head;

            if (Count == 0)
                return false;

            do
            {
                if (current.Value.Equals(item))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current == _Tail)
                            _Tail = previous;
                    }
                    else
                    {
                        if (Count == 1)
                        {
                            _Head = null;
                            _Tail = null;
                        }
                        else
                        {
                            _Head = current.Next;
                            _Tail.Next = _Head;
                        }
                    }

                    Count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            while (current != _Head);
            return false;
        }
        /// <summary>
        /// Очистка коллекции
        /// Сложность: O(1)
        /// </summary>
        public void Clear()
        {
            _Head = null;
            _Tail = null;
            Count = 0;
        }
        /// <summary>
        /// Наличие элемента в коллекции
        /// Сложность: O(n)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            ALinkedListNode<T> current = _Head;

            if (Count == 0)
                return false;

            do
            {
                if (current.Value.Equals(item))
                    return true;
                else
                    current = current.Next;
            }
            while (current != _Head);
            return false;
        }
        /// <summary>
        /// Копирует данные из коллекции в массив начиная с определенного индекса
        /// Сложность: O(n)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array is null");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex is less than 0");

            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("The number of elements in the source System.Collections.Generic.ICollection`1 is greater " +
                    "than the available space from arrayIndex to the end of the destination array.");

            var current = _Head;
            do
            {
                if (current != null)
                {
                    array[arrayIndex++] = current.Value;
                    current = current.Next;
                }
            }
            while (current != _Head);
        }
        /// <summary>
        /// Обход по всем элементам
        /// Сложность получения итератора: O(1)
        /// Сложность обхода коллекции (очевидно): O(n)
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            ALinkedListNode<T> current = _Head;

            do
            {
                if (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }    
            }
            while (current != _Head);
        }
        /// <summary>
        /// Явная реализация интерфейса для получения итератора 
        /// Сложность получения итератора: O(1)
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
