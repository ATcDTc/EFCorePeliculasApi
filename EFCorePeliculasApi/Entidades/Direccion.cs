using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculasApi.Entidades
{
	/*
     para que EFC, ignore esta clase y no
    la aplique como una entidad, se usa el 
    atributo 
        [NotMapped]
    esto hace que no importa donde se llegara a
    utilizar, esta no seria implementada en la bd

    para usar la entidad de propiedad, que nos permite
    utilizar las columnas de una tabla, en distintas clases
    para ser utilizada en otras entidades se usa el 
    atributo
        [Owned]

     */
	[Owned]
    public class Direccion
	{
        public string Calle { get; set; }
        public string Provincia { get; set; }
        [Required]
        public string Pais { get; set; }
    }
}
