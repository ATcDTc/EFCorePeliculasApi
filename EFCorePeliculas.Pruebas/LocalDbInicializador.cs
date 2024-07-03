using EFCorePeliculasApi;
using EFCorePeliculasApi.Servicios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
	/*
	 nos permite crear una bd local
	se debe agregar testclass, aunque esta no 
	contendra ninguna prueba
	 */
	[TestClass]
	public class LocalDbInicializador
	{
		private static readonly string _dbName = "PruebasDeIntegracion";

		/*
		 codigo utilitario
		 */
		static string Master=>new SqlConnectionStringBuilder
		{
			DataSource=@"(LocalDB)\MSSQLLocalDB",
			InitialCatalog="master",
			IntegratedSecurity=true,
		}.ConnectionString;

		static string Filename => Path.Combine(
			Path.GetDirectoryName(typeof(LocalDbInicializador).GetTypeInfo().Assembly.Location),
			$"{_dbName}.mdf");

		/*
		 AssemblyInitialize, nos permite las fn que tiene internamente
		cuando inice la corrida de la suite de pruebas
		 */
		[AssemblyInitialize]
		public static void Initialize(TestContext testcontext)
		{
			DeleteDB();//por si existe una db creada anteriormente
			CreateDB();
		}

		/*
		 AssemblyCleanup, nos permite realizar una limpieza
		 */
		[AssemblyCleanup]
		public static void End()
		{
			DeleteDB();
		}

		/*
		 nos permite tener el debido dbcontext, para la bd local
		 */
		public static ApplicationDbContext GetDbContextLocalDb(bool beginTransaction = true)
		{
			var op = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseSqlServer($"Data Source=(localdb)\\mssqllocaldb;Initial Catalog={_dbName};Integrated Security=True",
					x => x.UseNetTopologySuite()).Options;

			var servicioUsuario=new ServicioUsuario();

			var context=new ApplicationDbContext(op, servicioUsuario,eventosDbContext:null);

			//con esto se puede realizar el commit
			if (beginTransaction)
			{
				context.Database.BeginTransaction();
			}

			return context;
		}

		/*
		 permite crear la bd
		 */
		private static void CreateDB()
		{
			ExecuteCommand(Master, $@"
			CREATE DATABASE [{_dbName}]
			ON (NAME='{_dbName}', FILENAME='{Filename}')");

			using (var context=GetDbContextLocalDb(beginTransaction:false))
			{
				context.Database.Migrate();//aplicando las migraciones
				//si queremos poner data de prueba poner aca, solo para las pruebas
				context.SaveChanges();
			}
		}


		/*
		 borra la bd, cada una de los archivos
		 */
		static void DeleteDB()
		{
			var fileNames = GetDbFiles(Master, $@"
			SELECT [physical_name] FROM [sys].[master_files]
			WHERE [database_id]=DB_ID('{_dbName}')");

			foreach (var filename in fileNames)
			{
				File.Delete(filename);
			}

		}

		/*
		 codigo auxiliar para ejecutar los comandos
		 */
		static void ExecuteCommand(string connectionString,string query)
		{
			using (var connection=new SqlConnection(connectionString))
			{
				using (var cmd=new SqlCommand(query,connection))
				{
					connection.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}

		static IEnumerable<string>GetDbFiles(string connectionString, string query)
		{
			IEnumerable<string> files;

			using (var connection=new SqlConnection(connectionString))
			{
				connection.Open();
				using (var cmd=new SqlCommand(query,connection))
				{
					using (var da=new SqlDataAdapter(cmd))
					{
						var dt = new DataTable();
						da.Fill(dt);
						files=from DataRow myRow in dt.Rows
							  select (string)myRow["physical_name"];
					}
				}
			}

			return files;
		}

	}
}
