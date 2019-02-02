namespace HabitBuilderApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userProfile1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
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
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        DayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.DayStatus",
                c => new
                    {
                        DayStatusId = c.Int(nullable: false, identity: true),
                        NoteHeadline = c.String(maxLength: 100),
                        Note = c.String(),
                        Status_StatusId = c.Int(nullable: false),
                        Habit_HabitId = c.Int(),
                    })
                .PrimaryKey(t => t.DayStatusId)
                .ForeignKey("dbo.Statuses", t => t.Status_StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Habits", t => t.Habit_HabitId)
                .Index(t => t.Status_StatusId)
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
                "dbo.DayHabits",
                c => new
                    {
                        Day_DayId = c.Int(nullable: false),
                        Habit_HabitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Day_DayId, t.Habit_HabitId })
                .ForeignKey("dbo.Days", t => t.Day_DayId, cascadeDelete: true)
                .ForeignKey("dbo.Habits", t => t.Habit_HabitId, cascadeDelete: true)
                .Index(t => t.Day_DayId)
                .Index(t => t.Habit_HabitId);
            
            CreateIndex("dbo.AspNetUsers", "UserProfileId");
            AddForeignKey("dbo.AspNetUsers", "UserProfileId", "dbo.UserProfiles", "UserProfileId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Habits", "UserProfile_UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Reasons", "Habit_HabitId", "dbo.Habits");
            DropForeignKey("dbo.DayStatus", "Habit_HabitId", "dbo.Habits");
            DropForeignKey("dbo.DayStatus", "Status_StatusId", "dbo.Statuses");
            DropForeignKey("dbo.DayHabits", "Habit_HabitId", "dbo.Habits");
            DropForeignKey("dbo.DayHabits", "Day_DayId", "dbo.Days");
            DropForeignKey("dbo.Habits", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.DayHabits", new[] { "Habit_HabitId" });
            DropIndex("dbo.DayHabits", new[] { "Day_DayId" });
            DropIndex("dbo.Reasons", new[] { "Habit_HabitId" });
            DropIndex("dbo.DayStatus", new[] { "Habit_HabitId" });
            DropIndex("dbo.DayStatus", new[] { "Status_StatusId" });
            DropIndex("dbo.Habits", new[] { "UserProfile_UserProfileId" });
            DropIndex("dbo.Habits", new[] { "Category_CategoryId" });
            DropIndex("dbo.AspNetUsers", new[] { "UserProfileId" });
            DropTable("dbo.DayHabits");
            DropTable("dbo.Reasons");
            DropTable("dbo.Statuses");
            DropTable("dbo.DayStatus");
            DropTable("dbo.Days");
            DropTable("dbo.Categories");
            DropTable("dbo.Habits");
            DropTable("dbo.UserProfiles");
        }
    }
}
