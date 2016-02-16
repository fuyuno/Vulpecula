namespace Vulpecula.Universal.Services.Primitive
{
    public abstract class SuspendableService : Service
    {
        public object Tag;

        public abstract void Suspend();
    }
}