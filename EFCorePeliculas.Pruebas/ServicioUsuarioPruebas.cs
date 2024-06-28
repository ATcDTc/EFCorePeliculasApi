using EFCorePeliculasApi.Servicios;

namespace EFCorePeliculas.Pruebas
{
	/*
	 testclas, es donde ponemos las pruebas automaticas
	 */
	[TestClass]
	/*
	 se le pone el nombre de la clase a probar con la terminacion prueba o test
	 */
	public class ServicioUsuarioPruebas
	{
		[TestMethod]
		/*
		 importante ponerle un nombre descriptivo a la prueba,
		como el metodo que va aprobar y el fin de la prueba
		 */
		public void ObtenerUsuarioId_NoTraeValorVacioONulo()
		{
			//preparacion de la prueba
			var servicioUsuario=new ServicioUsuario();

			//prueba
			var resultado = servicioUsuario.ObtenerUsuarioId();

			/*
			verificacion - esperar que el software se comporte de la manera que se espera que lo haga
			con Assert, nos permite realizar las pruebas correspondientes, de diferentes maneras
			*/
			Assert.AreNotEqual("",resultado);//si no son iguales
			Assert.IsNotNull(servicioUsuario);//si no es null


		}
	}
}