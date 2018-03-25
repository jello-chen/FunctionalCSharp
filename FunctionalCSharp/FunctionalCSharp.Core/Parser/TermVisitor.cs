namespace FunctionalCSharp.Core.Parser
{
    public abstract class TermVisitor
    {
        protected TermVisitor() { }

        public virtual Term Visit(Term term)
        {
            if (term == null) return null;
            return term.Accept(this);
        }

        public virtual Term VisitLambda(LambdaTerm term)
        {
            var ident = term.Ident;
            var lambda = term.Term;
            if (ident != null) ident.Accept(this);
            if (lambda != null) lambda.Accept(this);
            return term;
        }

        public virtual Term VisitLet(LetTerm term)
        {
            return term;
        }

        public virtual Term VisitApp(AppTerm term)
        {
            return term;
        }

        public virtual Term VisitList(ListTerm term)
        {
            return term;
        }

        public virtual Term VisitVar(VarTerm term)
        {
            return term;
        }
    }
}
