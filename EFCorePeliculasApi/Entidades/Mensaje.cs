namespace EFCorePeliculasApi.Entidades
{
	public class Mensaje
	{
        public int Id { get; set; }
        public string Contenido { get; set; }
        /*
         estos campos se configuran para poder utilizar con la
        misma clase, con el fin de usar dos propiedades de 
        navegacion con la misma entidad, sin necesidad de crear
        otras, ya que contienen la misma informacion pero con
        diferente tipo de uso, lo cual para aplicarlo se usa diferentes
        campos con su id y una propiedad del tipo de clase de la entidad
        a utilizar, pero con diferente nombre
         */
        public int EmisorId { get; set; }
        public Persona Emisor { get; set; }
        public int ReceptorId { get; set; }
        public Persona Receptor { get; set; }
    }
}
