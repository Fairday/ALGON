using ALGON.DataStructures.Details;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ALGON.DataStructures.LinkedLists
{
    /// <summary>
    /// Базовая (учебная) реализация двусвязного списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ALinkedList<T> : ICollection<T>, IARC<T>, IList<T>
    {
        /// <summary>
        /// Ссылка на первый элемент
        /// </summary>
        public ALinkedListNode<T> Head { get; private set; }
        /// <summary>
        /// Ссылка на последний элемент
        /// </summary>
        public ALinkedListNode<T> Tail { get; private set; }
        /// <summary>
        /// Возращает количество элементов списка
        /// Сложность: O(1)
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Данная коллекция не может использоваться только для чтения
        /// </summary>
        public bool IsReadOnly => false;
        /// <summary>
        /// Индексатор доступа к элементу/изменения значения элемента по индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// Добавление элемента в конец списка
        /// Сложность: O(1), если есть ссылка на последний узел
        /// Сложность: O(n), если требуется выполнять последовательный доступ к последнему элементу
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            AddLast(item);
        }
        /// <summary>
        /// Добавление элемента в начало списка
        /// Сложность: O(1)
        /// </summary>
        /// <param name="item"></param>
        public void AddFirst(T item)
        {
            var node = new ALinkedListNode<T>(item);

            //Ссылка на текущий первый элемента
            var temp = Head;
            //Вставляем перед текущим первым элементом
            Head = node;
            Head.Next = temp;
            //Если элементов не было
            if (Count == 0)
            {
                Tail = Head;
            }
            else
            {
                temp.Previous = Head;
            }

            Count++;
        }
        /// <summary>
        /// Добавление элемента в конец
        /// Сложность: O(1)
        /// </summary>
        /// <param name="item"></param>
        public void AddLast(T item)
        {
            var node = new ALinkedListNode<T>(item);

            if (Count == 0)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }

            Tail = node;
            Count++;
        }
        /// <summary>
        /// Очистка коллекции
        /// Сложность: O(1)
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
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
            ALinkedListNode<T> current = Head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    //Найденный узел в середине или в конце
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        //Обновляем хвост
                        if (current.Next == null)
                            Tail = previous;
                        else
                            current.Next.Previous = previous;

                        Count--;
                    }
                    //Найденный узел первый или единственный в списке
                    else
                    {
                        RemoveFirst();
                    }

                    return true;
                }

                previous = current;
                current = previous.Next;
            }

            return false;
        }
        /// <summary>
        /// Удаление первого элемента
        /// Сложность: O(1)
        /// </summary>
        /// <returns></returns>
        public bool RemoveFirst()
        {
            if (Count != 0)
            {
                Head = Head.Next;

                Count--;

                if (Count == 0)
                {
                    Tail = null;
                }
                else
                {
                    Head.Previous = null;
                }

                return true;
            }

            return false;
        }
        /// <summary>
        /// Удаление последнео элемента
        /// Сложность: O(1)
        /// </summary>
        /// <returns></returns>
        public bool RemoveLast()
        {
            if (Count != 0)
            {
                Tail = Tail.Previous;

                Count--;

                if (Count == 0)
                {
                    Head = null;
                }
                else
                {
                    Tail.Next = null;
                }
            }

            return false;
        }
        /// <summary>
        /// Наличие элемента в коллекции
        /// Сложность: O(n)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                    return true;

                current = current.Next;
            }

            return false;
        }
        /// <summary>
        /// Копирует данные из коллекции в массив начиная с определенного индекса
        /// Сложность: O(n)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <exception cref="System.ArgumentNullException">array is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0</exception>
        /// <exception cref="System.ArgumentException">The number of elements in the source System.Collections.Generic.ICollection`1 is greater " + "than the available space from arrayIndex to the end of the destination array.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array is null");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex is less than 0");

            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("The number of elements in the source System.Collections.Generic.ICollection`1 is greater " +
                    "than the available space from arrayIndex to the end of the destination array.");

            var current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }
        /// <summary>
        /// Обход по всем элементам
        /// Сложность получения итератора: O(1)
        /// Сложность обхода коллекции (очевидно): O(n)
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
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
        /// <summary>
        /// Определяет: есть ли цикл в данном связном списке
        /// Временная сложность: O(n)
        /// </summary>
        /// <returns></returns>
        public bool CycleExist()
        {
            if (Head == null)
                return false;

            var slow = Head;
            var fast = Head.Next;

            while (slow != fast) 
            {
                if (fast == null || fast.Next == null)
                    return false;

                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return true;
        }
        /// <summary>
        /// Возвращает узел, на котором начинается циклическая структура
        /// Временная сложность: O(n)
        /// </summary>
        /// <returns></returns>
        public ALinkedListNode<T> CycleStart()
        {
            if (Head == null)
                return null;

            /// TODO: Подумать над алгоритм без дополнительного места
            var visitedNodes = new HashSet<ALinkedListNode<T>>();
            var current = Head;
            visitedNodes.Add(current);

            while (current != null) 
            {
                current = current.Next;
                if (!visitedNodes.Add(current)) 
                    return current;
            }

            return null;
        }
        /// <summary>
        /// Возвращает индекс первого вхождения элемента
        /// Возвращает -1, если элемент отсутствует в списке
        /// Временная сложность: O(n)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            if (Head == null)
                return -1;

            var current = Head;
            var i = 0;

            while (current != null)
            {
                if (current.Value.Equals(item)) 
                    return i;
                current = current.Next;
                i++;
            }

            return -1;
        }
        /// <summary>
        /// Удаляет элемент по индексу
        /// Временная сложность: O(n)
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="System.ArgumentOutOfRangeException">index out of range</exception>
        public void RemoveAt(int index)
        {
            if (index > Count - 1 || index < 0)
            {
                throw new ArgumentOutOfRangeException($"Index {index} out of range");
            }

            if (index == Count - 1)
                RemoveLast();

            if (index == 0)
                RemoveFirst();

            var current = Head;
            var i = 0;

            while (current != null) 
            {
                if (++i == index) 
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    Count--;
                    break;
                }
                current = current.Next;
            }
        }
        /// <summary>
        /// Вставка элемента по индексу
        /// Временная сложность: O(n)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <exception cref="System.ArgumentOutOfRangeException">index out of range</exception>
        public void Insert(int index, T item)
        {
            if (index > Count || index < 0)
            {
                throw new ArgumentOutOfRangeException($"Index {index} out of range");
            }

            if (index == Count)
                AddLast(item);

            if (index == 0)
                AddFirst(item);

            var node = new ALinkedListNode<T>(item);
            var current = Head;
            var i = 0;

            while (current != null)
            {
                if (++i == index) 
                {
                    current.Next.Previous = node;
                    node.Next = current.Next;
                    node.Previous = current;
                    current.Next = node;
                    Count++;
                    break;
                }
                current = current.Next;
            }
        }
        /// <summary>
        /// Выполняет функцию разворота списка
        /// Временная сложность: O(n)
        /// </summary>
        public void Reverse()
        {
            if (Head == null)
                return;

            var prev = Head;
            var head = Head.Next;
            var current = head;
            Tail = prev;
            prev.Next = null;

            while (head != null)
            {
                head = Head.Next;
                current.Next = prev;
                prev.Previous = current;
                prev = current;
                current = head;
            }

            Head = prev;
        }
    }
}
