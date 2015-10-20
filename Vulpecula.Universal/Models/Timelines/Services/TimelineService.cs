using System;

namespace Vulpecula.Universal.Models.Timelines.Services
{
    public abstract class TimelineService : IDisposable
    {
        public abstract void Dispose();

        public abstract void Connection();
    }
}