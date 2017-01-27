namespace BankOfMakeBelieve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAccNameToType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Type", c => c.String());
            DropColumn("dbo.Accounts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Name", c => c.String());
            DropColumn("dbo.Accounts", "Type");
        }
    }
}
