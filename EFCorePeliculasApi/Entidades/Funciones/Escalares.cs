using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Entidades.Funciones
{
	/*
	 clase estatica, que contiene todas las fn scalares del proyecto
	 */
	public static class Escalares
	{
		/*
		 debemos definir la fn de la bd como una fn definida por el usuario
		 */
		public static void RegistrarFunciones(ModelBuilder modelBuilder)
		{
			//efc toma esto como una plantilla para poderla invocar
			modelBuilder.HasDbFunction(() => FacturaDetallePromedio(0));
		}

		public static decimal FacturaDetallePromedio(int facturaId)
		{
			return 0;
		}
	}
}
