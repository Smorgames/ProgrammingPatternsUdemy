using System;
using System.Collections.Generic;

namespace Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            var e = new Node<string>("e");
            var d = new Node<string>("d");
            var c = new Node<string>("c");

            var b = new Node<string>("b", c, d);
            var a = new Node<string>("a", b, e);

            var list = a.PreOrder;

            foreach (var l in list)
                Console.Write($"{l} ");
        }
    }
    
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        public IEnumerable<T> PreOrder
        {
            get
            {
                List<T> list = new List<T>();
                list.Add(Value);
                
                if (Left != null)
                    list.AddRange(Left.PreOrder);
                
                if (Right != null)
                    list.AddRange(Right.PreOrder);

                return list;
            }
        }
    }
}
