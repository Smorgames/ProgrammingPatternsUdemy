using System;

namespace Factory
{
    class Program
    {
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Person(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString() => $"{Name} have {Id} ID";

            public class PersonFactory
            {
                private static int _counter = 0;

                public static Person CreatePerson(string name)
                {
                    var person = new Person(_counter, name);
                    _counter++;
                    return person;
                }
            }
        }

        static void Main(string[] args)
        {
            Person person1 = Person.PersonFactory.CreatePerson("Jack"); 
            Person person2 = Person.PersonFactory.CreatePerson("Jerry");

            Console.WriteLine(person1);
            Console.WriteLine(person2);
        }
    }
}
