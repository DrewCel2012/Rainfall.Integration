using Rainfall.Api.Filters;
using Rainfall.Service;
using Rainfall.Service.Interface;
using System.Reflection;

namespace Rainfall.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionHandlingFilterConfig)));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "Rainfall Api",
                    Version = "1.0",
                    Contact = new()
                    {
                        Name = "Sorted",
                        Url = new("https://www.sorted.com")
                    },
                    Description = "An API which provides rainfall reading data"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });

            // Enable CORS (Cross Origin Resource Sharing):
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: builder.Configuration[key: "CORSSettings:Name"] ?? "", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IRainfallService, RainfallService>();

            builder.Services.AddHttpClient(builder.Configuration["Rainfall.Api:ClientName"], httpClient =>
            {
                httpClient.BaseAddress = new Uri(builder.Configuration["Rainfall.Api:BaseAddress"]);
            });


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
