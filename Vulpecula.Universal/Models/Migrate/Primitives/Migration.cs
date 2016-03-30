using System;

namespace Vulpecula.Universal.Models.Migrate.Primitives
{
    public class Migration
    {
        private readonly double _fromVersion;

        private readonly MigrationBase _migrationInstance;
        private readonly double _toVersion;

        public Migration(double fromVersion, double toVersion, MigrationBase migration)
        {
            if (fromVersion >= toVersion)
                throw new InvalidOperationException("fromVersion >= toVersion");
            _fromVersion = fromVersion;
            _toVersion = toVersion;
            _migrationInstance = migration;
        }

        public string Rev => _migrationInstance.GetType()
                                               .Name;

        public bool IsMigrate(double oldVersion, double currentVersion)
        {
            if (_fromVersion >= oldVersion && _fromVersion < currentVersion && _toVersion <= currentVersion)
                return true;
            return false;
        }

        public void Migrate(Configuration configuration)
        {
            if (_migrationInstance.IsNeedReConfigure(_fromVersion, _toVersion))
                _migrationInstance.ReConfigure(configuration);
            _migrationInstance.Migrate(configuration);
        }
    }
}