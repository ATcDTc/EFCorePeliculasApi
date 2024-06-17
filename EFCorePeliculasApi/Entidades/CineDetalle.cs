using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.Entidades
{
	public class CineDetalle
	{
        /*
         tecnica de division de tablas
         */
        public int Id { get; set; }
        /*
         esta tecnica requiere que por lo menos un campo
        tenga el atributo Required
         */
        [Required]
        public string Historia { get; set; }
        public string? Valores { get; set; }
        public string? Misiones { get; set; }
        public string? CodigoEtica { get; set; }
        public Cine Cine { get; set; }
    }
}
