using FunctionalCSharp.Core;

namespace FunctionalCSharp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Combinator
            Optional<Calculator> c1 = new Calculator { First = 1, Second = 1 };
            Optional<Calculator> c2 = new Calculator { First = 2, Second = 2 };
            Optional<Calculator> c3 = new Calculator { First = 3, Second = 3 };

            var q = from i in c1
                    from j in c2
                    from k in c3
                    select (i.First + j.First + k.First) * (i.Second + j.Second + k.Second) + "";
            System.Console.WriteLine(q.Return());

            // Fixed Combinator
            var fac = Combinator.Fix<int, int>(f => x => x <= 1 ? 1 : x * f(x - 1));
            var fib = Combinator.Fix<int, int>(f => x => x <= 1 ? 1 : f(x - 1) + f(x - 2));
            var gcd = Combinator.Fix<int, int, int>(f => (x, y) => y == 0 ? x : f(y, x % y));
            System.Console.WriteLine(fac(5));
            System.Console.WriteLine(fib(5));
            System.Console.WriteLine(gcd(9 , 3));

            System.Console.ReadKey();
        }
    }

    class Calculator
    {
        public int First { get; set; }
        public int Second { get; set; }
    }
}
