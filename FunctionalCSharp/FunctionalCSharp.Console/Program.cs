using FunctionalCSharp.Core;

namespace FunctionalCSharp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Optional<Calculator> c1 = new Calculator { First = 1, Second = 1 };
            Optional<Calculator> c2 = new Calculator { First = 2, Second = 2 };
            Optional<Calculator> c3 = new Calculator { First = 3, Second = 3 };

            var q = from i in c1
                    from j in c2
                    from k in c3
                    select (i.First + j.First + k.First) * (i.Second + j.Second + k.Second) + "";
            System.Console.WriteLine(q.Return());

            System.Console.ReadKey();
        }
    }

    class Calculator
    {
        public int First { get; set; }
        public int Second { get; set; }
    }
}
