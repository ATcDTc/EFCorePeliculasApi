using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Entidades.SinLlaves
{
	/*
	 para crear entidades sin llaves, usamos el atributo
		[Keyless]
	 */
	public class CineSinUbicacion
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
