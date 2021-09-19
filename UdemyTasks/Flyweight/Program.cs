using System;
using System.Text;

namespace Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Sentence("Hello my baby, bitch i'll fuck u");
            s[0].Capitalize = true;
            s[2].Capitalize = true;
            s[5].Capitalize = true;
            Console.WriteLine(s);
        }
    }

    public class Sentence
    {
        private string[] _splittedText;

        public Sentence(string plainText)
        {
            var text = plainText;
            _splittedText = text.Split();
            _wordToken = new WordToken[_splittedText.Length];

            for (int i = 0; i < _wordToken.Length; i++)
                _wordToken[i] = new WordToken();
        }

        public WordToken this[int index] => _wordToken[index];
        private WordToken[] _wordToken;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < _splittedText.Length; i++)
            {
                string wordToAdd;

                if (_wordToken[i].Capitalize)
                {
                    var word = _splittedText[i];
                    var wordStringBuilder = new StringBuilder();

                    for (int j = 0; j < word.Length; j++)
                    {
                        var letter = word[j];
                        wordStringBuilder.Append(Char.ToUpper(letter));
                    }

                    wordToAdd = wordStringBuilder.ToString();
                }
                else
                    wordToAdd = _splittedText[i];

                stringBuilder.Append(i == _splittedText.Length - 1? wordToAdd : $"{wordToAdd} ");
            }

            return stringBuilder.ToString();
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }
}
