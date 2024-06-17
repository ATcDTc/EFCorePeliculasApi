using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EFCorePeliculasApi.Entidades
{
	/*
	 implementando nuestra deteccion de cambios personalizada
	por medio de una interfaz, ya que esto es una clase base
	*/
	public class Notificacion : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/*
		 usamos genericos para poderlo usar con cualquier propiedad
		 */
		protected void Set<T> (T valor,
			/*
			 usaremos el campo con propiedades de manera explicita,
			no inplicita, ya que esta tecnico lo requiere

			*/
			ref T campo,
			/*
			[CallerMemberName], nos permite tener el nombre de la propiedad
			en la que se esta trabajando
			 */
			[CallerMemberName] string propiedad=""
			)
		{
			//si los valores no son iguales
			if (!Equals(valor, campo))
			{
				//es un cambio
				campo= valor;
				/*
				 esta es la informacion la cual efc, usara para saber que propiedad
				ha sido cambiada o actualizada
				 */
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
			}
		}
	}
}
