using System;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = new Line(new Point(1, 6), new Point(4, 3));
            var lineCopy = line.DeepCopy();
            lineCopy.Start = new Point(888, 999);
            lineCopy.End = new Point(357, 1999);

            Console.WriteLine(line);
            Console.WriteLine(lineCopy);
        }
    }

    public class Point
    {
        public int X, Y;

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"point({X}; {Y})";
    }

    public class Line
    {
        public Point Start, End;

        public Line(Line line)
        {
            Start = new Point(line.Start);
            End = new Point(line.End);
        }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Line DeepCopy()
        {
            var copy = new Line(this);
            return copy;
        }

        public override string ToString() => $"Line with start {Start.ToString()} and end {End.ToString()}";
    }
}
