using System;

namespace F1T.DataStructures
{
    /// <summary>
    /// A First in First out (FIFO) queue implementation with a max size
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedSizeQueue<T>
    {
        private T[] items;
        public FixedSizeQueue(int capacity)
        {
            items = new T[capacity];
        }

        /// <summary>
        /// Pushes an item onto the <see cref="FixedSizeQueue{T}"/>
        /// <para>Removes an item if greater than capacity</para>
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            Array.Copy(items, 1, items, 0, items.Length - 1);
            items[items.Length - 1] = item; 
        }
        /// <summary>
        /// Turns the <see cref="FixedSizeQueue{T}"/> into a generic <see cref="Array"/> of type <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return items;
        }

        /// <summary>
        /// Returns the number of items in the Queue
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return items.Length;
        }
    }
}
