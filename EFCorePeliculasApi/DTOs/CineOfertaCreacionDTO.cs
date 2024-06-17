using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.DTOs
{
	public class CineOfertaCreacionDTO
	{
        /*attibuto que permite establecer el rango aceptable*/
        [Range(1,100)]
        public double PorcentajeDescuento { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal {  get; set; }
    }
}
