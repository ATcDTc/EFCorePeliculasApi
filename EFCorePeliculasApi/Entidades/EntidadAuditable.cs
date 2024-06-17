using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.Entidades
{
	/*
	 clase para sobrescribir, el savechanges
	 */
	public class EntidadAuditable
	{
        [StringLength(150)]
        public string? UsuarioCreacion { get; set; }
		[StringLength(150)]
		public string? UsuarioModificacion { get; set; }
    }
}
