using System;
using System.Threading.Tasks;

namespace Vulpecula.Universal.Models.Services.Primitive
{
    public abstract class AsyncService : IDisposable
    {
        public abstract void Dispose();

        public abstract Task StartAsync();
    }
}