namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LectureID = c.Int(nullable: false),
                        Place = c.String(nullable: false, maxLength: 255),
                        dateTime = c.DateTime(nullable: false),
                        Categoryid = c.Byte(nullable: false),
                        Lecture_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Categoryid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Lecture_Id)
                .Index(t => t.Categoryid)
                .Index(t => t.Lecture_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Lecture_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Categoryid", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "Lecture_Id" });
            DropIndex("dbo.Courses", new[] { "Categoryid" });
            DropTable("dbo.Courses");
            DropTable("dbo.Categories");
        }
    }
}
