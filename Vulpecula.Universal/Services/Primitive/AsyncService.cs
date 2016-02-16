using System;
using System.Threading.Tasks;

namespace Vulpecula.Universal.Services.Primitive
{
    public abstract class AsyncService : IDisposable
    {
        public abstract void Dispose();

        public abstract Task StartAsync();
    }
}