using System.Linq;
using System.Text;

namespace FunctionalCSharp.Core.Parser
{
    public class TermStringBuilder : TermVisitor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();
        private int indent = 0;

        public override Term VisitLambda(LambdaTerm term)
        {
            stringBuilder.AppendLine($"{GetIndentString()}LambdaTerm:");
            VisitSubTerm(term.Ident);
            VisitSubTerm(term.Term);
            return term;
        }

        public override Term VisitLet(LetTerm term)
        {
            stringBuilder.AppendLine($"{GetIndentString()}LetTerm:");
            VisitSubTerm(term.Ident);
            VisitSubTerm(term.Rhs);
            VisitSubTerm(term.Body);
            return term;
        }

        public override Term VisitApp(AppTerm term)
        {
            stringBuilder.AppendLine($"{GetIndentString()}AppTerm:");
            VisitSubTerm(term.Func);
            VisitSubTerm(term.List);
            return term;
        }

        public override Term VisitList(ListTerm term)
        {
            stringBuilder.AppendLine($"{GetIndentString()}ListTerm:");
            foreach (var childTerm in term.Args)
            {
                VisitSubTerm(childTerm);
            }
            return term;
        }

        public override Term VisitVar(VarTerm term)
        {
            stringBuilder.AppendLine($"{GetIndentString()}VarTerm: {term.Ident}");
            return term;
        }

        private void VisitSubTerm(Term term)
        {
            indent++;
            Visit(term);
            indent--;
        }

        private string GetIndentString() => string.Concat(Enumerable.Repeat(' ', indent * 4));

        public override string ToString() => stringBuilder.ToString();
    }
}
