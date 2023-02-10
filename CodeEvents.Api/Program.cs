using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeEvents.Api.Data;
using CodeEvents.Api.Data.Repositories;
using CodeEvents.Api.Core.Repositories;
using System.Reflection;

namespace CodeEvents.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            builder.Services.AddDbContext<CodeEventsApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CodeEventsApiContext") ?? throw new InvalidOperationException("Connection string 'CodeEventsApiContext' not found.")));


            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                            .AddNewtonsoftJson();
                                               // .AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("CodeEventsOpenAPISpecification", new()
                {
                    Title = "CodeEvent API",
                    Version = "1",
                    Description = "Through this API you can access code events and stuff.",
                    Contact = new()
                    {
                        Email = "david.nokto@lexicon.se",
                        Name = "David Nokto",
                        Url = new Uri("https://www.google.com")
                    },
                    License = new()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://www.google.com")
                    }
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                opt.IncludeXmlComments(xmlCommentsFullPath);
            });


            builder.Services.AddAutoMapper(typeof(MapperProfile));

            var app = builder.Build();

            await app.SeedDataAsync();




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(setupAction =>
                {
                    setupAction.SwaggerEndpoint("/swagger/CodeEventsOpenAPISpecification/swagger.json",
                        "CodeEvent API");
                    setupAction.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}