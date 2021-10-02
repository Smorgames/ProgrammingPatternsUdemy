using System;
using System.Collections.Generic;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            var tm = new TokenMachine();
            var t = new Token(111);

            var m = tm.AddToken(t);

            WriteTokens(tm, m);

            t.Value = 333;

            WriteTokens(tm, m);
        }

        public static void WriteTokens(TokenMachine tokenMachine, Memento memento)
        {
            Console.WriteLine("Token machine:");
            WriteTokens(tokenMachine);
            Console.WriteLine("Memento:");
            WriteTokens(memento);
        }

        public static void WriteTokens(TokenMachine tokenMachine)
        {
            var tokens = tokenMachine.Tokens;

            for (int i = 0; i < tokens.Count; i++)
                Console.WriteLine($"{i}) {tokens[i]}");
        }

        public static void WriteTokens(Memento memento)
        {
            var tokens = memento.Tokens;

            for (int i = 0; i < tokens.Count; i++)
                Console.WriteLine($"{i}) {tokens[i]}");
        }
    }

    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"Token: {Value}";
        }
    }

    public class Memento
    {
        public List<Token> Tokens { get { return _tokens; } }
        private List<Token> _tokens = new List<Token>();

        public Memento(List<Token> Tokens)
        {
            foreach (var token in Tokens)
            {
                var copiedToken = new Token(token.Value);
                _tokens.Add(copiedToken);
            }
        }

        public Memento()
        {

        }
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            var token = new Token(value);
            Tokens.Add(token);
            return new Memento(Tokens);
        }

        public Memento AddToken(Token token)
        {
            if (Tokens.Contains(token)) 
                return new Memento(Tokens);

            Tokens.Add(token);
            return new Memento(Tokens);
        }

        public void Revert(Memento m)
        {
            Tokens.Clear();

            foreach (var token in m.Tokens)
                Tokens.Add(token);
        }
    }
}
