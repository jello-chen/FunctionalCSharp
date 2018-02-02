using System;

namespace FunctionalCSharp.Core
{
    public class Optional<T> : IMonad<T> where T : class
    {
        public static Optional<T> None = new Optional<T>();
        private readonly T value;

        private Optional() { }

        public Optional(T value) => this.value = value ?? throw new ArgumentNullException(nameof(value));

        public IMonad<To> Bind<To>(Func<T, IMonad<To>> func) where To : class => value != null ? func(value) : Optional<To>.None;
    }
}
