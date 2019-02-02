namespace HabitBuilder.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Dates",
                c => new
                    {
                        DateId = c.Int(nullable: false, identity: true),
                        DateDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DateId);
            
            CreateTable(
                "dbo.DayStatus",
                c => new
                    {
                        DayStatusId = c.Int(nullable: false, identity: true),
                        NoteHeadline = c.String(maxLength: 100),
                        Note = c.String(),
                        Status_StatusId = c.Int(nullable: false),
                        Date_DateId = c.Int(),
                        Habit_HabitId = c.Int(),
                    })
                .PrimaryKey(t => t.DayStatusId)
                .ForeignKey("dbo.Statuses", t => t.Status_StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Dates", t => t.Date_DateId)
                .ForeignKey("dbo.Habits", t => t.Habit_HabitId)
                .Index(t => t.Status_StatusId)
                .Index(t => t.Date_DateId)
                .Index(t => t.Habit_HabitId);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        DayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.Habits",
                c => new
                    {
                        HabitId = c.Int(nullable: false, identity: true),
                        HabitName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        Category_CategoryId = c.Int(),
                        UserProfile_UserProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.HabitId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_UserProfileId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.UserProfile_UserProfileId);
            
            CreateTable(
                "dbo.Reasons",
                c => new
                    {
                        ReasonId = c.Int(nullable: false, identity: true),
                        ReasonText = c.String(maxLength: 100),
                        Habit_HabitId = c.Int(),
                    })
                .PrimaryKey(t => t.ReasonId)
                .ForeignKey("dbo.Habits", t => t.Habit_HabitId)
                .Index(t => t.Habit_HabitId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.HabitDays",
                c => new
                    {
                        Habit_HabitId = c.Int(nullable: false),
                        Day_DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Habit_HabitId, t.Day_DayId })
                .ForeignKey("dbo.Habits", t => t.Habit_HabitId, cascadeDelete: true)
                .ForeignKey("dbo.Days", t => t.Day_DayId, cascadeDelete: true)
                .Index(t => t.Habit_HabitId)
                .Index(t => t.Day_DayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Habits", "UserProfile_UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Reasons", "Habit_HabitId", "dbo.Habits");
            DropForeignKey("dbo.DayStatus", "Habit_HabitId", "dbo.Habits");
            DropForeignKey("dbo.HabitDays", "Day_DayId", "dbo.Days");
            DropForeignKey("dbo.HabitDays", "Habit_HabitId", "dbo.Habits");
            DropForeignKey("dbo.Habits", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.DayStatus", "Date_DateId", "dbo.Dates");
            DropForeignKey("dbo.DayStatus", "Status_StatusId", "dbo.Statuses");
            DropIndex("dbo.HabitDays", new[] { "Day_DayId" });
            DropIndex("dbo.HabitDays", new[] { "Habit_HabitId" });
            DropIndex("dbo.Reasons", new[] { "Habit_HabitId" });
            DropIndex("dbo.Habits", new[] { "UserProfile_UserProfileId" });
            DropIndex("dbo.Habits", new[] { "Category_CategoryId" });
            DropIndex("dbo.DayStatus", new[] { "Habit_HabitId" });
            DropIndex("dbo.DayStatus", new[] { "Date_DateId" });
            DropIndex("dbo.DayStatus", new[] { "Status_StatusId" });
            DropTable("dbo.HabitDays");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Reasons");
            DropTable("dbo.Habits");
            DropTable("dbo.Days");
            DropTable("dbo.Statuses");
            DropTable("dbo.DayStatus");
            DropTable("dbo.Dates");
            DropTable("dbo.Categories");
        }
    }
}
