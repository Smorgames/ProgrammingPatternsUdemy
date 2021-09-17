using System;
using System.Runtime.Intrinsics.X86;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Point() { X = 2, Y = 4};
            var p2 = new Point() { X = 5, Y = 3};
            
            var line = new Line() {Start = p1, End = p2};
            var deepCopy = line.DeepCopy();

            deepCopy.Start.X = 4;
            deepCopy.End.Y = 5;
            
            Console.WriteLine($"line: {line.ToString()}");
            Console.WriteLine($"line deep copy: {deepCopy.ToString()}");
        }
    }

    public class Point
    {
        public int X, Y;

        public override string ToString()
        {
            return $"[{X}; {Y}]";
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            var start = new Point() { X = Start.X, Y = Start.Y };
            var end = new Point() { X = End.X, Y = End.Y };
            
            var line = new Line();
            line.Start = start;
            line.End = end;
            
            return line;
        }

        public override string ToString()
        {
            return $"({Start.ToString()}; {End.ToString()})";
        }
    }
}
