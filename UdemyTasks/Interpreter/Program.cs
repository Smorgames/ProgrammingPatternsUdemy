using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var ep = new ExpressionProcessor();
            ep.Variables.Add('x', 5);
            var lex = ep.Lex("5+46-54+x");

            foreach (var token in lex)
                Console.WriteLine($"{token}");
        }

        public class Token
        {
            public enum Type
            {
                Integer,
                Add,
                Subtract
            }

            public Type MyType;
            public string Text;

            public Token(Type type, string text)
            {
                MyType = type;
                Text = text;
            }

            public override string ToString()
            {
                return $"[{Text}]";
            }
        }
        
        public abstract class Expression
        {
            public abstract int Value { get; }
        }
        
        public class NumberExpression : Expression
        {
            public override int Value { get; }

            public NumberExpression(int value)
            {
                Value = value;
            }
        }
        
        public abstract class BinaryExpression : Expression
        {
            public readonly Expression Left;
            public readonly Expression Right;

            public BinaryExpression(Expression left, Expression right)
            {
                Left = left;
                Right = right;
            }
        }
        
        public class AddExpression : BinaryExpression
        {
            public AddExpression(Expression left, Expression right) : base(left, right) { }

            public override int Value => Left.Value + Right.Value;
        }
        
        public class SubtractExpression : BinaryExpression
        {
            public SubtractExpression(Expression left, Expression right) : base(left, right) { }

            public override int Value => Left.Value + Right.Value;
        }

        public class ExpressionProcessor
        {
            public Dictionary<char, int> Variables = new Dictionary<char, int>();

            public List<Token> Lex(string input)
            {
                var tokens = new List<Token>();

                for (int i = 0; i < input.Length; i++)
                {
                    var isCorrectValue = false;

                    if (input[i] == '+')
                    {
                        tokens.Add(new Token(Token.Type.Add, "+"));
                        isCorrectValue = true;
                    }

                    if (input[i] == '-')
                    {
                        tokens.Add(new Token(Token.Type.Subtract, "-"));
                        isCorrectValue = true;
                    }

                    if (Char.IsDigit(input[i]))
                    {
                        var stringBuilder = new StringBuilder(input[i].ToString());

                        for (int j = i + 1; j < input.Length; j++)
                        {
                            if (Char.IsDigit(input[j]))
                            {
                                stringBuilder.Append(input[j]);
                                i++;
                            }
                            else
                                break;
                        }

                        var text = stringBuilder.ToString();
                        tokens.Add(new Token(Token.Type.Integer, text));
                        isCorrectValue = true;
                    }

                    if (Variables.TryGetValue(input[i], out var number))
                    {
                        tokens.Add(new Token(Token.Type.Integer, number.ToString()));
                        isCorrectValue = true;
                    }

                    if (!isCorrectValue)
                    {
                        var nullTokens = new List<Token>();
                        nullTokens.Add(new Token(Token.Type.Integer, "0"));
                        return nullTokens;
                    }
                }

                return tokens;
            }

            public void Parse(List<Token> tokens)
            {
                var lastNumber = 0;
                var lastOperator = Token.Type.Add;
                
                foreach (var token in tokens)
                {
                    if (token.MyType == Token.Type.Integer)
                    {
                        
                    }
                    
                    if (token.MyType == Token.Type.Add || token.MyType == Token.Type.Subtract)
                    {
                        var add = new AddExpression();
                    }
                }
            }

            public int Calculate(string expression)
            {

                return 0;
            }
        }
    }
}
