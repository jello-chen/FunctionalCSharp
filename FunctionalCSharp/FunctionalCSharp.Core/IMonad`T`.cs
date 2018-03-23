using System;
using System.Collections.Generic;

namespace FunctionalCSharp.Core
{
    public interface IMonad<T> : IEnumerable<T>
    {
        IMonad<T> Pure(T value);

        T Return();

        IMonad<TResult> Apply<TResult>(IMonad<Func<T, TResult>> funcMonad);

        IMonad<TResult> Apply<TResult>(IMonad<Func<T, IMonad<TResult>>> funcMonad);

        IMonad<To> Bind<To>(Func<T, IMonad<To>> func);

        IMonad<T3> Combine<T2, T3>(Func<T, T2, T3> func, IMonad<T2> monad);

        IMonad<T> Where(Func<T, bool> predicate);

        IMonad<TResult> Select<TResult>(Func<T, TResult> selector);

        IMonad<TResult> Select<TResult>(Func<T, IMonad<TResult>> selector);

        IMonad<TResult> SelectMany<TResult>(Func<T, IMonad<TResult>> selector);

        IMonad<TResult> SelectMany<TCollection, TResult>(Func<T, IMonad<TCollection>> collectionSelector, Func<T, TCollection, TResult> selector);
    }
}
