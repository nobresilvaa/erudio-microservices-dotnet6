using RestWithASPNETUdemy.Services;
using RestWithASPNETUdemy.Services.Inplementations;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void CongifureServices(IServiceCollection Services)
        {

           Services.AddControllers();
           Services.AddEndpointsApiExplorer();
           Services.AddSwaggerGen();

            Services.AddScoped<IPersonServices, PersonServiceImplementation>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment Environment)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }

    }
}
