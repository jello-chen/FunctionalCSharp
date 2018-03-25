namespace FunctionalCSharp.Core.Parser
{
    public abstract class Term
    {
        public virtual TermType TermType { get; }
        public virtual Term Accept(TermVisitor visitor) => visitor.Visit(this);
    }

    public class LambdaTerm : Term
    {
        public override TermType TermType => TermType.Lambda;

        public readonly VarTerm Ident;
        public readonly Term Term;
        public LambdaTerm(VarTerm i, Term t)
        {
            Ident = i;
            Term = t;
        }

        public override Term Accept(TermVisitor visitor)
        {
            return visitor.VisitLambda(this);
        }
    }

    public class LetTerm : Term
    {
        public override TermType TermType => TermType.Let;

        public readonly VarTerm Ident;
        public readonly Term Rhs;
        public Term Body;
        public LetTerm(VarTerm i, Term r, Term b)
        {
            Ident = i;
            Rhs = r;
            Body = b;
        }

        public override Term Accept(TermVisitor visitor)
        {
            return visitor.VisitLet(this);
        }
    }

    public class AppTerm : Term
    {
        public override TermType TermType => TermType.App;

        public readonly Term Func;
        public readonly ListTerm List;
        
        public AppTerm(Term func, ListTerm list)
        {
            Func = func;
            List = list;
        }

        public override Term Accept(TermVisitor visitor)
        {
            return visitor.VisitApp(this);
        }
    }

    public class ListTerm : Term
    {
        public override TermType TermType => TermType.List;

        public readonly Term[] Args;
        public ListTerm(Term[] args)
        {
            Args = args;
        }

        public override Term Accept(TermVisitor visitor)
        {
            return visitor.VisitList(this);
        }
    }

    public class VarTerm : Term
    {
        public override TermType TermType => TermType.Var;

        public readonly string Ident;
        public VarTerm(string ident)
        {
            Ident = ident;
        }

        public override Term Accept(TermVisitor visitor)
        {
            return visitor.VisitVar(this);
        }
    }

    public enum TermType
    {
        Lambda,
        Let,
        App,
        List,
        Var,
    }
}
