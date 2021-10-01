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
            Console.WriteLine(ep.Calculate("1"));
        }
    }

    public class Token
    {
        public enum Type
        {
            None,
            Integer,
            Addition,
            Subtraction
        }

        public Type MyType;
        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }

        public override string ToString()
        {
            return $"[{Text}] ";
        }
    }

    public interface IValue
    {
        int Value { get; }
    }

    public class Integer : IValue
    {
        public int Value { get { return Convert.ToInt32(_token.Text); } }

        private Token _token;

        public Integer(Token token)
        {
            if (token.MyType != Token.Type.Integer)
                throw new Exception();
            
            _token = token;
        }
    }

    public class BinaryOperator : IValue
    {
        public enum Type
        {
            Addition,
            Subtraction
        }

        public Type MyType;

        public IValue Left;
        public IValue Right;

        public int Value
        {
            get
            {
                if (MyType == Type.Addition)
                    return Left.Value + Right.Value;
                
                return Left.Value - Right.Value;
            }
        }
    }

    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        private List<Token> Lex(string input)
        {
            var tokens = new List<Token>();
            Token.Type lastTokenType = Token.Type.None;

            for (int i = 0; i < input.Length; i++)
            {
                var isCorrectValue = false;

                if (input[i] == '+')
                {
                    var token = new Token(Token.Type.Addition, "+");
                    tokens.Add(token);
                    isCorrectValue = true;

                    if (lastTokenType == Token.Type.Addition || lastTokenType == Token.Type.Subtraction)
                        return NullTokenList();

                    lastTokenType = Token.Type.Addition;
                }

                if (input[i] == '-')
                {
                    var token = new Token(Token.Type.Subtraction, "-");
                    tokens.Add(token);
                    isCorrectValue = true;

                    if (lastTokenType == Token.Type.Addition || lastTokenType == Token.Type.Subtraction)
                        return NullTokenList();

                    lastTokenType = Token.Type.Subtraction;
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
                    var token = new Token(Token.Type.Integer, text);

                    tokens.Add(token);
                    isCorrectValue = true;

                    if (lastTokenType == Token.Type.Integer)
                        return NullTokenList();

                    lastTokenType = Token.Type.Integer;
                }

                if (Variables.TryGetValue(input[i], out var number))
                {
                    var token = new Token(Token.Type.Integer, number.ToString());
                    tokens.Add(token);
                    isCorrectValue = true;

                    if (lastTokenType == Token.Type.Integer)
                        return NullTokenList();

                    lastTokenType = Token.Type.Integer;
                }

                if (!isCorrectValue)
                    return NullTokenList();
            }

            if (tokens.Count == 2)
                return NullTokenList();
            
            return tokens;
        }

        private List<Token> NullTokenList()
        {
            return new List<Token>() { new Token(Token.Type.Integer, "0")};
        }

        private int Parse(List<Token> tokens)
        {
            if (tokens.Count == 1)
            {
                if (tokens[0].Text == "0")
                    return 0;

                var value = Convert.ToInt32(tokens[0].Text);
                return value;
            }

            var operators = new List<IValue>();
            var j = 0;
            
            for (int i = 1; i < tokens.Count; i += 2)
            {
                var token = tokens[i];
                
                var operatorType = GetBinaryOperatorTypeBasedOnTokenType(token);
                var op = new BinaryOperator() { MyType = operatorType };
                
                var rightValue = new Integer(tokens[i + 1]);
                IValue leftValue;

                if (i == 1)
                    leftValue = new Integer(tokens[i - 1]);
                else
                    leftValue = operators[j - 1];
                
                op.Left = leftValue;
                op.Right = rightValue;
                
                operators.Add(op);

                j++;
            }

            var lastElementIndex = operators.Count - 1;
            return operators[lastElementIndex].Value;
        }

        private BinaryOperator.Type GetBinaryOperatorTypeBasedOnTokenType(Token token)
        {
            if (token.MyType == Token.Type.Addition) return BinaryOperator.Type.Addition; 
            if (token.MyType == Token.Type.Subtraction) return BinaryOperator.Type.Subtraction;
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

