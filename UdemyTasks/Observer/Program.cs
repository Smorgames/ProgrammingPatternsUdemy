using System;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new Game();

            var r1 = new Rat(g);
            var r2 = new Rat(g);
            var r3 = new Rat(g);

            r1.RatAttack();
            Console.WriteLine();

            r2.Dispose();

            r3.RatAttack();
            Console.WriteLine();
        }
    }

    public class Game
    {
        private List<Rat> _rats = new List<Rat>();

        public void SubscribeRat(Rat rat)
        {
            if (_rats.Contains(rat)) return;

            _rats.Add(rat);
            UpdateRatsAttack();
        }

        public void UnsubscribeRat(Rat rat)
        {
            if (_rats.Contains(rat))
            {
                _rats.Remove(rat);
                UpdateRatsAttack();
            }
        }

        private void UpdateRatsAttack()
        {
            foreach (var r in _rats)
                r.Attack = _rats.Count;
        }
    }

    public class Rat : IDisposable
    {
        public int Attack = 1;

        private Game _game;

        public Rat(Game game)
        {
            _game = game;
            _game.SubscribeRat(this);
        }

        public void RatAttack()
        {
            Console.WriteLine($"Rats hited {Attack} damage");
        }

        public void Dispose()
        {
            _game.UnsubscribeRat(this);
        }
    }
}
