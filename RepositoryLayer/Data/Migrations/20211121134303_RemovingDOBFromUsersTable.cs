﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Data.Migrations
{
    public partial class RemovingDOBFromUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
