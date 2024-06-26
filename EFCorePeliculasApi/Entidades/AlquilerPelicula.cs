﻿namespace EFCorePeliculasApi.Entidades
{
	public class AlquilerPelicula
	{
        public int Id { get; set; }
        public int PeliculaId { get; set; }
        public DateTime FechaFinalAlquiler { get; set; }
        public int PagoId { get; set; }
        public Pago Pago { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
