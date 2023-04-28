using System;
using System.Threading;

namespace ConsoleApp
{
    class Counter
    {
        public event EventHandler<int> NumberGenerated;

        public void GenerateNumbers()
        {
            for (int i = 1; i <= 1000; i++)
            {
                NumberGenerated?.Invoke(this, i);
            }
        }
    }

    class Wait
    {
        private readonly int _targetValue;

        public Wait(int targetValue)
        {
            _targetValue = targetValue;
        }

        public void WaitForValue(object sender, int value)
        {
            if (value == _targetValue)
            {
                Console.WriteLine($"Target value {_targetValue} found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();
            Wait wait200 = new Wait(200);
            Wait wait800 = new Wait(800);

            counter.NumberGenerated += wait200.WaitForValue;
            counter.NumberGenerated += wait800.WaitForValue;

            Thread counterThread = new Thread(counter.GenerateNumbers);
            counterThread.Start();

            Console.WriteLine("Waiting for target values...");

            counterThread.Join();

            Console.WriteLine("Done.");
        }
    }
}
