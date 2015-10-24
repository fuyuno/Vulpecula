using System;

namespace Vulpecula.Universal.Models.Services.Primitive
{
    /// <summary>
    /// </summary>
    public abstract class Service : IDisposable
    {
        public abstract void Dispose();

        public abstract void Start();
    }
}