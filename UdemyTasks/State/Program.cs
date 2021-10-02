using System;
using System.Text;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            var cl = new CombinationLock(new int[] { 1, 5, 3, 4 });

            Console.WriteLine(cl.Status);

            cl.EnterDigit(1);

            Console.WriteLine(cl.Status);

            cl.EnterDigit(5);

            Console.WriteLine(cl.Status);

            cl.EnterDigit(6);

            Console.WriteLine(cl.Status);

            cl.EnterDigit(4);

            Console.WriteLine(cl.Status);

        }
    }

    public class CombinationLock
    {
        public CombinationLock(int[] combination)
        {
            Status = _locked;

            var length = combination.Length;
            _password = new int[length];
            _password = combination;
            _index = 0;
        }

        public string Status;

        private string _locked = "LOCKED";
        private string _open = "OPEN";
        private string _error = "ERROR";

        private int[] _password;
        private int _index = 0;

        public void EnterDigit(int digit)
        {
            if (digit == _password[_index])
            {
                if (_index == _password.Length - 1)
                {
                    Status = _open;
                }
                else
                {
                    var sb = new StringBuilder();

                    for (int i = 0; i <= _index; i++)
                        sb.Append(_password[i]);

                    Status = sb.ToString();

                    _index++;
                }
            }
            else
                Status = _error;
        }
    }
}
