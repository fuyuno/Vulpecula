namespace Vulpecula.Universal.Models.Services.Primitive
{
    public abstract class SuspendableService : Service
    {
        public object Tag;

        public abstract void Suspend();
    }
}