using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculasApi.Entidades
{
	public class Log
	{
		/*
         para generar propiamente nuestra primary key, usando
        anotaciones de datos
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
        nos permite decirle al EFC, que no genere nada
         */
		
        public Guid Id { get; set; }
        public string Mensaje { get; set; }
        /*
         propiedad de prueba para migrate()
         */
        [MaxLength(10)]
        public string? EjemploMigracion { get; set; }
    }
}
