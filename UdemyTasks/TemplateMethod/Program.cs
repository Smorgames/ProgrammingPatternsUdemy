using System;

namespace TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var creatures = new Creature[] { new Creature(1, 2), new Creature(1, 3) };
            var tcg = new PermanentCardDamage(creatures);

            Console.WriteLine(tcg.Combat(0, 1));
            Console.WriteLine($"Creature 0: [{creatures[0].Attack}, {creatures[0].Health}]");
            Console.WriteLine($"Creature 1: [{creatures[1].Attack}, {creatures[1].Health}]");

        }
    }

    public class Creature
    {
        public int Attack, Health;

        public Creature(int attack, int health)
        {
            Attack = attack;
            Health = health;
        }
    }

    public abstract class CardGame
    {
        public Creature[] Creatures;

        public CardGame(Creature[] creatures)
        {
            Creatures = creatures;
        }

        // returns -1 if no clear winner (both alive or both dead)
        public int Combat(int creature1, int creature2)
        {
            Creature first = Creatures[creature1];
            Creature second = Creatures[creature2];
            Hit(first, second);
            Hit(second, first);
            bool firstAlive = first.Health > 0;
            bool secondAlive = second.Health > 0;
            if (firstAlive == secondAlive) return -1;
            return firstAlive ? creature1 : creature2;
        }

        protected abstract void Hit(Creature attacker, Creature other);
    }

    public class TemporaryCardDamageGame : CardGame
    {
        public TemporaryCardDamageGame(Creature[] creatures) : base(creatures) { }

        protected override void Hit(Creature attacker, Creature other)
        {
            var startOtherHealth = other.Health;

            other.Health -= attacker.Attack;

            if (other.Health > 0)
                other.Health = startOtherHealth;
        }

    }

    public class PermanentCardDamage : CardGame
    {
        public PermanentCardDamage(Creature[] creatures) : base(creatures) { }

        protected override void Hit(Creature attacker, Creature other)
        {
            other.Health -= attacker.Attack;
        }
    }
}
