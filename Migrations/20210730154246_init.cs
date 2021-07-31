using Microsoft.EntityFrameworkCore.Migrations;

namespace IAccessSearch.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TSearchStrings",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSearchStrings", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "TSearchStrings",
                columns: new[] { "ID", "Content" },
                values: new object[] { "3AE69BBF-52FB42AF-8310-DFAAE0C6296A", "Sample Record for nuget migration" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TSearchStrings");
        }
    }
}
