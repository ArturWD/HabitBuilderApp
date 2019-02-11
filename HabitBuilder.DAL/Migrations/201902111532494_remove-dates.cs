namespace HabitBuilder.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DayStatus", "Date_DateId", "dbo.Dates");
            DropIndex("dbo.DayStatus", new[] { "Date_DateId" });
            AddColumn("dbo.DayStatus", "StatusDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.DayStatus", "Date_DateId");
            DropTable("dbo.Dates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Dates",
                c => new
                    {
                        DateId = c.Int(nullable: false, identity: true),
                        DateDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DateId);
            
            AddColumn("dbo.DayStatus", "Date_DateId", c => c.Int());
            DropColumn("dbo.DayStatus", "StatusDate");
            CreateIndex("dbo.DayStatus", "Date_DateId");
            AddForeignKey("dbo.DayStatus", "Date_DateId", "dbo.Dates", "DateId");
        }
    }
}
