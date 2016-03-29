using Vulpecula.Universal.Models.Migrate.Primitives;

namespace Vulpecula.Universal.Models.Migrate
{
    public class Migration20160329 : MigrationBase
    {
        #region Overrides of MigrationBase

        public override bool IsNeedReConfigure(double oldVersion, double newVersion)
        {
            return false;
            // throw new NotImplementedException();
        }

        public override void Migrate(Configuration configuration)
        {
            // Broken changes. Reset configurations
            configuration.RemoveValue(ConfigurationKeys.ColumnsKey);
        }

        #endregion Overrides of MigrationBase
    }
}