using Umbraco.Core.Migrations;

namespace byte5.EditingLookContentApp.Migrations
{
    public class EditingLookMigrationPlan : MigrationPlan
    {
        public EditingLookMigrationPlan() : base("EditingLookContentApp")
        {
            From(string.Empty).To<MigrationCreateTables>("first-migration");
        }

    }
}