namespace EFCorePeliculasApi.Entidades
{
	/*
	 con abstract, nadie puede instanciarla, ya que es un pago
	 */
	public abstract class Pago
	{
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public TipoPago TipoPago { get; set; }
    }
}
