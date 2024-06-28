using EFCorePeliculas.Pruebas.Mocks;
using EFCorePeliculasApi.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
	[TestClass]
	public class ActualizadorObservableCollectionPruebas
	{
		[TestMethod]
		public void Actualizar_SiEntidadesEsVacio_EntoncesTodosLosDTOsPasanAFormarParteDeEntidades()
		{
			//preparacion
			var mapeardo = new Mapeador();
			var actualizadorObersableCollection = new ActualizadorObservableCollection(mapeardo);
			var entidades = new ObservableCollection<ConId>();
			var dtos = new List<ConId>()
			{
				new ConId { Id = 1 },
				new ConId { Id = 2 }
			};

			//PRUEBA
			actualizadorObersableCollection.Actualizar(entidades, dtos);

			//verificacion
			Assert.AreEqual(2,entidades.Count);//que tenga dos entidades
			Assert.AreEqual(1,entidades[0].Id);//que la primera sea uno
			Assert.AreEqual(2, entidades[1].Id);//que la segunda sea dos

		}

		/*
		 se puede hacer mas de una prueba
		 */
		[TestMethod]
		public void Actualizar_SiDTOsEsVacio_EntoncesTodasLasEntidadesSonRemovidas()
		{
			//preparacion
			var mapeardo = new Mapeador();
			var actualizadorObersableCollection = new ActualizadorObservableCollection(mapeardo);
			var entidades= new ObservableCollection<ConId>()
			{
				new ConId { Id = 1 },
				new ConId { Id = 2 }
			};
			var dtos = new List<ConId>();

			//prueba
			actualizadorObersableCollection.Actualizar(entidades,dtos);

			//verificacion
			Assert.AreEqual(0,entidades.Count);

		}

		[TestMethod]
		public void Actualizar_SiDTOyEntidadesTienenLosMismosObjetos_EntoncesLasCantidadesDeLasColeccionesNoSeAlteran()
		{
			//preparar
			var mapeardo = new Mapeador();
			var actualizadorObersableCollection = new ActualizadorObservableCollection(mapeardo);
			var entidades = new ObservableCollection<ConId>()
			{
				new ConId { Id = 1 },
				new ConId { Id = 2 }
			};
			var dtos = new List<ConId>()
			{
				new ConId { Id = 1 },
				new ConId { Id = 2 }
			};

			//prueba
			actualizadorObersableCollection.Actualizar(entidades, dtos);

			//verificar
			Assert.AreEqual(2,entidades.Count);
			Assert.AreEqual(2,dtos.Count);


		}

	}
}
