using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var ep = new ExpressionProcessor();
            ep.Variables.Add('x', 5);

            Console.WriteLine(ep.Calculate("5+31-25-3+4"));
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
            public Expression Parent { get; set; }
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
            public Expression Left { get; set; }
            public Expression Right { get; set; }

            public BinaryExpression(Expression left, Expression right)
            {
                Left = left;
                Right = right;
            }

            public BinaryExpression()
            {
                
            }
        }
        
        public class AddExpression : BinaryExpression
        {
            public AddExpression(Expression left, Expression right) : base(left, right) { }

            public AddExpression() : base() { }

            public override int Value => Left.Value + Right.Value;
        }
        
        public class SubtractExpression : BinaryExpression
        {
            public SubtractExpression(Expression left, Expression right) : base(left, right) { }
            public SubtractExpression() : base() { }

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

            public int Parse(List<Token> tokens)
            {
                var isInit = false;
                var value = 0;
                
                for (int i = 1; i < tokens.Count; i+=2)
                {
                    if (!isInit)
                    {
                        var ex = GetBinaryExpressionBasedOnTokenType(tokens[i].MyType);
                        ex.Left = new NumberExpression(Convert.ToInt32(tokens[i - 1].Text));
                        ex.Right = new NumberExpression(Convert.ToInt32(tokens[i + 1].Text));
                        isInit = true;
                    }
                    else
                    {
                        var ex = GetBinaryExpressionBasedOnTokenType(tokens[i].MyType);
                        ex.Left = new NumberExpression(Convert.ToInt32(tokens[i - 2].Text));
                        ex.Right = new NumberExpression(Convert.ToInt32(tokens[i + 1].Text));

                        if (i == tokens.Count - 2)
                            value = ex.Value;
                    }
                }

                return value;
            }

            private BinaryExpression GetBinaryExpressionBasedOnTokenType(Token.Type type)
            {
                if (type == Token.Type.Add)
                {
                    var add = new AddExpression();
                    return add;
                }

                if (type == Token.Type.Subtract)
                {
                    var sub = new SubtractExpression();
                    return sub;
                }

                throw new Exception();
            }

            public int Calculate(string expression)
            {
                var tokens = Lex(expression);
                var value = Parse(tokens);
                return value;
            }
        }
    }
}
