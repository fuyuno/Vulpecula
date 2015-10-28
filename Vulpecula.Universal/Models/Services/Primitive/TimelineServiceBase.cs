using System;
using System.Collections.Generic;

namespace Vulpecula.Universal.Models.Services.Primitive
{
    public abstract class TimelineServiceBase<T> : SuspendableService
    {
        public List<Action<T>> Subscribers { get; }

        // ReSharper disable once MemberCanBeProtected.Global
        public CroudiaProvider Provider { get; }

        protected TimelineServiceBase(CroudiaProvider provider)
        {
            this.Subscribers = new List<Action<T>>();
            this.Provider = provider;
        }
    }
}