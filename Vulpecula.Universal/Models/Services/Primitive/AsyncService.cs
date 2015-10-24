using System.Threading.Tasks;

namespace Vulpecula.Universal.Models.Services.Primitive
{
    public abstract class AsyncService : Service
    {
        public abstract Task StartAsync();

        public override void Start()
        {
            // This method is not called by drivers.
        }
    }
}