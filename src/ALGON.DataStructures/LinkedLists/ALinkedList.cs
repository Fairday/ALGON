﻿using ALGON.DataStructures.Details;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ALGON.DataStructures.LinkedLists
{
    /// <summary>
    /// Базовая (учебная) реализация двусвязного списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ALinkedList<T> : ICollection<T>, IARC<T>
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
    }
}
