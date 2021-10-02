using System;

namespace NullObject
{
    class Program
    {
        static void Main(string[] args)
        {
            var nullLog = new NullLog();
            var a = new Account(nullLog);

            a.SomeOperation();
            a.SomeOperation();
            a.SomeOperation();
            a.SomeOperation();
            a.SomeOperation();
            a.SomeOperation();
        }
    }

    public interface ILog
    {
        // maximum # of elements in the log
        int RecordLimit { get; }

        // number of elements already in the log
        int RecordCount { get; set; }

        // expected to increment RecordCount
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog log;

        public Account(ILog log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");
            if (c + 1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();
        }
    }

    public class NullLog : ILog
    {
        public int RecordLimit { get { return c + 1; } }

        public int RecordCount 
        { 
            get 
            {
                var remainder = ++r % 3;

                if (remainder == 1)
                    return c + 1;
                else
                    return c;
            }

            set { c = value; } 
        }
        private int c = 0;
        private int r = 2;

        public void LogInfo(string message) { }
    }
}
