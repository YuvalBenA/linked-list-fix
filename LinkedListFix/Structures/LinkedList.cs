using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListFix.Structures
{
    class LinkedList<T> : IList<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Last { get; private set; }
        public T this[int index]
        {
            get
            {
                return GetNodeByIndex(index).Value;
            }
            set
            {
                GetNodeByIndex(index).Value = value;
            }
        }

        public int Count { get; }

        public bool IsReadOnly { get; }

        private Node<T> GetNodeByIndex(int index)
        {
            if (index < Count && index > 0)
            {
                int placement = 0;
                Node<T> currentNode = Head;
                while (placement != index)
                {
                    currentNode = currentNode.Next;
                }
                return currentNode;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        private int GetLength()
        {
            if (Head == null)
            {
                return 0;
            }

            int countNodes = 1;
            Node<T> currentNode = Head;

            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
                countNodes++;
            }

            return countNodes;
        }

        public void Add(T item)
        {
            try
            {
                Last.Next = item as Node<T>;
                Last = Last.Next;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Item needs to be a Node.");
            }
        }

        public void Clear()
        {
            Head = null;
            Last = null;
        }

        public bool Contains(T item)
        {
            try
            {
                Node<T> searchedNode = item as Node<T>;
                Node<T> currentNode = Head;
                while (currentNode.Next != null)
                {
                    if (currentNode == searchedNode)
                    {
                        return true;
                    }
                    currentNode = currentNode.Next;
                }
                return currentNode == searchedNode;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Item needs to be a Node.");
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < array.Length && arrayIndex > -1)
            {
                Array.Resize(ref array, array.Length + Count);
                T[] leftovers = new T[Count];
                int currentIndex = arrayIndex;
                Node<T> currentNode = Head;
                for (int i = 0; i < leftovers.Length; i++)
                {
                    leftovers[i] = array[currentIndex];
                    array[currentIndex] = currentNode.Value;
                    currentNode = currentNode.Next;
                    currentIndex++;
                }
                for (int i = currentIndex; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        for (int j = 0; j < leftovers.Length; j++)
                        {
                            array[i] = leftovers[j];
                            i++;
                        }
                        break;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = Head;
            while (currentNode.Next != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        public int IndexOf(T item)
        {
            try
            {
                Node<T> searchedNode = item as Node<T>;
                Node<T> currentNode = Head;
                int currentIndex = 0 ;
                while (currentNode.Next != null)
                {
                    if (currentNode == searchedNode)
                    {
                        return currentIndex;
                    }
                    currentIndex++;
                    currentNode = currentNode.Next;
                }
                if (currentNode == searchedNode)
                {
                    return currentIndex;
                }
                return -1;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Item needs to be a Node.");
            }
        }

        public void Insert(int index, T item)
        {
            try
            {
                Node<T> selectedNode = GetNodeByIndex(index);
                selectedNode = item as Node<T>;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Item needs to be a Node.");
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public bool Remove(T item)
        {
            try
            {
                int previousIndex = IndexOf(item) - 1;
                Node<T> selectedNode = item as Node<T>;
                GetNodeByIndex(previousIndex).Next = selectedNode.Next;
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;   
            }
        }

        public void RemoveAt(int index)
        {
            try
            {
                Node<T> previousNode = GetNodeByIndex(index - 1);
                previousNode.Next = previousNode.Next.Next;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
