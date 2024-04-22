using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Data
{
    [NopMigration("2023/09/30 12:00:00", "Nopguru.Nop.Plugins.HomePage.Popup CourseManagement schema", MigrationProcessType.Installation)]
    public class CourseManagementSchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<CourseManagement>();
        }
    }
}
