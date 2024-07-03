using EFCorePeliculasApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
	[TestClass]
	public class CinesControllerPruebas: BasePruebas
	{
		[TestMethod]
		public async Task Get_MandoLongitudYLatitud_Obtengo2CinesCercanos()
		{
			var latitud = 18.481139;
			var longitud = -69.938950;


			/*
			 la transacciones para que esta prueba no afecte a las otras pruebas
			 */
			using (var context=LocalDbInicializador.GetDbContextLocalDb())
			{
				var mapper = ConfigurarAutoMapper();

				var controller = new CinesController(context, mapper);
				var respuesta = await controller.Get(latitud,longitud);
				//transformamos ese actionresult en object result
				var objectResult= respuesta as ObjectResult;
				var cines = (IEnumerable<object>)objectResult.Value;//casteando a un enumerable de objecto para verificar la cantidad

				Assert.AreEqual(2, cines.Count());

			}

		}
	}
}
