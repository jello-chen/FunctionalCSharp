using System;

namespace FunctionalCSharp.Core
{
    /// <summary>
    /// Combinator
    /// </summary>
    public static class Combinator
    {
        #region Y Combinator
        public static Func<T, TResult> Fix<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f) => x => f(Fix(f))(x);

        public static Func<T1, T2, TResult> Fix<T1, T2, TResult>(Func<Func<T1, T2, TResult>, Func<T1, T2, TResult>> f) => (x, y) => f(Fix(f))(x, y);
        #endregion


        #region SKI Combinator
        public static Func<Func<TResult, TResult>, Func<Func<T, TResult>, T>, Func<T, TResult>, TResult> S<T, TResult>() => (x, y, z) => x(z(y(z)));

        public static Func<T1, Func<T2, T1>> K<T1, T2>() => x => y => x;

        public static Func<T, T> I<T>() => x => x; 
        #endregion
    }
}
