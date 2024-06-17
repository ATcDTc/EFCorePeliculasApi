using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.DTOs
{
	public class CineCreacionDTO
	{
        [Required]
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set;}
        public CineOfertaCreacionDTO CineOferta { get; set; }
        /*
        para recibir varios salas de cines
        */
        public SalaDeCineCreacionDTO[] SalasDeCine { get; set; }
    }
}
