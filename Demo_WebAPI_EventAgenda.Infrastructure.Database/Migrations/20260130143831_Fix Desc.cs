using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agenda_Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            // ↓ Customisation pour manipuler les données
            migrationBuilder.Sql(

                "UPDATE Agenda_Events SET Desc=Name"

            );


            migrationBuilder.Sql(

                "UPDATE Agenda_Events SET Name= SUBSTRING (Name, 1, 100)"

            );

            // ↑ Customisation

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Agenda_Events",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "Name",
               table: "Agenda_Events",
               type: "nvarchar(250)",
               maxLength: 250,
               nullable: false,
               oldClrType: typeof(string),
               oldType: "nvarchar(100)",
               oldMaxLength: 100);

            // ↓ Costumisation d'annulation de la migration

            migrationBuilder.Sql(
                
                "UPDATE Agenda_Events SET Name = SUBSTRING(Desc, 1, 500)"   
          
            );
            // ↑ Customisation

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Agenda_Events");

           
        }
    }
}
