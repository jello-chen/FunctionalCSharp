using System;

namespace FunctionalCSharp.Core
{
    public interface IMonad<T>
    {
        IMonad<T> Pure(T value);

        T Return();

        IMonad<TResult> Apply<TResult>(IMonad<Func<T, TResult>> funcMonad) where TResult : class;

        IMonad<TResult> Apply<TResult>(IMonad<Func<T, IMonad<TResult>>> funcMonad) where TResult : class;

        IMonad<To> Bind<To>(Func<T, IMonad<To>> func) where To : class;

        IMonad<T> Where(Func<T, bool> predicate);

        IMonad<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : class;

        IMonad<TResult> SelectMany<TResult>(Func<T, IMonad<TResult>> selector) where TResult : class;

        IMonad<TResult> SelectMany<TCollection, TResult>(Func<T, IMonad<TCollection>> collectionSelector, Func<T, TCollection, TResult> selector) where TResult : class;
    }
}
