using System;
using System.Linq.Expressions;
using System.Threading;

namespace Vulpecula.Streaming.Reactive.Internal
{
    public abstract class ConnectionBase<T, TV, TU> : IDisposable
    {
        protected readonly T Obj;
        protected readonly TV Type;
        protected readonly Expression<Func<string, object>>[] Parameters;
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

        internal abstract void Connection();

        /// <summary>
        /// アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public void Dispose()
        {
            this.TokenSource.Cancel();
        }

        protected void DisposeToken()
        {
            this.TokenSource.Dispose();
            this.TokenSource = null;
            this.Observer.OnCompleted();
            this.Observer = null;
        }
    }
}