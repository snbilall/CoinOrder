using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinOrderApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProcessType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationPermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<bool>(type: "bit", nullable: false),
                    Sms = table.Column<bool>(type: "bit", nullable: false),
                    PushNotification = table.Column<bool>(type: "bit", nullable: false),
                    CoinOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationPermission_CoinOrder_CoinOrderId",
                        column: x => x.CoinOrderId,
                        principalTable: "CoinOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnqueuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoinOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailMessage_CoinOrder_CoinOrderId",
                        column: x => x.CoinOrderId,
                        principalTable: "CoinOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PushNotificationMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnqueuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoinOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushNotificationMessages_CoinOrder_CoinOrderId",
                        column: x => x.CoinOrderId,
                        principalTable: "CoinOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmsMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnqueuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoinOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsMessage_CoinOrder_CoinOrderId",
                        column: x => x.CoinOrderId,
                        principalTable: "CoinOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinOrder_CreatedDate",
                table: "CoinOrder",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_CoinOrder_DeletedDate",
                table: "CoinOrder",
                column: "DeletedDate");

            migrationBuilder.CreateIndex(
                name: "IX_CoinOrder_Id",
                table: "CoinOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CoinOrder_UserId_DeletedDate",
                table: "CoinOrder",
                columns: new[] { "UserId", "DeletedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationPermission_CoinOrderId",
                table: "CommunicationPermission",
                column: "CoinOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationPermission_DeletedDate",
                table: "CommunicationPermission",
                column: "DeletedDate");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationPermission_Id",
                table: "CommunicationPermission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessage_CoinOrderId",
                table: "EmailMessage",
                column: "CoinOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessage_DeletedDate",
                table: "EmailMessage",
                column: "DeletedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessage_Id",
                table: "EmailMessage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTemplate_DeletedDate",
                table: "MessageTemplate",
                column: "DeletedDate");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTemplate_Id",
                table: "MessageTemplate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationMessages_CoinOrderId",
                table: "PushNotificationMessages",
                column: "CoinOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationMessages_DeletedDate",
                table: "PushNotificationMessages",
                column: "DeletedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationMessages_Id",
                table: "PushNotificationMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SmsMessage_CoinOrderId",
                table: "SmsMessage",
                column: "CoinOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsMessage_DeletedDate",
                table: "SmsMessage",
                column: "DeletedDate");

            migrationBuilder.CreateIndex(
                name: "IX_SmsMessage_Id",
                table: "SmsMessage",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunicationPermission");

            migrationBuilder.DropTable(
                name: "EmailMessage");

            migrationBuilder.DropTable(
                name: "MessageTemplate");

            migrationBuilder.DropTable(
                name: "PushNotificationMessages");

            migrationBuilder.DropTable(
                name: "SmsMessage");

            migrationBuilder.DropTable(
                name: "CoinOrder");
        }
    }
}
