namespace AppGroupe2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agenda",
                c => new
                    {
                        IdAgenda = c.Int(nullable: false, identity: true),
                        DatePlanifier = c.DateTime(precision: 0),
                        Titre = c.String(unicode: false),
                        HeureDebut = c.String(unicode: false),
                        HeureFin = c.String(unicode: false),
                        Creaneau = c.String(unicode: false),
                        Statut = c.String(unicode: false),
                        IdMedecin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAgenda)
                .ForeignKey("dbo.Personnes", t => t.IdMedecin, cascadeDelete: true)
                .Index(t => t.IdMedecin);
            
            CreateTable(
                "dbo.Personnes",
                c => new
                    {
                        IDU = c.Int(nullable: false, identity: true),
                        NomPrenom = c.String(nullable: false, maxLength: 160, storeType: "nvarchar"),
                        Adresse = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        Tel = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        IdUtilisateur = c.Int(),
                        Identifiant = c.String(maxLength: 20, storeType: "nvarchar"),
                        MotDePasse = c.String(maxLength: 250, storeType: "nvarchar"),
                        Status = c.Boolean(),
                        IdMedecin = c.String(unicode: false),
                        Specialite = c.String(maxLength: 100, storeType: "nvarchar"),
                        NumeroOrdre = c.String(maxLength: 10, storeType: "nvarchar"),
                        GroupSanguin = c.String(maxLength: 3, storeType: "nvarchar"),
                        Poids = c.Single(),
                        Taille = c.Single(),
                        TelephoneFixe = c.String(maxLength: 15, storeType: "nvarchar"),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.IDU);
            
            CreateTable(
                "dbo.RendezVous",
                c => new
                    {
                        IdRv = c.Int(nullable: false, identity: true),
                        Agenda_IdAgenda = c.Int(),
                    })
                .PrimaryKey(t => t.IdRv)
                .ForeignKey("dbo.Agenda", t => t.Agenda_IdAgenda)
                .Index(t => t.Agenda_IdAgenda);
            
            CreateTable(
                "dbo.Soins",
                c => new
                    {
                        IdSoin = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.IdSoin);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RendezVous", "Agenda_IdAgenda", "dbo.Agenda");
            DropForeignKey("dbo.Agenda", "IdMedecin", "dbo.Personnes");
            DropIndex("dbo.RendezVous", new[] { "Agenda_IdAgenda" });
            DropIndex("dbo.Agenda", new[] { "IdMedecin" });
            DropTable("dbo.Soins");
            DropTable("dbo.RendezVous");
            DropTable("dbo.Personnes");
            DropTable("dbo.Agenda");
        }
    }
}
