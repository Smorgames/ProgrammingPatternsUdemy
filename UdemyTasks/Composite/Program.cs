using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
             var mv = new ManyValues();
             var mv2 = new ManyValues();
             
             mv.Add(1);
             mv.Add(2);
             mv.Add(3);
             mv.Add(4);
             mv.Add(5);
             mv2.Add(6);
             mv2.Add(7);
             mv2.Add(8);
             mv2.Add(9);
             mv2.Add(10);

             var iValueContainerList = new List<IValueContainer>() { mv, mv2 };
             Console.WriteLine(iValueContainerList.Sum());
        }
    }
    
    public interface IValueContainer : IEnumerable<int>
    {
        public int i { get; set; }
    }

    public class SingleValue : IValueContainer
    {
        public int Value;
        public int i { get; set; }

        public IEnumerator<int> GetEnumerator()
        {
            if (Value != i)
                i = Value;
            
            yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {
        public int i { get; set; }
    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            
            return result;
        }
    }
}
