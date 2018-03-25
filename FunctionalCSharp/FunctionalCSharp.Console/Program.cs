using FunctionalCSharp.Core;
using FunctionalCSharp.Core.Parser;

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

            // List Monad
            ListMonad<int> listMonad = new ListMonad<int> { 1, 3, 5, 7, 9 };
            System.Console.WriteLine(listMonad.Combine((m1, m2) => m1 + m2, new Optional<int>(2)).Return());

            // Fixed Combinator
            var fac = Combinator.Fix<int, int>(f => x => x <= 1 ? 1 : x * f(x - 1));
            var fib = Combinator.Fix<int, int>(f => x => x <= 1 ? 1 : f(x - 1) + f(x - 2));
            var gcd = Combinator.Fix<int, int, int>(f => (x, y) => y == 0 ? x : f(y, x % y));
            System.Console.WriteLine(fac(5));
            System.Console.WriteLine(fib(5));
            System.Console.WriteLine(gcd(12 , 8));

            // Parser Combinator
            MiniMLParserFromString parser = new MiniMLParserFromString();
            Result<string, Term> result = parser.All(
                        @"let true = \x.\y.x in
                          let false = \x.\y.y in
                          let if = \b.\l.\r.(b l) r in
                          if true then false else true;");
            System.Console.WriteLine(GetTermString(result.Value));

            System.Console.ReadKey();
        }

        private static string GetTermString(Term term)
        {
            TermStringBuilder termStringBuilder = new TermStringBuilder();
            termStringBuilder.Visit(term);
            return termStringBuilder.ToString();
        }
    }

    class Calculator
    {
        public int First { get; set; }
        public int Second { get; set; }
    }
}
