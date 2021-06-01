using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<object> factoryMethod = ObjectFactory.GetNotSingletonObject;
            Console.WriteLine(SingletonTester.IsSingleton(factoryMethod));

            factoryMethod = ObjectFactory.GetSingletonObject;
            Console.WriteLine(SingletonTester.IsSingleton(factoryMethod));
        }
    }

    public class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            object obj1 = func();
            object obj2 = func();
            return object.Equals(obj1, obj2);
        }
    }

    public class SingletonObject
    {
        private static Lazy<SingletonObject> _instance = new Lazy<SingletonObject>(() => new SingletonObject());

        public static SingletonObject Instance => _instance.Value;
    }

    public class ObjectFactory
    {
        public static object GetNotSingletonObject()
        {
            object obj = new object();
            return obj;
        }

        public static object GetSingletonObject()
        {
            SingletonObject singleton = SingletonObject.Instance;
            return singleton;
        }
    }
}
