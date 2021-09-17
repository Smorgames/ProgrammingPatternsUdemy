using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dragon = new Dragon(new Bird(), new Lizard(), 1);
            Console.WriteLine(dragon.Crawl());
            Console.WriteLine(dragon.Fly());

            dragon.Age = 5;
            Console.WriteLine(dragon.Crawl());
            Console.WriteLine(dragon.Fly());
            
            dragon.Age = 15;
            Console.WriteLine(dragon.Crawl());
            Console.WriteLine(dragon.Fly());
        }
    }
    
    public class Bird
    {
        public int Age { get; set; }
      
        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }
      
        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon
    {
        public int Age { get => _bird.Age; set { _bird.Age = _lizard.Age = value; }}

        private Bird _bird;
        private Lizard _lizard;

        public Dragon()
        {
            _bird = new Bird();
            _lizard = new Lizard();
            Age = 1;
        }

        public string Fly()
        {
            return _bird.Fly();
        }

        public string Crawl()
        {
            return _lizard.Crawl();
        }
    }
}
