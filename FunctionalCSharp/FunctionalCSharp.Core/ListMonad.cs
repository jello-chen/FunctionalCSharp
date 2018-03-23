using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalCSharp.Core
{
    public class ListMonad<T> : List<T>, IMonad<T>
    {
        public IMonad<TResult> Apply<TResult>(IMonad<Func<T, TResult>> funcMonad)
        {
            ListMonad<TResult> results = new ListMonad<TResult>();
            foreach (Func<T, TResult> func in funcMonad)
            {
                if(func != null)
                {
                    foreach (T item in this)
                    {
                        results.Add(func(item));
                    }
                }
            }
            return results;
        }

        public IMonad<TResult> Apply<TResult>(IMonad<Func<T, IMonad<TResult>>> funcMonad)
        {
            ListMonad<TResult> results = new ListMonad<TResult>();
            foreach (Func<T, IMonad<TResult>> func in funcMonad)
            {
                if(func != null)
                {
                    foreach (var e1 in this)
                    {
                        foreach (var e2 in func(e1))
                        {
                            results.Add(e2);
                        }
                    }
                }
            }
            return results;
        }

        public IMonad<To> Bind<To>(Func<T, IMonad<To>> func)
        {
            ListMonad<To> results = new ListMonad<To>();
            foreach (var e1 in this)
            {
                foreach (var e2 in func(e1))
                {
                    results.Add(e2);
                }
            }
            return results;
        }

        public IMonad<T3> Combine<T2, T3>(Func<T, T2, T3> func, IMonad<T2> monad)
        {
            ListMonad<T3> results = new ListMonad<T3>();
            foreach (var e1 in this)
            {
                foreach (var e2 in monad)
                {
                    results.Add(func(e1, e2));
                }
            }
            return results;
        }

        public IMonad<T> Pure(T value)
        {
            ListMonad<T> results = new ListMonad<T>();
            results.Add(value);
            return results;
        }

        public T Return()
        {
            return this.First();
        }

        public IMonad<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            ListMonad<TResult> results = new ListMonad<TResult>();
            foreach (var e1 in this)
            {
                results.Add(selector(e1));
            }
            return results;
        }

        public IMonad<TResult> Select<TResult>(Func<T, IMonad<TResult>> selector)
        {
            ListMonad<TResult> results = new ListMonad<TResult>();
            foreach (var e1 in this)
            {
                foreach (var e2 in selector(e1))
                {
                    results.Add(e2);
                }
            }
            return results;
        }

        public IMonad<TResult> SelectMany<TResult>(Func<T, IMonad<TResult>> selector)
        {
            ListMonad<TResult> results = new ListMonad<TResult>();
            foreach (var e1 in this)
            {
                foreach (var e2 in selector(e1))
                {
                    results.Add(e2);
                }
            }
            return results;
        }

        public IMonad<TResult> SelectMany<TCollection, TResult>(Func<T, IMonad<TCollection>> collectionSelector, Func<T, TCollection, TResult> selector)
        {
            ListMonad<TResult> results = new ListMonad<TResult>();
            foreach (var e1 in this)
            {
                foreach (var e2 in collectionSelector(e1))
                {
                    results.Add(selector(e1, e2));
                }
            }
            return results;
        }

        public IMonad<T> Where(Func<T, bool> predicate)
        {
            ListMonad<T> results = new ListMonad<T>();
            foreach (var item in this)
            {
                if(predicate(item))
                {
                    results.Add(item);
                }
            }
            return results;
        }
    }
}
