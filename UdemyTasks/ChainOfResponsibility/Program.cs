using System;
using System.Collections.Generic;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public abstract class Creature
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

        protected Game _game;

        public Creature(Game game)
        {
            _game = game;
        }
    }

    public class Goblin : Creature
    {
        public Goblin(Game game) : base(game)
        {
            Attack = Defense = 1;
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game)
        {
            Attack = Defense = 3;
        }
    }

    public class GoblinModifier
    {
        protected readonly Game _game;
        protected readonly List<Creature> _creatures;

        public GoblinModifier(Game game, List<Creature> creatures)
        {
            _game = game;
            _creatures = creatures;
        }

        private void Handle()
        {

        }
    }

    public class Game
    {
        public IList<Creature> Creatures;
    }
}
