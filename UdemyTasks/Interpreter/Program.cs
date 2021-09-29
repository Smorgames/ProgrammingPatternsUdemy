using System;
using System.Collections.Generic;
using System.Text;

  namespace Coding.Exercise
  {
      class Program
      {
          static void Main(string[] args)
          {
              var ep = new ExpressionProcessor();
              ep.Variables.Add('x', 3);
              ep.Variables.Add('y', 2);
              var input = "1+2+3";
              Console.WriteLine($"{input}={ep.Calculate(input)}");
              input = "1+2+xy";
              Console.WriteLine($"{input}={ep.Calculate(input)}");
              input = "10-2+x";
              Console.WriteLine($"{input}={ep.Calculate(input)}");
          }
      }
      
      public class Token
        {
            public enum Type
            {
                Integer,
                Addition,
                Subtracttion
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
            public Expression Left { get; set; }
            public Expression Right { get; set; }

            public BinaryExpression(Expression left, Expression right)
            {
                Left = left;
                Right = right;
            }

            public BinaryExpression() { }
        }
        
        public class AddExpression : BinaryExpression
        {
            public AddExpression(Expression left, Expression right) : base(left, right) { }

            public AddExpression() : base() { }

            public override int Value { get { return Left.Value + Right.Value; } }
        }
        
        public class SubtractExpression : BinaryExpression
        {
            public SubtractExpression(Expression left, Expression right) : base(left, right) { }
            public SubtractExpression() : base() { }

            public override int Value { get { return Left.Value - Right.Value; } }
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
                        tokens.Add(new Token(Token.Type.Addition, "+"));
                        isCorrectValue = true;
                    }

                    if (input[i] == '-')
                    {
                        tokens.Add(new Token(Token.Type.Subtracttion, "-"));
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
                if (tokens.Count < 3)
                    return 0;
                
                var isInit = false;
                var value = 0;
                var binaryExpressions = new List<Expression>();
                var j = 0;

                for (int i = 1; i < tokens.Count; i+=2)
                {
                    if (!isInit)
                    {
                        var ex = GetBinaryExpressionBasedOnTokenType(tokens[i].MyType);

                        if (ex == null)
                            return 0;
                        
                        ex.Left = new NumberExpression(Convert.ToInt32(tokens[i - 1].Text));
                        ex.Right = new NumberExpression(Convert.ToInt32(tokens[i + 1].Text));
                        binaryExpressions.Add(ex);
                        isInit = true;
                    }
                    else
                    {
                        var ex = GetBinaryExpressionBasedOnTokenType(tokens[i].MyType);
                        
                        if (ex == null)
                            return 0;
                        
                        ex.Left = binaryExpressions[j++];
                        ex.Right = new NumberExpression(Convert.ToInt32(tokens[i + 1].Text));
                        binaryExpressions.Add(ex);

                        if (i == tokens.Count - 2)
                            value = ex.Value;
                    }
                }

                return value;
            }

            private BinaryExpression GetBinaryExpressionBasedOnTokenType(Token.Type type)
            {
                if (type == Token.Type.Addition)
                {
                    var add = new AddExpression();
                    return add;
                }

                if (type == Token.Type.Subtracttion)
                {
                    var sub = new SubtractExpression();
                    return sub;
                }

                return null;
            }

            public int Calculate(string expression)
            {
                var tokens = Lex(expression);
                var value = Parse(tokens);
                return value;
            }
        }
  }
