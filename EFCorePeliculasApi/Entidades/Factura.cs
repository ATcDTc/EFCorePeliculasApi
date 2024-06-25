using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.Entidades
{
	public class Factura
	{
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        /*
         campo para uso de secuencias
         */
        public int NumeroFactura { get; set; }
		/*
         conflito de concurrencia por fila tipo byte
        usando anotaciones de datos 
            [Timestamp]
         */
        public byte[] Version{ get; set; }
    }
}
