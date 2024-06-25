using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.DTOs
{
	public class GeneroActualizacionDTO
	{
        /*
		 configuracion de conflito de concurrencia con el modelo desconectado
		en donde se debe enviar el valor original mas el nuevo valor
		 */
        public int Identificador { get; set; }
		[Required]
        public string Nombre { get; set; }
        [Required]
        public string Nombre_Original { get; set; }
    }
}
