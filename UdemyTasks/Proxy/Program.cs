using System;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var rp = new ResponsiblePerson(new Person() { Age = 19 });
            Console.WriteLine(rp.Drive());
            Console.WriteLine(rp.Drink());
            Console.WriteLine(rp.DrinkAndDrive());
        }
    }

    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        public int Age { get => _person.Age; set => _person.Age = value; }

        private Person _person;

        public ResponsiblePerson(Person person)
        {
            _person = person;
        }

        public string Drink()
        {
            if (Age >= 18)
                return _person.Drink();
            else
                return "too young";
        }

        public string Drive()
        {
            if (Age >= 16)
                return _person.Drive();
            else
                return "too young";
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }
    }
}
