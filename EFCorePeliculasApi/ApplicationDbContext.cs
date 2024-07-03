using EFCorePeliculasApi.Entidades;
using EFCorePeliculasApi.Entidades.Configuraciones;
using EFCorePeliculasApi.Entidades.Funciones;
using EFCorePeliculasApi.Entidades.Seeding;
using EFCorePeliculasApi.Entidades.SinLlaves;
using EFCorePeliculasApi.Servicios;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace EFCorePeliculasApi
{
	public class ApplicationDbContext : DbContext
	{
		private readonly IServicioUsuario servicioUsuario;

		/*
		 si no queremos usar inyeccion de dependencia para el dbcontext
		 */
		public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions options,
			/*
			 implementando inyeccion de dependencia en el dbcontext
			para implementacion de la interfaz de usuarios
			 */
			IServicioUsuario servicioUsuario
			/*
			 implementando inyeccion de dependencia para los 
			eventos de tracked y statechanged
			 */
			,IEventosDbContext eventosDbContext
			
			) : base(options)
		{
			this.servicioUsuario = servicioUsuario;

			/*
			 si no es nulo los eventos, pero estos eventos se aplican
			cuando se usa el Tracking
			 */
			if (eventosDbContext is not null)
			{
				ChangeTracker.Tracked += eventosDbContext.ManejarTracked;
				ChangeTracker.StateChanged += eventosDbContext.ManejarStateChanged;
				/*
				 manejo de eventos del SaveChanges
				 */
				SavingChanges += eventosDbContext.ManejarSavingChanges;
				SavedChanges += eventosDbContext.ManejarSavedChanges;
				SaveChangesFailed += eventosDbContext.ManejarSaveChangesFailed;
			}
		}

		/*
		 sobrescribiendo el SaveChanges - token, que es el utilizamos
		 */
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			//llamamos al procedimiento private
			ProcesarSalvado();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void ProcesarSalvado()
		{
			/*
			 hacemos uso de ChangeTracker, el seguidor de cambios
			 */
			foreach (var item in ChangeTracker
				//todas las entidades
				.Entries()
					//se agrega un pequeño filtro para aquellas con estado de agregadas
					.Where(e=>e.State==EntityState.Added
						//tambien las entidades auditables
						&& e.Entity is EntidadAuditable
					)
				)
			{
				var entidad=item.Entity as EntidadAuditable;

				/*
				 
					entidad.UsuarioCreacion = "Jordan";
					entidad.UsuarioModificacion = "Jordan";
				 configurando inyeccion para obtener el usuario
				 */

				entidad.UsuarioCreacion = servicioUsuario.ObtenerUsuarioId();
				entidad.UsuarioModificacion = servicioUsuario.ObtenerUsuarioId();
			}

			//cuando es modificada
			foreach (var item in ChangeTracker.Entries().Where(e=>e.State==EntityState.Modified
				&& e.Entity is EntidadAuditable))
			{
				var entidad=item.Entity as EntidadAuditable;
				entidad.UsuarioModificacion = servicioUsuario.ObtenerUsuarioId();
				//no queremos que el usuario creacion sea modificado, en ningun concepto
				item.Property(nameof(entidad.UsuarioCreacion)).IsModified = false;
			}

		}

		/*
		 OnConfiguring, nos permite configurar desde aca el dbcontext
		es otra manera de configurarlo en vez desde program.cs
		 */
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			/*
			 realizamos un if, si ya existe una configuracion previa en la clase program
			 */
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(
				/*
				 para no pasar el nombre del connectionString, podemos poner el nombre del
				valor de este, que esta en el json
				 */
				"name=DefaultConnection", op =>
				{
					op.UseNetTopologySuite();
				}).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			}
						
		}

		/*
		 para crear, una convencion por defecto de un tipo de valor para diferentes
		campos, que vayamos a utilizar
		si se desea especificar un tipo para un campo, se puede utilizar, lo cual hace
		que se omita la convencion creada acá
		 */
		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.Properties<DateTime>().HaveColumnType("date");
		}

		/*
		 api fuente, se crea en OnModelCreating, para las primary y otras opciones
		hay cosas que aca se puede configurar mas que con atributos
		 */
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			/*
			aca estamos diciendo cual campo es la llave primaria
			modelBuilder.Entity<Genero>().HasKey(g => g.Identificador);
			
			 podemos configurar aca los limites de los campos
			con .Property y .HasMaxLength()
			 
			modelBuilder.Entity<Genero>().Property(g => g.Nombre)
					podemos configurar mas de una propiedad
				.HasMaxLength(150)
					para poner que el campo no permita nulos
				.IsRequired()
			
				 podemos ponerle el nombre que queramos
				.HasColumnName("nombreGenero");
			
				;
			
			  para darle otro nombre a la tabla distinta a la clase
			  .ToTable(name: "TablaGeneros", schema: "peliculas")
			modelBuilder.Entity<Genero>().ToTable(name: "TablaGeneros", schema: "peliculas");
			
			
			modelBuilder.Entity<Actor>().Property(a => a.Nombre)
				.IsRequired()
				.HasMaxLength(150);
			
			modelBuilder.Entity<Actor>().Property(a => a.FechaNacimiento)
					para poner el tipo de dato al campo por medio de api fuente
				.HasColumnType("date");
			

			modelBuilder.Entity<Cine>().Property(c => c.Nombre)
				.HasMaxLength(150)
				.IsRequired();
			
			 para campos con el tipo de dato decimal, para ponerle la precision
				.HasPrecision(precision:nNumeroPrecision,scale:nNumeroDecimales);
			ejm:
				modelBuilder.Entity<Cine>().Property(c => c.Precio)
				.HasPrecision(precision:9,scale:2);
			

			modelBuilder.Entity<Pelicula>().Property(p=>p.Titulo)
				.HasMaxLength(250)
				.IsRequired();
			
			modelBuilder.Entity<Pelicula>().Property(p => p.FechaEstreno)
				.HasColumnType("date");
			
			modelBuilder.Entity<Pelicula>().Property(p => p.PosterURL)
				.HasMaxLength(500)
					esto se refiere a los caracteres que vamos a aceptar en este
					campo, esto se hace para ahorrar espacios, ya que los caracteres
					especiales lo consumen, pero si lo ocuparamos, le ponemos true
				.IsUnicode(false);
			
			para relacionar una tabla con otra, para realizar relaciones
			
			modelBuilder.Entity<CineOferta>().Property(co=>co.PorcentajeDescuento)
				.HasPrecision(precision:5,scale:2);
			
			modelBuilder.Entity<CineOferta>().Property(co=>co.FechaInicio)
				.HasColumnType("date");
			modelBuilder.Entity<CineOferta>().Property(co => co.FechaFinal)
				.HasColumnType("date");
		

			modelBuilder.Entity<SalaDeCine>().Property(sc=>sc.Precio)
				.HasPrecision(precision:9,scale:2);
			
			 para cambiar un valor por defecto de un campo int, existe dos tipos
				.HasDefaultValue("nValorPorDefecto"), que le pasa al SQL desde la app el valor
				.HasDefaultValueSQL("expresionSQL"), puedo crear una expresion de SQLServer,
					que genere ese valor por defecto, ejm: GETDATE(), etc
			
			modelBuilder.Entity<SalaDeCine>().Property(sc => sc.TipoSalaDeCine)
				.HasDefaultValue(TiposSalaDeCine.DosDimensiones);

			
			 configuracion de relaciones de muchos a muchos de forma manual
			 configurando las llave primary key compuesta, de forma manual
			
			modelBuilder.Entity<PeliculaActor>().HasKey(
				pa => new
					{
						pa.PeliculaId,
						pa.ActorId
					}
				);
			modelBuilder.Entity<PeliculaActor>().Property(pa => pa.Personaje)
				.HasMaxLength(150);
			*/
			/*
			 para aplicar la configuracion desde la carpeta de config para las entities,
			existen dos maneras: 
				
				modelBuilder.ApplyConfiguration(new nombreClaseDeConfiguracion());
					se usa para cada una de las configuraciones
				
				la otra manera es tomar todas las config desde donde se esta ejecutando desde 
				el assembly, lo que se quiere a dar a entender es que busque y ejecute todas
				las configuraciones que estan en el proyecto y apliquelas con:
					modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			*/
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			/*
			 para saber cuando se usa el dbcontext en una bd en memoria
			para no cargar la dataseeding
			 */
			if (!Database.IsInMemory())
			{
				/*
				 para aplicar la precarga de datos desde el archivo seeding
				*/
				SeedingModuloConsulta.Seed(modelBuilder);
				/*
				 seeding como ejm, para la esplixacion de dos propiedades de navegacion en la misma clase
				o InverseProperty
				 */
				SeedingPersonaMensaje.Seed(modelBuilder);

				/*
				 seeding ejm, para ejecutarlas funciones escalares
				 */
				SeedingFacturas.Seed(modelBuilder);

				/*
				 configuracion de las fn scalares en el archivo de fn's
				 */
				Escalares.RegistrarFunciones(modelBuilder);
			}

			

			/*
			 configurando una fn de tabla 
			 */
			modelBuilder.HasDbFunction(() => PeliculaConConteosFn(0));

			/*
			 configurando una secuencia con su propio esquema
			para evitar conflitos
			 */
			modelBuilder.HasSequence<int>("NumeroFactura", "factura")
				/*
				 si queremos que la secuencia inicie en un valor en especifico
				usamos
					.StartsAt(10_000)
				si queremos que se incremente de un valor a otra valor
					.IncrementsBy(5)
				 */
				;

			/*
			 esto nos permite decirle al EFC, que no genere nada para la primary key
				modelBuilder.Entity<Log>().Property(l=>l.Id).ValueGeneratedNever();
			 */

			/*
			 para ignorar una clase a nivel de dbcontext, para todo el proyecto
			para que no sea migrado a la bd, usamos .ignore<nombreClase>()
				
				modelBuilder.Ignore<Direccion>();
			 */

			/*
			 configuracion de una entidad sin llaves
			para realizar el query arbitrario
			
				modelBuilder.Entity<CineSinUbicacion>()
		
					 .HasNoKey(), nos permite crear entidades sin llaves
					 
						.HasNoKey()
						.ToSqlQuery(
					
							aca metemos el query que ocupamos con el lenguaje de sql server
					
								"SELECT Id, Nombre FROM Cines"
							)
					
					 .ToView(null), se agrega para que no se nos agregue una tabla
					en la bd con el schema de cinesSinUbicacion
					
						.ToView(null);
			*/

			/*
			 usando una clase sin llaves, con un query que usa una vista, creada en la bd
			 
				modelBuilder.Entity<PeliculaConConteos>()
					.HasNoKey()
				
						aca pondremos el nombre de nuestra vista, ya que la utilizaremos
				
					.ToView("PeliculasConConteos");
			*/


			/*
			 automatizando configuraciones con el api fluente,
			aca iteraremos todos los modelos de las entidades
			 */
			foreach (var tipoEntidad in modelBuilder.Model.GetEntityTypes())
			{
				/*
				 tendremos cada una de nuestras entidades de la app
				para poder iterar cada una de las propiedades de nuestras
				entidades
				 */
				foreach (var prop in tipoEntidad.GetProperties())
				{
                    
                    if (
						/*
						 de acuerdo al tipo de dato => ClrType
						*/
						prop.ClrType==typeof(string) &&
						prop.Name.Contains(
							"URL",
							/*
							 aca ignoramos si esta escrito en mayusculas o minusculas
							 */
							StringComparison.CurrentCultureIgnoreCase)
						)
                    {
                        /*
                         .SetIsUnicode(), nos permite guardar las url, sin unicode,
						esto es para guardar con menos espacio dicho campo
                         */
						prop.SetIsUnicode(false);
						/*
						 para ponerle el top de longitud a la propiedad de la column
						para la bd
						 */
						prop.SetMaxLength(500);
                    }
                }

			}
		}

		/*
		 llamando una fn scalar desde el motor de sql server
			si queremos pasar la fn [DbFunction(Name="nombreFn")]
			o la llamamos como se llama la fn en la bd
		 */
		[DbFunction]
		public int FacturaDetalleSuma(int facturaId) => 0;

		/*
		 llamando una fn de tabla desde el motor de sql server,
		todo esto es una plantilla que utiliza EFC, para despues sustituir con
		la fn del sql server
		 */
		public IQueryable<PeliculaConConteos> PeliculaConConteosFn(int peliculaId)
		{
			return FromExpression(()=> PeliculaConConteosFn(peliculaId));
		}

		public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<CineOferta> CinesOfertas { get; set; }
        public DbSet<SalaDeCine> SalasDeCine { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<Log> Logs { get; set; }

        /*
		 se crea este dbset, de la entidad sin llave, para poderlo
		tambien llamar desde api fluente como propiedad
		 */
        public DbSet<CineSinUbicacion> CineSinUbicacion { get; set; }
        public DbSet<PeliculaConConteos> PeliculaConConteos { get; set; }
        /*
		 este es para explicacion del inversePropety
		 */
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        /*
		 dbset de la tecnica division de tablas
		 */
        public DbSet<CineDetalle> CineDetalle { get; set; }
        /*
		 Herencia - Tabla por jerarquia
		 */
        public DbSet<Pago> Pagos { get; set; }
        /*
		 Herencia - Tabla por tipo
		 */
        public DbSet<Producto> Productos { get; set; }
        /*
		 configuracion de begintransaction
		 */
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
