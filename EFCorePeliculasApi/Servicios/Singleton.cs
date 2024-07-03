using EFCorePeliculasApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Servicios
{
	public class Singleton
	{
		private readonly IServiceProvider serviceProvider;

		/*
		clase de prueba para pasar el dbcontext a singleton
		pero dara un error, ya que este es del tipo de servicio
		scoped de manera normal, entonces lo que se hace
		es configurar de manera artificial un context scoped
		del dbcontext, por medio de
		IServiceProvider
		*/


		public Singleton(IServiceProvider serviceProvider)
        {
			this.serviceProvider = serviceProvider;
		}

		/*
		 instanciar un dbcontext, en un context
		 */
		public async Task<IEnumerable<Genero>> ObtenerGeneros()
		{
			await using (var scope=serviceProvider
				/*
				 aca creamos un un servicio de tipo scoped
				para el context
				 */
				.CreateAsyncScope())
			{
				var context = scope.ServiceProvider
					/*
					 aca creamos nuestro servicio para consumir el dbcontext
					 */
					.GetRequiredService<ApplicationDbContext>();
				return await context.Generos.ToListAsync();

			}
		}

    }
}
