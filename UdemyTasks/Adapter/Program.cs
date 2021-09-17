using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            var square = new Square() { Side = 5 };
            var adapter = new SquareToRectangleAdapter(square);
            Console.WriteLine(adapter.Area());
        }
    }
    
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }
    
    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public int Width { get; }
        public int Height { get; }
        public SquareToRectangleAdapter(Square square)
        {
            Width = Height = square.Side;
        }
    }
}
