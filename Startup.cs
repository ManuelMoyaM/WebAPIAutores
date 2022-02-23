using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Controllers;

namespace WebAPIAutores

{

    public class Startup
    {

        public Startup(IConfiguration configuration)

        {

            //INYECCION DE DEPENDENCIAS
            //Instanciamos la clase AutoresController
            //Para poder instanciar nuestro clase AutoresController debemos pasarle el dbContext.
            var AutoresController = new AutoresController( 
                new ApplicationDbContext(null),
                    new ServicioB()
                    );
            Configuration = configuration;
        }

        public IConfiguration Configuration {get;}

        public void ConfigureServices(IServiceCollection services)
        {

            //Metodo que nos perimite ignorar los ciclos infinitos al momento de llamar a una clase que instancia a otra clase
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));



            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();



            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });

        }      
    }   
}