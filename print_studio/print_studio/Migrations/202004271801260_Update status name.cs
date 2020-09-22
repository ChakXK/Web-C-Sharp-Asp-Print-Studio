namespace print_studio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatestatusname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderStatus", "name", c => c.String(maxLength: 30, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderStatus", "name", c => c.Int());
        }
    }
}
