using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeSubmissionSimple.Server.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Langauge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeStub = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionId);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_Candidates_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "SubmissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestStatuses",
                columns: table => new
                {
                    TestStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStatuses", x => x.TestStatusId);
                    table.ForeignKey(
                        name: "FK_TestStatuses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestStatuses_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "SubmissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "CodeStub", "Description", "Langauge" },
                values: new object[,]
                {
                    { 1, "def reverse_string():", "Given a string x, reverse and return it", "Python" },
                    { 2, "\r\n            public class Solution{\r\n                public int LastIndexOfOne(string S) {\r\n                \r\n                    return 0;  \r\n                }\r\n            }", "Given a string S consisting only '0's and '1's,  find the last index of the '1' present in it.", "C#" },
                    { 3, "", "Given a database 'Users', write a query to display all the users", "SQL" },
                    { 4, "", "Given a piece of html, change the text to be red using the 'red-card class\r\n<div id=\"firstDiv\" class=\"red-card\">", "CSS" },
                    { 5, "", "Given a piece of html, change the text to be pink using Javascript\r\n<div id=\"firstDiv\">", "JavaScript" },
                    { 6, "function toCelsius(fahrenheit) {\r\n}", "Given a farenheit variable, convert it to Celcius ", "JavaScript" },
                    { 7, "\r\n            public class Solution{\r\n                public int[] MergeArrays(int[] nums1, int[]nums2) {\r\n          \r\n                }\r\n            }", "Merge two sorted arrays and return it as a single array.", "C#" }
                });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "isCompleted" },
                values: new object[] { 1, false });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "CandidateId", "Email", "Name", "SubmissionId", "Surname" },
                values: new object[] { 1, "chux05@hotmail.com", "Promise", 1, "Email" });

            migrationBuilder.InsertData(
                table: "TestStatuses",
                columns: new[] { "TestStatusId", "Code", "QuestionId", "SubmissionId" },
                values: new object[,]
                {
                    { 1, "", 1, 1 },
                    { 2, "", 2, 1 },
                    { 3, "", 6, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_SubmissionId",
                table: "Candidates",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestStatuses_QuestionId",
                table: "TestStatuses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStatuses_SubmissionId",
                table: "TestStatuses",
                column: "SubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "TestStatuses");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Submissions");
        }
    }
}
