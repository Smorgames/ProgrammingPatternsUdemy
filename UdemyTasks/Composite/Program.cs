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
             var sv = new SingleValue() { i = 4 };
             var mv = new ManyValues();
             
             mv.Add(4);
             mv.Add(3);
             mv.Add(2);
             mv.Add(1);
             mv.Add(5);
            
             Console.WriteLine(sv.Sum());
             Console.WriteLine(mv.Sum());
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

    public class Test
    {
        
    }

    public class Test2 : List<int>
    {
        
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
