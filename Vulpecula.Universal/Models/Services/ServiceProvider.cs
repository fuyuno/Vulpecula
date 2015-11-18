using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Vulpecula.Universal.Models.Services.Primitive;

namespace Vulpecula.Universal.Models.Services
{
    public static class ServiceProvider
    {
        private static readonly List<SuspendableService> Service;
        private static readonly Queue<Service> Queue;
        private static readonly Queue<AsyncService> QueueAsync;
        private static readonly object LockObj;

        private static bool _flag1;
        private static bool _flag2;

        private static IDisposable _disposable1;
        private static IDisposable _disposable2;

        public static ReadOnlyCollection<SuspendableService> SuspendableServices => Service.AsReadOnly();

        static ServiceProvider()
        {
            Service = new List<SuspendableService>();
            Queue = new Queue<Service>();
            QueueAsync = new Queue<AsyncService>();
            LockObj = new object();
            _flag1 = false;
            _flag2 = false;

            StartWatchers();
        }

        private static void StartWatchers()
        {
            // 500ms ごとに、 Queue に登録されたサービスを実行。
            _disposable1 = Observable.Interval(TimeSpan.FromMilliseconds(500)).Select(w => _flag1)
                .DistinctUntilChanged()
                .Where(w => w)
                .Repeat()
                .Subscribe(w => WorkQueue());

            _disposable2 = Observable.Interval(TimeSpan.FromMilliseconds(500)).Select(w => _flag2)
                .DistinctUntilChanged()
                .Where(w => w)
                .Repeat()
                .Subscribe(async w => await WorkQueueAsync());
        }

        public static async Task StartService()
        {
            StartWatchers();

            foreach (var service in Service)
                service.Start();
            WorkQueue();
            await WorkQueueAsync();
        }

        public static void SuspendService()
        {
            foreach (var service in Service)
            {
                service.Suspend();
                service.Dispose();
            }
            foreach (var service in Queue)
            {
                service.Dispose();
            }
            foreach (var service in QueueAsync)
            {
                service.Dispose();
            }
            _disposable1.Dispose();
            _disposable2.Dispose();
        }

        /// <summary>
        /// <para>サービスをキューに登録します。</para>
        /// <para>登録されたサービスは、即実行されていきます。</para>
        /// </summary>
        public static void RegisterService(Service service)
        {
            Queue.Enqueue(service);
            if (!_flag1)
            {
                _flag1 = true;
            }
        }

        /// <summary>
        /// <para>サービスをキューに登録します。</para>
        /// <para>登録されたサービスは、非同期で実行されていきます。</para>
        /// </summary>
        /// <param name="service"></param>
        public static void RegisterService(AsyncService service)
        {
            QueueAsync.Enqueue(service);
            if (!_flag2)
            {
                _flag2 = true;
            }
        }

        /// <summary>
        /// <para>バックグラウンド動作としてサービスを登録します。</para>
        /// <para>登録されたサービスは即時実行され、アプリケーション終了時まで動作します。</para>
        /// </summary>
        /// <param name="service"></param>
        public static void RegisterService(SuspendableService service)
        {
            service.Start();
            Service.Add(service);
        }

        private static void WorkQueue()
        {
            lock (LockObj)
            {
                if (Queue.Count <= 0)
                    return;

                try
                {
                    var queue = Queue;
                    while (queue.Count > 0)
                    {
                        var service = queue.Dequeue();
                        service.Start();
                        service.Dispose();
                    }
                }
                catch
                {
                    // ignored
                }
                _flag1 = false;
            }
        }

        private static async Task WorkQueueAsync()
        {
            if (QueueAsync.Count <= 0)
                return;

            try
            {
                var quque = QueueAsync;
                while (quque.Count > 0)
                {
                    var service = quque.Dequeue();
                    await service.StartAsync();
                    service.Dispose();
                }
            }
            catch
            {
                // ignored
            }
            _flag2 = false;
        }
    }
}