using System;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            var vectorTriangle = new Triangle(new VectorRenderer());
            var pixelTriangle = new Triangle(new RasterRenderer());
            var vectorSquare = new Square(new VectorRenderer());
            var pixelSquare = new Square(new RasterRenderer());

            Console.WriteLine(vectorTriangle.ToString());
            Console.WriteLine(pixelTriangle.ToString());
            Console.WriteLine(vectorSquare.ToString());
            Console.WriteLine(pixelSquare.ToString());
        }
    }

    public abstract class Shape
    {
        public string Name { get; set; }

        protected IRenderer _iRenderer;
        
        public Shape(IRenderer iRenderer)
        {
            _iRenderer = iRenderer;
        }
        
        public override string ToString() => $"Drawing {Name} {_iRenderer.WhatToRenderAs}";

    }

    public class Square : Shape
    {
        public Square(IRenderer iRenderer) : base(iRenderer)
        {
            Name = "Square";
        }
    }
    
    public class Triangle : Shape
    {
        public Triangle(IRenderer iRenderer) : base(iRenderer)
        {
            Name = "Triangle";
        }
    }

    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs => "as lines";
    }
    
    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "as pixels";
    }
}
