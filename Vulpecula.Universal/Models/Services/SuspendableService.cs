namespace Vulpecula.Universal.Models.Services
{
    public abstract class SuspendableService : Service
    {
        public abstract void Suspend();
    }
}