using System;
using System.Collections.Generic;

namespace Vulpecula.Universal.Models.Services
{
    public abstract class TimelineServiceBase<T> : SuspendableService
    {
        public List<Action<T>> Subscribers { get; }
        protected CroudiaProvider Provider { get; }

        protected TimelineServiceBase(CroudiaProvider provider)
        {
            this.Subscribers = new List<Action<T>>();
            this.Provider = provider;
        }

        public virtual void ReConnect()
        {
            this.Suspend();
            this.Start();
        }
    }
}