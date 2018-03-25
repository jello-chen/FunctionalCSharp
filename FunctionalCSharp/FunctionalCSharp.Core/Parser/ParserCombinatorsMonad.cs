using System;

namespace FunctionalCSharp.Core.Parser
{
    public static class ParserCombinatorsMonad
    {
        public static Parser<TInput, TValue> Where<TInput, TValue>(
            this Parser<TInput, TValue> parser,
            Func<TValue, bool> predicate)
        {
            return input =>
            {
                var result = parser(input);
                if (result == null || !predicate(result.Value)) return null;
                return result;
            };
        }

        public static Parser<TInput, TValue2> Select<TInput, TValue, TValue2>(
            this Parser<TInput,TValue> parser,
            Func<TValue, TValue2> selector)
        {
            return input =>
            {
                var result = parser(input);
                if (result == null) return null;
                return new Result<TInput, TValue2>(selector(result.Value), result.Rest);
            };
        }

        public static Parser<TInput, TValue2> SelectMany<TInput, TValue, TIntermediate, TValue2>(
            this Parser<TInput, TValue> parser,
            Func<TValue, Parser<TInput, TIntermediate>> selector,
            Func<TValue, TIntermediate, TValue2> projector)
        {
            return input =>
            {
                var result = parser(input);
                if (result == null) return null;
                var value = result.Value;
                var result2 = selector(value)(result.Rest);
                if (result2 == null) return null;
                return new Result<TInput, TValue2>(projector(value, result2.Value), result2.Rest);
            };
        }
    }
}
