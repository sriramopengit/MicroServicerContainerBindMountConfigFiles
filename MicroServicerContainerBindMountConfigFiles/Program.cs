
namespace MicroServicerContainerBindMountConfigFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Configuration
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //       .AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true)
            //       .AddEnvironmentVariables();


            // Read DOCKER_ENV variable which is injected into the container
            // through the docker-compose.yml file (default to "Dev" if not set)
            var dockerEnv = Environment.GetEnvironmentVariable("DOCKER_ENV") ?? "Dev";

            // Configure Configuration Sources
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Docker.{dockerEnv}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
        }
    }
}
