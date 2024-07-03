using EFCorePeliculasApi;
using EFCorePeliculasApi.Servicios;

//using EFCorePeliculasApi.CompiledModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	/*
	 para evitar los errores ciclicos en los datos relacionales, se adicciona esta
	opcion en el json, para que ignore las repeticiones de ciclos
	 */
	.AddJsonOptions(op=>op.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles)
	;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 si no queremos usar inyeccion de dependencia

	var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

	builder.Services.AddDbContext<ApplicationDbContext>(
		//op=>op.UseSqlServer(connectionStrings,
		//	//para utilizar UseNetTopologySuite()
		//	sqlServer => sqlServer.UseNetTopologySuite()
		// )

//para agregar mas opciones globales para las consultas usamos {}
op => {
	op.UseSqlServer(connectionStrings,
		//para utilizar UseNetTopologySuite()
		sqlServer => sqlServer.UseNetTopologySuite());
	//para globalizar los queries de solo lectura a nivel global, 
	//como comportamiento por defecto del dbcontext
	op.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

	
	//configuracion para el uso de los modelos compilados

	//	op.UseModel(ApplicationDbContextModel.Instance);
	//

	//
	// para configurar el lazy loading, para uso del proyecto que nos permite
	//cargar y mantener en memoria la data de tabla

	//	op.UseLazyLoadingProxies();
	//		pero tiene la desventaja del problema n+1
	//


});

/*
solo agregamos 
	builder.Services.AddDbContext<ApplicationDbContext>();
*/

var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(
//op=>op.UseSqlServer(connectionStrings,
//	//para utilizar UseNetTopologySuite()
//	sqlServer => sqlServer.UseNetTopologySuite()
// )

	//para agregar mas opciones globales para las consultas usamos {}
	op => {
		op.UseSqlServer(connectionStrings,
			//para utilizar UseNetTopologySuite()
			sqlServer => sqlServer.UseNetTopologySuite());
		//para globalizar los queries de solo lectura a nivel global, 
		//como comportamiento por defecto del dbcontext
		op.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


		//configuracion para el uso de los modelos compilados

		//	op.UseModel(ApplicationDbContextModel.Instance);
		//

		//
		// para configurar el lazy loading, para uso del proyecto que nos permite
		//cargar y mantener en memoria la data de tabla

		//	op.UseLazyLoadingProxies();
		//		pero tiene la desventaja del problema n+1
		//


	}
);

/*
 registrando interface de servicio que nos permite realizar la deteccion
de cambios personalizada
	builder.Services.AddScoped<IActualizarObservableCollection, ActualizadorObservableCollection>();
 */
//builder.Services.AddScoped<IActualizarObservableCollection, ActualizadorObservableCollection>();

/*
 para inyectar por medio de inyeccion de dependencia en el dbcontext
 */
builder.Services.AddScoped<IServicioUsuario,ServicioUsuario>();

/*
 registrando la interface del servicio nuevo, con inyeccion de dependencia
 */
builder.Services.AddScoped<IEventosDbContext, EventosDbContext>();

/*
 para la configuracion de autoMapper, que nos permite automapear, valga la redondancia
clases de tipo DTO, para queries simples
 */
builder.Services.AddAutoMapper(typeof(Program));

/*
 tratando de configurar un dbcontext como servicio singleton
 */
builder.Services.AddSingleton<Singleton>();

var app = builder.Build();

/*
 configuracion para Database Migrate

scope, nos permite crear un contexto, para poder instanciar el dbcontext
ya que este es un servicio dentro de la app, para rescatarlo, necesitamos 
usar un scope

	using (var scope=app.Services.CreateScope())
	{
		//obtenemos una instancia del dbcontext
		var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		//como program se ejecuta al iniciar la app, entonces se corre la migracion
		applicationDbContext.Database.Migrate();

	}

 */



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
