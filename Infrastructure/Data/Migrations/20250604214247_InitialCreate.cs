using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories_catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_catalog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "options_response",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    option_question_id = table.Column<long>(type: "bigint", nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    OptionText = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options_response", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "surveys",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    componenthtml = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    componentreact = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Instruction = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatalogOptions_Id = table.Column<int>(type: "integer", nullable: false),
                    CategoriesOptions_Id = table.Column<int>(type: "integer", nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    OptionsResponseId = table.Column<long>(type: "bigint", nullable: true),
                    CategoriesCatalogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_category_options_categories_catalog_CategoriesCatalogId",
                        column: x => x.CategoriesCatalogId,
                        principalTable: "categories_catalog",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_category_options_options_response_OptionsResponseId",
                        column: x => x.OptionsResponseId,
                        principalTable: "options_response",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Survey_Id = table.Column<int>(type: "integer", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    componenthtml = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    componentreact = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    chapter_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    chapter_title = table.Column<string>(type: "text", nullable: false),
                    SurveyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapters", x => x.id);
                    table.ForeignKey(
                        name: "FK_chapters_surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "surveys",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChapterId = table.Column<int>(type: "integer", nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    question_number = table.Column<string>(type: "text", nullable: true),
                    response_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    comment_question = table.Column<string>(type: "text", nullable: true),
                    question_text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_questions_chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    SubQuestion_Id = table.Column<int>(type: "integer", nullable: false),
                    subquestion_number = table.Column<string>(type: "text", nullable: true),
                    comment_subquestion = table.Column<string>(type: "text", nullable: true),
                    subquestiontext = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_questions_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "summaryoptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Survey_Id = table.Column<int>(type: "integer", nullable: false),
                    code_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Question_Id = table.Column<int>(type: "integer", nullable: false),
                    value_rta = table.Column<string>(type: "text", nullable: true),
                    SurveyId = table.Column<int>(type: "integer", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_summaryoptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_summaryoptions_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_summaryoptions_surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "surveys",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "option_questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Option_Id = table.Column<int>(type: "integer", nullable: false),
                    SubQuestion_Id = table.Column<int>(type: "integer", nullable: false),
                    OptionCatalog_Id = table.Column<int>(type: "integer", nullable: false),
                    OptionQuestion_Id = table.Column<int>(type: "integer", nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Updated_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    comment_options = table.Column<string>(type: "text", nullable: true),
                    numberoption = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SubQuestionId = table.Column<int>(type: "integer", nullable: true),
                    CategoriesCatalogId = table.Column<int>(type: "integer", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: true),
                    OptionsResponseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_option_questions_categories_catalog_CategoriesCatalogId",
                        column: x => x.CategoriesCatalogId,
                        principalTable: "categories_catalog",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_option_questions_options_response_OptionsResponseId",
                        column: x => x.OptionsResponseId,
                        principalTable: "options_response",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_option_questions_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_option_questions_sub_questions_SubQuestionId",
                        column: x => x.SubQuestionId,
                        principalTable: "sub_questions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_options_CategoriesCatalogId",
                table: "category_options",
                column: "CategoriesCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_category_options_OptionsResponseId",
                table: "category_options",
                column: "OptionsResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_chapters_SurveyId",
                table: "chapters",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_CategoriesCatalogId",
                table: "option_questions",
                column: "CategoriesCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_OptionsResponseId",
                table: "option_questions",
                column: "OptionsResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_QuestionId",
                table: "option_questions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_SubQuestionId",
                table: "option_questions",
                column: "SubQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_ChapterId",
                table: "questions",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_sub_questions_QuestionId",
                table: "sub_questions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_summaryoptions_QuestionId",
                table: "summaryoptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_summaryoptions_SurveyId",
                table: "summaryoptions",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_options");

            migrationBuilder.DropTable(
                name: "option_questions");

            migrationBuilder.DropTable(
                name: "summaryoptions");

            migrationBuilder.DropTable(
                name: "categories_catalog");

            migrationBuilder.DropTable(
                name: "options_response");

            migrationBuilder.DropTable(
                name: "sub_questions");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "chapters");

            migrationBuilder.DropTable(
                name: "surveys");
        }
    }
}
