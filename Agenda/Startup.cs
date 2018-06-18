using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Agenda.Models;

namespace Agenda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AgendaContext>(options => options.UseInMemoryDatabase("Agenda"));
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Agenda API", Version = "v1" });
            });
        }

        private static void AddTestData(AgendaContext agendaContext)
        {
            agendaContext.Professores.Add(new Professor{Nome = "Ericson Fonseca", EMail = "ericson.fonseca@opensourcebootcamp.org.br"});
            agendaContext.Professores.Add(new Professor{Nome = "Robson Araújo", EMail = "robson.araujo@opensourcebootcamp.org.br"});
            agendaContext.Professores.Add(new Professor{Nome = "Renato Groffe", EMail = "renato.groffe@opensourcebootcamp.org.br"});
            agendaContext.Professores.Add(new Professor{Nome = "Adriano Maringolo", EMail = "adriano.maringolo@opensourcebootcamp.org.br"});
            agendaContext.Cursos.Add(new Curso{Nome="Quick Start: ASP.NET Core Fundamentals", CargaHorariaHoras = 4, Descricao = "Aprenda os primeiros passos do ASP.NET Core na prática!"});
            agendaContext.Cursos.Add(new Curso{Nome="Quick Start: Entity Framework Core Fundamentals", CargaHorariaHoras = 4, Descricao = "Aprenda os primeiros passos do Entity Framework Core na prática!"});
            agendaContext.Cursos.Add(new Curso{Nome="Quick Start: Azure Fundamentals", CargaHorariaHoras = 4, Descricao = "Aprenda os primeiros passos do Azure na prática!"});
            agendaContext.SaveChanges();   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            AgendaContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agenda API V1");
            });

            AddTestData(dbContext);

            app.UseCors(builder => builder.AllowAnyMethod()
                                          .AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
