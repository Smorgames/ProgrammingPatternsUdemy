using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<object> func = SingletonFactory.GetSingletonObject;
            Func<object> func2 = SingletonFactory.GetNotSingletonObject;
            
            Console.WriteLine(IsSingleton(func));
            Console.WriteLine(IsSingleton(func2));
        }
        
        public static bool IsSingleton(Func<object> func)
        {
            var obj1 = func.Invoke();
            var obj2 = func.Invoke();
            return obj1 == obj2;
        }
    }
    
    public class SingletonObject
    {
        private static Lazy<SingletonObject> _instance =
            new Lazy<SingletonObject>(() => new SingletonObject());

        public static SingletonObject Instance => _instance.Value;
    }

    public class SingletonFactory
    {
        public static SingletonObject GetSingletonObject()
        {
            var obj = SingletonObject.Instance;
            return obj;
        }

        public static object GetNotSingletonObject()
        {
            var obj = new object();
            return obj;
        }
    }
}
