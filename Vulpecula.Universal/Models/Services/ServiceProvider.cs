using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vulpecula.Universal.Models.Services
{
    public static class ServiceProvider
    {
        private static readonly List<SuspendableService> Service;
        private static readonly Queue<Service> Queue;
        private static readonly object LockObj;

        public static ReadOnlyCollection<SuspendableService> RegisteredServices => Service.AsReadOnly();

        static ServiceProvider()
        {
            Service = new List<SuspendableService>();
            Queue = new Queue<Service>();
            LockObj = new object();
        }

        public static void StopService()
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
        }

        /// <summary>
        /// <para>サービスをキューに登録します。</para>
        /// <para>登録されたサービスは、順次実行されていきます。</para>
        /// </summary>
        public static void RegisterService(Service service)
        {
            Queue.Enqueue(service);
            WorkQueue();
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
                foreach (var service in Queue)
                {
                    service.Start();
                    service.Dispose();
                }
            }
        }
    }
}