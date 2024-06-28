using EFCorePeliculasApi;
using EFCorePeliculasApi.Servicios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
	internal class BasePruebas
	{
		/*
		 metodo que crea un dbcontext para pruebas
		 */
		protected ApplicationDbContext ConstruirContext(string nombreBD)
		{
			var op=new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(nombreBD).Options;

			var servicioUsuario = new ServicioUsuario();

			var dbcontext=new ApplicationDbContext(op, servicioUsuario,eventosDbContext:null);
			return dbcontext;
		}
	}
}
