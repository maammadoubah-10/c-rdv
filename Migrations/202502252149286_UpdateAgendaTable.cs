namespace AppGroupe2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateAgendaTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Agenda", "HeureDebut", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Agenda", "HeureFin", c => c.Time(nullable: false, precision: 7));
        }

        public override void Down()
        {
            AlterColumn("dbo.Agenda", "HeureFin", c => c.String(unicode: false));
            AlterColumn("dbo.Agenda", "HeureDebut", c => c.String(unicode: false));
        }
    }
}
