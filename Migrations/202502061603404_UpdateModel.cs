namespace AppGroupe2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Utilisateurs", newName: "Personnes");
            DropForeignKey("dbo.Agenda", "IdMedecin", "dbo.Utilisateurs");
            DropPrimaryKey("dbo.Personnes");
            AddColumn("dbo.Agenda", "DatePlanifier", c => c.DateTime(precision: 0));
            AddColumn("dbo.Agenda", "Titre", c => c.String(unicode: false));
            AddColumn("dbo.Agenda", "HeureDebut", c => c.String(unicode: false));
            AddColumn("dbo.Agenda", "HeureFin", c => c.String(unicode: false));
            AddColumn("dbo.Agenda", "Creaneau", c => c.String(unicode: false));
            AddColumn("dbo.Agenda", "Statut", c => c.String(unicode: false));
            AddColumn("dbo.Personnes", "IDU", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Personnes", "NomPrenom", c => c.String(nullable: false, maxLength: 160, storeType: "nvarchar"));
            AddColumn("dbo.Personnes", "Tel", c => c.String(nullable: false, maxLength: 20, storeType: "nvarchar"));
            AddColumn("dbo.Personnes", "MotDePasse", c => c.String(maxLength: 250, storeType: "nvarchar"));
            AddColumn("dbo.Personnes", "Status", c => c.Boolean());
            AddColumn("dbo.Personnes", "GroupSanguin", c => c.String(maxLength: 3, storeType: "nvarchar"));
            AddColumn("dbo.Personnes", "Poids", c => c.Single());
            AddColumn("dbo.Personnes", "Taille", c => c.Single());
            AddColumn("dbo.Personnes", "TelephoneFixe", c => c.String(maxLength: 15, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "IdUtilisateur", c => c.Int());
            AlterColumn("dbo.Personnes", "Adresse", c => c.String(nullable: false, maxLength: 200, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "NumeroOrdre", c => c.String(maxLength: 10, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "Email", c => c.String(nullable: false, maxLength: 80, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "Identifiant", c => c.String(maxLength: 20, storeType: "nvarchar"));
            AddPrimaryKey("dbo.Personnes", "IDU");
            AddForeignKey("dbo.Agenda", "IdMedecin", "dbo.Personnes", "IDU", cascadeDelete: true);
            DropColumn("dbo.Personnes", "telephone");
            DropColumn("dbo.Personnes", "telephone1");
            DropTable("dbo.Personnes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Personnes",
                c => new
                    {
                        IDU = c.Int(nullable: false, identity: true),
                        NomPrenom = c.String(nullable: false, maxLength: 160, storeType: "nvarchar"),
                        Adress = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        tel = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        GroupSanguin = c.String(maxLength: 3, storeType: "nvarchar"),
                        Poids = c.Single(),
                        Taille = c.Single(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.IDU);
            
            AddColumn("dbo.Personnes", "telephone1", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AddColumn("dbo.Personnes", "telephone", c => c.String(maxLength: 100, storeType: "nvarchar"));
            DropForeignKey("dbo.Agenda", "IdMedecin", "dbo.Personnes");
            DropPrimaryKey("dbo.Personnes");
            AlterColumn("dbo.Personnes", "Identifiant", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "Email", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "NumeroOrdre", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "Adresse", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Personnes", "IdUtilisateur", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Personnes", "TelephoneFixe");
            DropColumn("dbo.Personnes", "Taille");
            DropColumn("dbo.Personnes", "Poids");
            DropColumn("dbo.Personnes", "GroupSanguin");
            DropColumn("dbo.Personnes", "Status");
            DropColumn("dbo.Personnes", "MotDePasse");
            DropColumn("dbo.Personnes", "Tel");
            DropColumn("dbo.Personnes", "NomPrenom");
            DropColumn("dbo.Personnes", "IDU");
            DropColumn("dbo.Agenda", "Statut");
            DropColumn("dbo.Agenda", "Creaneau");
            DropColumn("dbo.Agenda", "HeureFin");
            DropColumn("dbo.Agenda", "HeureDebut");
            DropColumn("dbo.Agenda", "Titre");
            DropColumn("dbo.Agenda", "DatePlanifier");
            AddPrimaryKey("dbo.Personnes", "IdUtilisateur");
            AddForeignKey("dbo.Agenda", "IdMedecin", "dbo.Utilisateurs", "IdUtilisateur", cascadeDelete: true);
            RenameTable(name: "dbo.Personnes", newName: "Utilisateurs");
        }
    }
}
