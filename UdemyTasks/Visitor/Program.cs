using System;
using System.Text;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var e = new AdditionExpression(
                new MultiplicationExpression(
                    new Value(4),
                    new AdditionExpression( new Value(1), new Value(84))),
                    new Value(5)
                );

            var ep = new ExpressionPrinter();
            ep.Visit(e);

            Console.WriteLine(ep);
        }
    }

    public abstract class Expression
    {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression
    {
        public readonly int TheValue;

        public Value(int value)
        {
            TheValue = value;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class AdditionExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public AdditionExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class MultiplicationExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public MultiplicationExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public abstract class ExpressionVisitor
    {
        public abstract void Visit(Value value);
        public abstract void Visit(AdditionExpression ae);
        public abstract void Visit(MultiplicationExpression me);
    }

    public class ExpressionPrinter : ExpressionVisitor
    {
        private StringBuilder _stringBuilder = new StringBuilder();

        public override void Visit(Value value)
        {
            _stringBuilder.Append($"{value.TheValue}");
        }

        public override void Visit(AdditionExpression ae)
        {
            _stringBuilder.Append("(");
            VisitForExpression(ae.LHS);
            _stringBuilder.Append("+");
            VisitForExpression(ae.RHS);
            _stringBuilder.Append(")");
        }

        public override void Visit(MultiplicationExpression me)
        {
            VisitForExpression(me.LHS);
            _stringBuilder.Append("*");
            VisitForExpression(me.RHS);
        }

        private void VisitForExpression(Expression e)
        {
            if (e is Value)
                Visit(e as Value);
            if (e is AdditionExpression)
                Visit(e as AdditionExpression);
            if (e is MultiplicationExpression)
                Visit(e as MultiplicationExpression);
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }
}
