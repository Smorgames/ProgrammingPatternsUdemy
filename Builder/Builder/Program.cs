using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);

            Console.ReadKey();
        }

        public class CodeBuilder
        {
            private readonly string _rootName;
            private Class _root = new Class();

            public CodeBuilder(string rootName)
            {
                _rootName = rootName;
                _root = new Class(_rootName);
            }

            public CodeBuilder AddField(string fieldName, string fieldType)
            {
                Field field = new Field(fieldName, fieldType);
                _root.Fields.Add(field);
                return this;
            }

            public override string ToString() => _root.ToString();
        }

        public class Class
        {
            public string Name;
            public List<Field> Fields = new List<Field>();

            private StringBuilder _stringBuilder = new StringBuilder();

            public Class()
            {

            }

            public Class(string name)
            {
                Name = name;
            }

            public override string ToString()
            {
                _stringBuilder.Append($"public class {Name}\n" + "{\n");

                foreach (var field in Fields)
                    _stringBuilder.Append(field.ToString());

                _stringBuilder.Append("}");

                return _stringBuilder.ToString();
            }
        }

        public class Field
        {
            public string Type, Name;

            private const int _indentSize = 2;

            public Field(string name, string type)
            {
                Type = type;
                Name = name;
            }

            private string ToStringImpl()
            {
                string indent = new string(' ', _indentSize);

                return indent + $"public {Type} {Name};\n";
            }

            public override string ToString() => ToStringImpl();
        }
    }
}
