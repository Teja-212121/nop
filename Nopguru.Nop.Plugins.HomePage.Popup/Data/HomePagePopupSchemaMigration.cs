using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nopguru.Nop.Plugins.HomePage.Popup.Domains;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Data
{
    [NopMigration("2022/09/28 12:00:00", "Nopguru.Nop.Plugins.HomePage.Popup schema", MigrationProcessType.Installation)]
    public class HomePagePopupSchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<HomePagePopup>();
        }
    }
}
