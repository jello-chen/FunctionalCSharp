using System;
using System.Collections;
using System.Collections.Generic;

namespace FunctionalCSharp.Core
{
    public class Optional<T> : IMonad<T>
    {
        public static Optional<T> None = new Optional<T>();
        private readonly T value;

        private Optional() { }

        public Optional(T value)
        {
            if (IsNull(value))
                throw new ArgumentNullException(nameof(value));
            this.value = value;
        }

        public static implicit operator Optional<T>(T value) => new Optional<T>(value);

        public static implicit operator T(Optional<T> optional) => optional.value;

        public IMonad<T> Pure(T value) => new Optional<T>(value);

        public T Return() => value;

        public IMonad<TResult> Apply<TResult>(IMonad<Func<T, TResult>> funcMonad) => !IsNull(value) ? new Optional<TResult>(funcMonad.Return()(value)) : Optional<TResult>.None;

        public IMonad<TResult> Apply<TResult>(IMonad<Func<T, IMonad<TResult>>> funcMonad) => !IsNull(value) ? funcMonad.Return()(value): Optional<TResult>.None;

        public IMonad<To> Bind<To>(Func<T, IMonad<To>> func) => !IsNull(value) ? func(value) : Optional<To>.None;

        public Optional<T> Join(Optional<Optional<T>> optional) => optional == Optional<Optional<T>>.None ? None : optional.value;

        public IMonad<T3> Combine<T2, T3>(Func<T, T2, T3> func, IMonad<T2> monad) => new Optional<T3>(func(value, monad.Return()));

        public IMonad<T> Where(Func<T, bool> predicate) => !IsNull(value) && predicate(value) ? new Optional<T>(value) : None;

        public IMonad<TResult> Select<TResult>(Func<T, TResult> selector) => !IsNull(value) ? new Optional<TResult>(selector(value)) : Optional<TResult>.None;

        public IMonad<TResult> Select<TResult>(Func<T, IMonad<TResult>> selector) => !IsNull(value) ? selector(value) : Optional<TResult>.None;

        public IMonad<TResult> SelectMany<TResult>(Func<T, IMonad<TResult>> selector) => !IsNull(value) ? selector(value) : Optional<TResult>.None;

        public IMonad<TResult> SelectMany<TCollection, TResult>(Func<T, IMonad<TCollection>> collectionSelector, Func<T, TCollection, TResult> selector)
        {
            if(!IsNull(value))
            {
                TCollection collection = collectionSelector(value).Return();
                if(collection != null)
                {
                    return new Optional<TResult>(selector(value, collection));
                }
            }
            return Optional<TResult>.None;
        }

        private bool IsNull(T value)
        {
            Type type = typeof(T);
            if(!type.IsValueType)
            {
                return value == null;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
