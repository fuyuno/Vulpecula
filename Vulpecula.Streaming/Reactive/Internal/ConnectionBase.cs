﻿using System;
using System.Linq.Expressions;
using System.Threading;

namespace Vulpecula.Streaming.Reactive.Internal
{
    public abstract class ConnectionBase<T, TV, TU> : IDisposable
    {
        protected readonly T Obj;
        protected readonly Expression<Func<string, object>>[] Parameters;
        protected readonly TV Type;
        protected IObserver<TU> Observer;
        protected CancellationTokenSource TokenSource;

        protected ConnectionBase(T obj, TV type, Expression<Func<string, object>>[] parameters, IObserver<TU> observer)
        {
            this.Obj = obj;
            this.Type = type;
            this.Parameters = parameters;
            this.Observer = observer;
            this.TokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public void Dispose()
        {
            this.TokenSource?.Cancel();
        }

        internal abstract void Connection();

        protected void DisposeToken()
        {
            this.TokenSource.Dispose();
            this.TokenSource = null;
            this.Observer.OnCompleted();
            this.Observer = null;
        }
    }
}