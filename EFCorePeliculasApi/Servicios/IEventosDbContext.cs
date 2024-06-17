using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCorePeliculasApi.Servicios
{
	public interface IEventosDbContext
	{
		void ManejarSaveChangesFailed(object sender, SaveChangesFailedEventArgs args);
		void ManejarSavedChanges(object sender, SavedChangesEventArgs args);
		void ManejarSavingChanges(object sender, SavingChangesEventArgs args);
		void ManejarStateChanged(object sender, EntityStateChangedEventArgs args);
		void ManejarTracked(object sender, EntityTrackedEventArgs args);
	}
}
