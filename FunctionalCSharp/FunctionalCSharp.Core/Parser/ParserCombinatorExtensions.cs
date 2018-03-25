namespace FunctionalCSharp.Core.Parser
{
    public static class ParserCombinatorExtensions
    {
        public static Parser<TInput, TValue> Or<TInput, TValue>(
            this Parser<TInput, TValue> parser1,
            Parser<TInput, TValue> parser2) => input => parser1(input) ?? parser2(input);

        public static Parser<TInput, TValue2> And<TInput, TValue, TValue2>(
            this Parser<TInput, TValue> parser1,
            Parser<TInput, TValue2> parser2) => input => parser2(parser1(input).Rest);
    }
}
