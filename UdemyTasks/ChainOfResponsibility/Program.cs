using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Creatures.Add(new Goblin(game));
            game.Creatures.Add(new Goblin(game));
            game.Creatures.Add(new Goblin(game));
            game.Creatures.Add(new GoblinKing(game));
            game.Creatures.Add(new Goblin(game));
            game.Creatures.RemoveAt(1);
            Console.WriteLine(game);
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

        private int _goblinKingCount;

        public Creature this[int index] 
        {
            get { return _goblins[index]; }
            set { _goblins[index] = value; }
        }

        public int Count => _goblins.Count;

        public bool IsReadOnly => false;

        public void Add(Creature item)
        {
            _goblins.Add(item);
            HandleGoblins(item, ListOperation.Add);
        }

        public bool Remove(Creature item)
        {
            var remove = _goblins.Remove(item);

            if (remove)
                HandleGoblins(item, ListOperation.Remove);

            return remove;
        }

        private void HandleGoblins(Creature item, ListOperation operation)
        {

            if (item is GoblinKing)
            {
                switch (operation)
                {
                    case ListOperation.Add:
                        _goblinKingCount++;
                        break;
                    case ListOperation.Remove:
                        _goblinKingCount--;
                        break;
                }
            }

            HandleGoblins();
        }

        private void HandleGoblins()
        {
            foreach (var goblin in _goblins)
            {
                if (goblin is GoblinKing)
                    continue;

                if (goblin is Goblin)
                {
                    goblin.Attack = 1 + _goblinKingCount;
                    goblin.Defense = _goblins.Count;
                }
            }
        }

        public void Clear()
        {
            _goblins.Clear();
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

        public void RemoveAt(int index)
        {
            _goblins.RemoveAt(index);
            HandleGoblins();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _goblins.GetEnumerator();
        }
    }

    public enum ListOperation
    {
        Add,
        Remove
    }

    public class Game
    {
        public IList<Creature> Creatures = new GoblinModifier();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("In game:");

            foreach (var goblin in Creatures)
                sb.Append($"{goblin} ");

            return sb.ToString();
        }
    }
}
