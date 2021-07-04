namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Lecture_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "Lecture_Id" });
            DropColumn("dbo.Courses", "LectureID");
            RenameColumn(table: "dbo.Courses", name: "Lecture_Id", newName: "LectureID");
            AlterColumn("dbo.Courses", "LectureID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Courses", "LectureID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Courses", "LectureID");
            AddForeignKey("dbo.Courses", "LectureID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "LectureID", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "LectureID" });
            AlterColumn("dbo.Courses", "LectureID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Courses", "LectureID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Courses", name: "LectureID", newName: "Lecture_Id");
            AddColumn("dbo.Courses", "LectureID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "Lecture_Id");
            AddForeignKey("dbo.Courses", "Lecture_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
