using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class funescalarFactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR ALTER FUNCTION FacturaDetalleSuma(@FacturaId INT)
RETURNS INT
AS
BEGIN
	DECLARE @suma INT;

	SET @suma= (
		SELECT SUM(Precio)
		FROM FacturaDetalles
			WHERE FacturaId=@FacturaId
		)

	RETURN @suma
END
GO
");
            migrationBuilder.Sql(@"CREATE OR ALTER FUNCTION FacturaDetallePromedio(@FacturaId INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
	DECLARE @promedio DECIMAL(18,2);

	SELECT @promedio= AVG(Precio)
	FROM FacturaDetalles
		WHERE FacturaId=@FacturaId

	RETURN @promedio
END
GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION dbo.FacturaDetalleSuma");
            migrationBuilder.Sql("DROP FUNCTION dbo.FacturaDetallePromedio");
        }
    }
}
