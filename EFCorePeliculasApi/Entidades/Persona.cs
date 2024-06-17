using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculasApi.Entidades
{
	public class Persona
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
		/*
         propiedad de navegacion
        se especifica a que foreign key, debe corresponder
        cada una de acuerdo al caso, con el atributo
            [InverseProperty("nombrePropiedad")]
        esto nos permite saber cual es cual, con esta propiedad
        para poder decir de que campo corresponde a cada llave
        foranea, lo cual se pone el nombre de la propiedad
        que difere con la otra propiedad que esta en la misma
        clase que hace referencia
         */
		[InverseProperty("Emisor")]
        public List<Mensaje> MensajesEnviados { get; set; }
        [InverseProperty("Receptor")]
        public List<Mensaje> MensajesRecibidos { get; set; }
    }
}
