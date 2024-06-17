using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.Entidades
{
	public class FacturaDetalle
	{
        /*
         ejm de begintransaction
         */
        public int Id { get; set; }
        public int FacturaId { get; set; }
        [StringLength(150)]
        public string Producto { get; set; }
        [Precision(18,2)]
        public decimal Precio { get; set; }
    }
}
