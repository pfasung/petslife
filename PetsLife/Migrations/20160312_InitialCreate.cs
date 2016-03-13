using System.Data.Entity.Migrations;

namespace PetsLife.Migrations
{
    public class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.PetOwners",
                c => new
                {
                    Id = c.Int(nullable: false),
                    FirstName = c.String(nullable: false, maxLength: 20),
                    LastName = c.String(nullable: false, maxLength: 20),
                    EmailAddress = c.String(nullable: false, maxLength: 250)
                })
                .PrimaryKey(t => t.Id);


            CreateTable("dbo.Pets",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 20),
                    BirthDate = c.DateTime(nullable: false),
                    OwnerId = c.Int(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PetOwners", t => t.OwnerId, cascadeDelete: true);

            CreateTable("dbo.PetWalkers",
                c => new
                {
                    Id = c.Int(nullable: false),
                    FirstName = c.String(nullable: false, maxLength: 20),
                    LastName = c.String(nullable: false, maxLength: 20),
                    EmailAddress = c.String(nullable: false, maxLength: 250),
                    PhoneNumber = c.String(nullable: false, maxLength: 60)
                })
                .PrimaryKey(t => t.Id);

            CreateTable("dbo.PetApprovals",
                                c => new
                                {
                                    Id = c.Int(nullable: false),
                                    PetId = c.Int(nullable: false),
                                    WalkerId = c.Int(nullable: false),
                                    DateApproved = c.DateTime(nullable: false)
                                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .ForeignKey("dbo.PetWalkers", t => t.WalkerId, cascadeDelete: true);

        }

        public override void Down()
        {
            DropForeignKey("dbo.PetApprovals", "PetId", "dbo.Pets");
            DropForeignKey("dbo.PetApprovals", "WalkerId", "dbo.PetWalkers");
            DropTable("dbo.PetApprovals");

            DropForeignKey("dbo.Pets", "OwnerId", "dbo.PetOwners");
            DropTable("dbo.Pets");

            DropTable("dbo.PetOwners");
            DropTable("dbo.PetWalkers");
        }
    }
}