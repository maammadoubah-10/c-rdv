namespace AppGroupe2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomDeTaMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agenda",
                c => new
                    {
                        IdAgenda = c.Int(nullable: false, identity: true),
                        IdMedecin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAgenda)
                .ForeignKey("dbo.Utilisateurs", t => t.IdMedecin, cascadeDelete: true)
                .Index(t => t.IdMedecin);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        IdUtilisateur = c.Int(nullable: false, identity: true),
                        IdMedecin = c.String(unicode: false),
                        specialite = c.String(maxLength: 100, storeType: "nvarchar"),
                        adresse = c.String(maxLength: 100, storeType: "nvarchar"),
                        NumeroOrdre = c.String(maxLength: 100, storeType: "nvarchar"),
                        email = c.String(maxLength: 100, storeType: "nvarchar"),
                        telephone = c.String(maxLength: 100, storeType: "nvarchar"),
                        identifiant = c.String(maxLength: 100, storeType: "nvarchar"),
                        telephone1 = c.String(maxLength: 100, storeType: "nvarchar"),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.IdUtilisateur);
            
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
            DropForeignKey("dbo.Agenda", "IdMedecin", "dbo.Utilisateurs");
            DropIndex("dbo.RendezVous", new[] { "Agenda_IdAgenda" });
            DropIndex("dbo.Agenda", new[] { "IdMedecin" });
            DropTable("dbo.Soins");
            DropTable("dbo.Personnes");
            DropTable("dbo.RendezVous");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Agenda");
        }
    }
}
