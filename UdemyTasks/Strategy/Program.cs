using System;
using System.Numerics;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            //var strategy = new OrdinaryDiscriminantStrategy();
            //var solver = new QuadraticEquationSolver(strategy);
            //var results = solver.Solve(1, 10, 16);

            //var strategy = new RealDiscriminantStrategy();
            //var solver = new QuadraticEquationSolver(strategy);
            //var results = solver.Solve(1, 10, 16);

            //var strategy = new OrdinaryDiscriminantStrategy();
            //var solver = new QuadraticEquationSolver(strategy);
            //var results = solver.Solve(1, 4, 5);

            var strategy = new RealDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 4, 5);
            var complexNaN = new Complex(double.NaN, double.NaN);

            Console.WriteLine(results.Item1);
            Console.WriteLine(results.Item2);
        }
    }

    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            return b * b - 4 * a * c;
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var d = b * b - 4 * a * c;

            if (d < 0)
                return Double.NaN;

            return d;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var d = strategy.CalculateDiscriminant(a, b, c);

            if (Double.IsNaN(d))
                return new Tuple<Complex, Complex>(new Complex(double.NaN, double.NaN), new Complex(double.NaN, double.NaN));

            var x1 = (-b + Complex.Sqrt(d)) / (2 * a);
            var x2 = (-b - Complex.Sqrt(d)) / (2 * a);

            return new Tuple<Complex, Complex>(x1, x2);
        }
    }
}
