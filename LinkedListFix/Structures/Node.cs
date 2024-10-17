using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListFix.Structures
{
    class Node<T>
    {//needs to have list and previous 
        public T Value { get; set; }

        public Node<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }

        public Node(T value, Node<T> next)
        {
            Value = value;
            Next = next;
        }
    }
}
