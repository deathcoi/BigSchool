namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategoryTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CATEGORIES(ID,NAME) VALUES(1,'CONG NGHE PHAN MEM')");
            Sql("INSERT INTO CATEGORIES(ID,NAME) VALUES(2,'LAP TRINH WEB')");
            Sql("INSERT INTO CATEGORIES(ID,NAME) VALUES(3,'KY THUAT LAP TRINH')");
        }
        
        public override void Down()
        {
        }
    }
}
