using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCorePeliculasApi.Servicios
{
	public class EventosDbContext:IEventosDbContext
	{
		private readonly ILogger<EventosDbContext> logger;

		/*
control de eventos de tracked y statechanged
usando loger de EFC
*/
		public EventosDbContext(ILogger<EventosDbContext> logger)
        {
			this.logger = logger;
		}

		public void ManejarTracked(object sender, EntityTrackedEventArgs args)
		{
			var mensaje=$"Entidad: {args.Entry.Entity}, estado: {args.Entry.State}";

			logger.LogInformation(mensaje);
		}

		public void ManejarStateChanged(object sender, EntityStateChangedEventArgs args)
		{
			var mensaje = $"Entidad: {args.Entry.Entity}, estado anterior: {args.OldState}, estado nuevo: {args.NewState}";
			logger.LogInformation(mensaje);
		}

		/*
		 configuracion de eventos de savechanges
		 */
		public void ManejarSavingChanges(object sender, SavingChangesEventArgs args) 
		{
			//se traer todas las entidades que se van a guardar
			var entidades = ((ApplicationDbContext)
				//castea el sender
				sender
				).ChangeTracker.Entries();

			foreach (var entry in entidades)
			{
				var mensaje = $"Entidad: {entry.Entity} va a ser {entry.State}";
				logger.LogInformation(mensaje);
			}
		
		}

		public void ManejarSavedChanges(object sender, SavedChangesEventArgs args) 
		{
			var mensaje = $"Fueron procesadas {args.EntitiesSavedCount} entidades";
			logger.LogInformation(mensaje);
		}

		public void ManejarSaveChangesFailed(object sender, SaveChangesFailedEventArgs args)
		{
			logger.LogError(args.Exception, "Error en el SaveChanges");
		}

    }
}
