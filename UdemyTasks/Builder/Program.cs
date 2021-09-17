using System;
using System.Text;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }

    public class CodeBuilder
    {
        private string _root;
        private StringBuilder _stringBuilder;

        private int _indentSize = 2;

        public CodeBuilder(string root)
        {
            _root = root;
            _stringBuilder = new StringBuilder();
            _stringBuilder.AppendLine($"public class {_root}");
            _stringBuilder.AppendLine("{");
        }

        public CodeBuilder AddField(string varName, string varType)
        {
            _stringBuilder.AppendLine(new string(' ', _indentSize) + $"public {varType} {varName};");
            return this;
        }

        public override string ToString()
        {
            _stringBuilder.AppendLine("}");
            return _stringBuilder.ToString();
        }
    }
}
