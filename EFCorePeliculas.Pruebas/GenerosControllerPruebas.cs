using EFCorePeliculasApi.Controllers;
using EFCorePeliculasApi.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
	/*
	 hereda de la clase base que contiene la config del dbcontext
	 */
	[TestClass]
	public class GenerosControllerPruebas:BasePruebas
	{
		[TestMethod]
		public async Task Post_SiEnvioDosGeneros_AmbosSonInsertados()
		{
			//preparacion
			//le ponemos un nombre unico a la bd
			var nombreDB=Guid.NewGuid().ToString();

			var context1= ConstruirContext(nombreDB);
			var generosController = new GenerosController(context1, mapper: null);
			var generos = new Genero[]
			{
				new Genero()
				{
					Nombre="Genero 1"
				},
				new Genero() {Nombre="Genero 2" }
			};

			//probar
			await generosController.Post(generos);

			//verificacion
			/*
			 es mejor usar un nuevo context, porque nos arriesgamos que en otro modo en memoria
			se encuentre los generos, y nos de la app un falso positivo
			 */
			var context2=ConstruirContext(nombreDB);
			var generosDB = await context2.Generos.ToListAsync();

			Assert.AreEqual(2, generosDB.Count);

			//combrobar que los datos estan en el orden correcto
			var existeGenero1 = generosDB.Any(g => g.Nombre == "Genero 1");
			Assert.IsTrue(existeGenero1,message:"El Genero 1 no fue encontrado");

			var existeGenero2 = generosDB.Any(g => g.Nombre == "Genero 2");
			Assert.IsTrue(existeGenero2,message:"El Genero 2 no fue encontrado");

		}

		/*
		 prueba negativa, que nos permite observar el comportamiento del software
		ante una eventualidad, para ver su comportamiento esperado
		 */
		[TestMethod]
		public async Task Put_SiEnvioUnGeneroConNombreOriginalIncorrecto_UnaExcepcionEsArrojada()
		{
			//preparacion
			var nombreDB=Guid.NewGuid().ToString();
			var context1=ConstruirContext(nombreDB);
			var mapper = ConfigurarAutoMapper();
			//insertando un genero en la db
			var generoPrueba=new Genero() { Nombre="Genero 1" };
			context1.Add(generoPrueba);
			await context1.SaveChangesAsync();

			var context2 = ConstruirContext(nombreDB);
			var generoController=new GenerosController(context2, mapper);

			//prueba y verificacion, a veces se rompe la estructura basica de las pruebas pero es necesario
			await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(()=>
				generoController.PutModeloDesconectado(new EFCorePeliculasApi.DTOs.GeneroActualizacionDTO()
				{
					Identificador=generoPrueba.Identificador,
					Nombre="Genero 2",
					Nombre_Original="Nombre incorrecto"
				})
			);

		}

		[TestMethod]
		public async Task Put_SiEnvioUnGeneroConNombreOriginalCorrecto_EntoncesSeActualizaElGenero()
		{
			//preparacion
			var nombreDb = Guid.NewGuid().ToString();
			var context1 = ConstruirContext(nombreDb);
			var mapper = ConfigurarAutoMapper();
			var generoPrueba = new Genero() { Nombre = "Genero 1" };
			context1.Add(generoPrueba);
			await context1.SaveChangesAsync();

			var context2 = ConstruirContext(nombreDb);
			var controllerGenero = new GenerosController(context2, mapper);

			//prueba
			await controllerGenero.PutModeloDesconectado(new EFCorePeliculasApi.DTOs.GeneroActualizacionDTO()
			{
				Identificador = generoPrueba.Identificador,
				Nombre = "Genero 2",
				Nombre_Original = "Genero 1"
			});

			//verificacion
			var context3=ConstruirContext(nombreDb);
			var generoBD=await context3.Generos.SingleAsync();//porque deberia haber solo uno

			Assert.AreEqual(generoPrueba.Identificador, generoBD.Identificador);
			Assert.AreEqual("Genero 2", generoBD.Nombre);

		}
	}
}
