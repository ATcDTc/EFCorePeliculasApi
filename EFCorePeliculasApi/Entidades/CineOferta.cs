namespace EFCorePeliculasApi.Entidades
{
	public class CineOferta
	{
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal PorcentajeDescuento { get; set; }
		/*
         con esta hacemos la relacion entre tablas
        si es una a una o muchas a una o muchas a muchas
        
            public int CineId { get; set; }
        para hacer este campo de tipo de relacion opcional
        se pone al final del tipo de dato el signo ? lo 
        cual, hace que no se borre dicho dato, ya que el campo
        es opcional
         */
		public int CineId { get; set; }
    }
}
