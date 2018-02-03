using System;

namespace FunctionalCSharp.Core
{
    public class Optional<T> : IMonad<T> where T : class
    {
        public static Optional<T> None = new Optional<T>();
        private readonly T value;

        private Optional() { }

        public Optional(T value) => this.value = value ?? throw new ArgumentNullException(nameof(value));

        public static implicit operator Optional<T>(T value) => new Optional<T>(value);

        public static implicit operator T(Optional<T> optional) => optional.value;

        public IMonad<T> Pure(T value) => new Optional<T>(value);

        public T Return() => value;

        public IMonad<TResult> Apply<TResult>(IMonad<Func<T, TResult>> funcMonad) where TResult : class => value != null ? new Optional<TResult>(funcMonad.Return()(value)) : Optional<TResult>.None;

        public IMonad<TResult> Apply<TResult>(IMonad<Func<T, IMonad<TResult>>> funcMonad) where TResult : class => value != null ? funcMonad.Return()(value): Optional<TResult>.None;

        public IMonad<To> Bind<To>(Func<T, IMonad<To>> func) where To : class => value != null ? func(value) : Optional<To>.None;

        public Optional<T> Join(Optional<Optional<T>> optional) => optional == Optional<Optional<T>>.None ? None : optional.value;

        public IMonad<T> Where(Func<T, bool> predicate) => value != null && predicate(value) ? new Optional<T>(value) : None;

        public IMonad<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : class => value != null ? new Optional<TResult>(selector(value)) : Optional<TResult>.None;

        public IMonad<TResult> SelectMany<TResult>(Func<T, IMonad<TResult>> selector) where TResult : class => value != null ? selector(value) : Optional<TResult>.None;

        public IMonad<TResult> SelectMany<TCollection, TResult>(Func<T, IMonad<TCollection>> collectionSelector, Func<T, TCollection, TResult> selector) where TResult : class
        {
            if(value != null)
            {
                TCollection collection = collectionSelector(value).Return();
                if(collection != null)
                {
                    return new Optional<TResult>(selector(value, collection));
                }
            }
            return Optional<TResult>.None;
        }
    }
}
