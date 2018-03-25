using System;

namespace FunctionalCSharp.Core.Parser
{
    public abstract class CharParsers<TInput> : Parsers<TInput>
    {
        public abstract Parser<TInput, char> AnyChar { get; }

        public Parser<TInput, char> Char(char ch)
        {
            return from c in AnyChar
                   where c == ch
                   select c;
        }

        public Parser<TInput, char> Char(Predicate<char> predicate)
        {
            return from c in AnyChar
                   where predicate(c)
                   select c;
        }
    }
}
