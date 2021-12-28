using System;

namespace F1T.DataStructures
{
    public class FixedSizeQueue<T>
    {
        private T[] items;
        private int top = 0;
        public FixedSizeQueue(int capacity)
        {
            items = new T[capacity];
        }

        public void Push(T item)
        {
            // "scroll" the whole chart to the left
            Array.Copy(items, 1, items, 0, items.Length - 1);

            items[items.Length - 1] = item; 
        }

        public T[] ToArray()
        {
            return items;
        }
    }
}
