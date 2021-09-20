using System;
using System.Collections;
using System.Collections.Generic;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
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

        public override string ToString()
        {
            return $"Goblin [{Attack}; {Defense}]";
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game)
        {
            Attack = Defense = 3;
        }

        public override string ToString()
        {
            return $"Goblin King [{Attack}; {Defense}]";
        }
    }

    public class GoblinModifier : IList<Creature>
    {
        private List<Creature> _goblins = new List<Creature>();

        private int _goblinCount;
        private int _goblinKingCount;

        public Creature this[int index] 
        {
            get => _goblins[index];
            set => _goblins[index] = value; 
        }

        public int Count => _goblins.Count;

        public bool IsReadOnly => false;

        public void Add(Creature item)
        {
            if (item is GoblinKing)
                _goblinKingCount++;
            else if (item is Goblin)
                _goblinCount++;
            
            _goblins.Add(item);

            foreach (var goblin in _goblins)
            {
                if (goblin is GoblinKing)
                    break;

                if (goblin is Goblin)
                {
                    item.Attack = 1 + _goblinKingCount;
                    item.Defense = _goblinCount;
                }
            }
        }

        public void Clear()
        {
            _goblins.Clear();
            _goblinCount = 0;
            _goblinKingCount = 0;
        }

        public bool Contains(Creature item)
        {
            return _goblins.Contains(item);
        }

        public void CopyTo(Creature[] array, int arrayIndex)
        {
            _goblins.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Creature> GetEnumerator()
        {
            return _goblins.GetEnumerator();
        }

        public int IndexOf(Creature item)
        {
            return _goblins.IndexOf(item);
        }

        public void Insert(int index, Creature item)
        {
            _goblins.Insert(index, item);
        }

        public bool Remove(Creature item)
        {
            return _goblins.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _goblins.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _goblins.GetEnumerator();
        }
    }

    public class Game
    {
        public IList<Creature> Creatures = new GoblinModifier();


    }
}
