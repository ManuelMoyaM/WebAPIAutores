using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Controllers;
using WebAPIAutores.Services;
using WebAPIAutores.Middlewares;

namespace WebAPIAutores

{

    public class Startup
    {

        public Startup(IConfiguration configuration)

        {

            //INYECCION DE DEPENDENCIAS
            //Instanciamos la clase AutoresController
            //Para poder instanciar nuestro clase AutoresController debemos pasarle el dbContext.
            //Al instanciar de esta manera nuestra clase dependen de abstracciones y no de tipos concretos.
            //Asi tenemos la flexibilidad de utilizar servicioA o servicioB
            /* var AutoresController = new AutoresController( 
                new ApplicationDbContext(null),
                    new ServicioB()
                    );
            Configuration = configuration; */
        }

        public IConfiguration Configuration {get;}

        public void ConfigureServices(IServiceCollection services)
        {

            //Metodo que nos perimite ignorar los ciclos infinitos al momento de llamar a una clase que instancia a otra clase
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            //Este addDbContext se encarga de configurar el applicationDbContext como un servico.
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));



            services.AddEndpointsApiExplorer();

            //Esto quiere decir que cuando una clase requiera un Iservicio(Interfaz) se le entrega un servicioA junto con las dependencias de estas mismas
            services.AddTransient<IServicio, ServicioA>();
            //Tambien podemos instanciar solo la clase(Nose como sea cuando existe mas de una interfaz si )
            /* services.AddTransient<ServicioA>(); */

            //Tipos de servicios
            //services.AddTransient<IServicio, ServicioA>();  Cuando se llama al Iservicio siempre tendremos una instancia nueva  de servicioA
            //services.AddScopped<IServicio, ServicioA>();  Cuando se llama al Iservicio siempre tendremos una instancia distinta de servicioA

             //services.AddSingleton<IServicio, ServicioA>();  SE compartira la misma instanncia del servicioA



            

            services.AddSwaggerGen();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            ///Si no me equivoco todo esto corresponde a middleware

            app.UseMiddleware<LoguearRespuestahttpMiddleware>();




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