using System;
using System.Linq;

namespace FunctionalCSharp.Core.Parser
{
    public abstract class MiniMLParsers<TInput> : CharParsers<TInput>
    {
        public Parser<TInput, char[]> Whitespace;

        public Func<char, Parser<TInput, char>> WsChr;

        public Parser<TInput, string> Id;

        public Parser<TInput, string> Ident;

        public Parser<TInput, string> LetId;

        public Parser<TInput, string> InId;

        public Parser<TInput, Term> Term;

        public Parser<TInput, Term> Term1;

        public Parser<TInput, Term> All;

        public MiniMLParsers()
        {

            Whitespace = Rep(Char(' ').Or(Char('\t').Or(Char('\n')).Or(Char('\r'))));

            WsChr = chr => Whitespace.And(Char(chr));

            Id = from w in Whitespace

                 from c in Char(char.IsLetter)

                 from cs in Rep(Char(char.IsLetterOrDigit))

                 select cs.Aggregate(c.ToString(), (acc, ch) => acc + ch);

            Ident = from s in Id where s != "let" && s != "in" select s;

            LetId = from s in Id where s == "let" select s;

            InId = from s in Id where s == "in" select s;

            Term1 = (from x in Ident

                     select (Term)new VarTerm(x))

                    .Or(

                    (from u1 in WsChr('(')

                     from t in Term

                     from u2 in WsChr(')')

                     select t));

            Term = (from u1 in WsChr('\\')

                    from x in Ident

                    from u2 in WsChr('.')

                    from t in Term

                    select (Term)new LambdaTerm(new VarTerm(x), t))

                    .Or(

                    (from letid in LetId

                     from x in Ident

                     from u1 in WsChr('=')

                     from t in Term

                     from inid in InId

                     from c in Term

                     select (Term)new LetTerm(new VarTerm(x), t, c)))

                    .Or(

                    (from t in Term1

                     from ts in Rep(Term1)

                     select (Term)new AppTerm(t, new ListTerm(ts))));

            All = from t in Term from u in WsChr(';') select t;

        }

    }
}
