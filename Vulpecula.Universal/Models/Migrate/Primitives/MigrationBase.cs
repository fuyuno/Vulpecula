namespace Vulpecula.Universal.Models.Migrate.Primitives
{
    public abstract class MigrationBase
    {
        public abstract bool IsNeedReConfigure(double oldVersion, double newVersion);

        public virtual void ReConfigure(Configuration configuration)
        {
            // Nothing to do
        }

        public abstract void Migrate(Configuration configuration);
    }
}