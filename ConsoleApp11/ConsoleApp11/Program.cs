using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            string pattern = @"(\d+|\+|\-|\*|\/)";
            string[] parts = Regex.Split(input, pattern);
            List<string> list = new List<string>(parts);
            list.RemoveAll(item => string.IsNullOrWhiteSpace(item)); 

            string[] operation = new string[] { "*", "/", "+", "-" };

            Dictionary<string, ISignature> dict = new Dictionary<string, ISignature>()
            {
                {"+", new Plus()},
                {"-", new Minus()},
                {"*", new Multiplication()},
                {"/", new Division()}
            };

            for (int i = 0; i < operation.Length; i++)
            {
                while (list.Contains(operation[i]))
                {
                    var index = list.IndexOf(operation[i]);
                    double a = double.Parse(list[index - 1]);
                    double b = double.Parse(list[index + 1]);
                    Context context = new Context(dict[list[index]]);
                    double result = context.ExecuteOperation(a, b);
                    list.RemoveRange(index - 1, 3);
                    list.Insert(index - 1, result.ToString());
                }
            }

            Console.WriteLine(list[0]);
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