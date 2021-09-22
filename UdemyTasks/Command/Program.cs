using System;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        public Action TheAction;
        public int Amount;
        public bool Success;
    }

    public class Account
    {
        public int Balance { get; set; }

        public void Process(Command c)
        {
            if (c.TheAction == Command.Action.Deposit)
            {
                Balance += c.Amount;
                c.Success = true;
            }
            if (c.TheAction == Command.Action.Withdraw)
            {
                if (c.Amount <= Balance)
                {
                    Balance -= c.Amount;
                    c.Success = true;
                }
                else
                    c.Success = false;
            }
        }
    }
}
