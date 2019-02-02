namespace HabitBuilderApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserProfileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserProfileId");
        }
    }
}
