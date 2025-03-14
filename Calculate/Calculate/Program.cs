using System;
using System.Collections.Generic;

namespace Culculate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            Dictionary<string, ISignature> dict = new Dictionary<string, ISignature>()
            {
                {"+", new Plus()},
                {"-", new Minus()},
                {"*", new Multiplication()},
                {"/", new Division()}
            };

            var parts = input.Split(' ');

            double a = double.Parse(parts[0]);
            double b = double.Parse(parts[2]);

            string op = parts[1];

            Context context = new Context(dict[op]);
            double result = context.ExecuteOperation(a, b);

            Console.WriteLine(result);
        }
    }

    interface ISignature
    {
        double Sign(double a, double b);
    }

    public class Plus : ISignature
    {
        public double Sign(double a, double b) => a + b;
    }

    public class Minus : ISignature
    {
        public double Sign(double a, double b) => a - b;
    }

    public class Multiplication : ISignature
    {
        public double Sign(double a, double b) => a * b;
    }

    public class Division : ISignature
    {
        public double Sign(double a, double b) => a / b;
    }

    class Context
    {
        private ISignature _oper;

        public Context(ISignature oper)
        {
            _oper = oper;
        }

        public ISignature Operation
        {
            set { _oper = value; }
        }

        public double ExecuteOperation(double a, double b) => _oper.Sign(a, b);
    }
}