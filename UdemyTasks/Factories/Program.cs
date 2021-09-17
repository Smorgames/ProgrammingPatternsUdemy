using System;

namespace Factories
{
    class Program
    {
        static void Main(string[] args)
        {
            var personFactory = new PersonFactory();
            var person1 = personFactory.CreatePerson("Ivan"); Console.WriteLine(person1);
            var person2 = personFactory.CreatePerson("John"); Console.WriteLine(person2);
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Person with name {Name} has ID = {Id}";
        }
    }

    public class PersonFactory
    {
        private static int _personCounter = 0;

        public Person CreatePerson(string name)
        {
            var person = new Person() { Name = name, Id = _personCounter++ };
            return person;
        }
    }
}
