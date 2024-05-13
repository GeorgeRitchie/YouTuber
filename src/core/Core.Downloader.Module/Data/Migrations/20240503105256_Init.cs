using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Downloader.Module.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "downloader");

            migrationBuilder.CreateTable(
                name: "scheduled_downloads",
                schema: "downloader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DownloadingType = table.Column<string>(type: "TEXT", nullable: false),
                    Timing_Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timing_Type = table.Column<string>(type: "TEXT", nullable: false),
                    Timing_StartDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Timing_EndDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Timing_StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: true),
                    Timing_EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheduled_downloads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "media_files",
                schema: "downloader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    YouTubeId = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Thumbnail_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Thumbnail_Image = table.Column<byte[]>(type: "BLOB", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_media_files_scheduled_downloads_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "downloader",
                        principalTable: "scheduled_downloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playlists",
                schema: "downloader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    YouTubeId = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorName = table.Column<string>(type: "TEXT", nullable: false),
                    Thumbnail_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Thumbnail_Image = table.Column<byte[]>(type: "BLOB", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_playlists_scheduled_downloads_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "downloader",
                        principalTable: "scheduled_downloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "media_streams",
                schema: "downloader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SizeInBytes = table.Column<double>(type: "REAL", nullable: false),
                    Container = table.Column<string>(type: "TEXT", nullable: false),
                    Quality = table.Column<string>(type: "TEXT", nullable: false),
                    StreamType = table.Column<string>(type: "TEXT", nullable: false),
                    MediaFileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_streams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_media_streams_media_files_MediaFileId",
                        column: x => x.MediaFileId,
                        principalSchema: "downloader",
                        principalTable: "media_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_media_files_ScheduleId",
                schema: "downloader",
                table: "media_files",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_media_streams_MediaFileId",
                schema: "downloader",
                table: "media_streams",
                column: "MediaFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_playlists_ScheduleId",
                schema: "downloader",
                table: "playlists",
                column: "ScheduleId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "media_streams",
                schema: "downloader");

            migrationBuilder.DropTable(
                name: "playlists",
                schema: "downloader");

            migrationBuilder.DropTable(
                name: "media_files",
                schema: "downloader");

            migrationBuilder.DropTable(
                name: "scheduled_downloads",
                schema: "downloader");
        }
    }
}
