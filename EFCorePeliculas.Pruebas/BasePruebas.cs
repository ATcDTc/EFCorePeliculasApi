﻿using AutoMapper;
using EFCorePeliculasApi;
using EFCorePeliculasApi.Servicios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
	public class BasePruebas
	{
		/*
		 metodo que crea un dbcontext para pruebas
		 */
		protected ApplicationDbContext ConstruirContext(string nombreBD)
		{
			var op=new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(nombreBD).Options;

			var servicioUsuario = new ServicioUsuario();

			var dbcontext=new ApplicationDbContext(op, servicioUsuario,eventosDbContext:null);
			return dbcontext;
		}

		/*
		 metodo que prueba AutoMapper
		 */
		protected IMapper ConfigurarAutoMapper()
		{
			var config = new MapperConfiguration(op =>
			{
				//instanciando la clase que tiene las configuraciones del mapeo de nuestas instancias
				op.AddProfile(new AutoMapperProfiles());
			});

			return config.CreateMapper();
		}

	}
}
