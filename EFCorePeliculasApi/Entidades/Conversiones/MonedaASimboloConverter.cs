using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCorePeliculasApi.Entidades.Conversiones
{
	/*
	 esta clase permite la conversion del data enum a tipo de
	simbolo de la moneda seleccionada
	 */
	public class MonedaASimboloConverter:ValueConverter<Moneda,string>
	{
        public MonedaASimboloConverter()
			/*
			 utilizamos :Base, pasando dos metodos personalizados
			que permite mapear de moneda a string y viceversa
			 */
			:base(
				 /*
				  debemos pasar ambas conversiones
				  */
				 valor=>MapeoMonedaString(valor),
				 valor=>MapeoStringMoneda(valor)
				 )
        {
            
        }

		/*
		 mapeamos de moneda enum a string
		 */
		private static string MapeoMonedaString(Moneda valor)
		{
			/*
			 hacemos un swicth para cada caso
			 */
			return valor switch
			{
				Moneda.ColonCostarricense => "₡",
				Moneda.DolarEstadounidense => "$",
				Moneda.Euro => "€",
				Moneda.BTC => "₿",
				/*
				 aca el valor por defecto que es desconocida
					_ => "?"
				 */
				_=>"?"
			};
		}

		/*
		 mapeo de string a la moneda
		 */
		private static Moneda MapeoStringMoneda(string valor)
		{
			return valor switch
			{
				"₡" => Moneda.ColonCostarricense,
				"$" => Moneda.DolarEstadounidense,
				"€" => Moneda.Euro,
				"₿" => Moneda.BTC,
				var x when (x=="?" || string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x)) => Moneda.Desconocida
			};
		}
    }
}
