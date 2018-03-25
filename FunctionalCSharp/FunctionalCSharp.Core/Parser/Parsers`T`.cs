using System.Linq;

namespace FunctionalCSharp.Core.Parser
{
    public abstract class Parsers<TInput>
    {
        public Parser<TInput, TValue> Succeed<TValue>(TValue value) => 
            input => new Result<TInput, TValue>(value, input);

        public Parser<TInput, TValue[]> Rep<TValue>(Parser<TInput, TValue> parser) =>
            Rep1(parser).Or(Succeed(new TValue[0]));

        public Parser<TInput, TValue[]> Rep1<TValue>(Parser<TInput, TValue> parser)
        {
            return from x in parser
                   from xs in Rep(parser)
                   select (new[] { x }).Concat(xs).ToArray();
        }
    }
}
