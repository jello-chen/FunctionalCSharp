using System;

namespace FunctionalCSharp.Core
{
    public interface IMonad<out T>
    {
        IMonad<To> Bind<To>(Func<T, IMonad<To>> func) where To : class;
    }
}
