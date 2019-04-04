using byte5.EditingLookContentApp.Models;
using Umbraco.Core.Migrations;

namespace byte5.EditingLookContentApp.Migrations
{
    public class MigrationCreateTables : MigrationBase
    {
        public MigrationCreateTables(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!TableExists("EditingLook"))
            {
                Create.Table<EditingLook>().Do();
            }
        }

    }
}