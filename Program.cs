using System;
using System.IO;
using System.Xml.Linq;
using Dragon.Services;
using IntegrationSample;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();



/// Standard example of how to set setup a web host with a few services and middleware.
try
{
    var builder = WebApplication.CreateBuilder(args);

    //Serilog (optional), Provides structured logging.
    builder.Host.UseSerilog();

    // support for compressed responses. Always good to test with this.
    builder.Services.AddResponseCompression(options =>
       {
           options.Providers.Add<BrotliCompressionProvider>();
           options.Providers.Add<GzipCompressionProvider>();
       });


    // Add services to the container.
    // Database in memory for testing, change here if you want to use a real database provided through Entity Framework
    // You can use what ever data layer you like but it should be persistent and not in memory.
    builder.Services.AddDbContext<MyAppDbContext>(optionsBuilder =>
         {
             optionsBuilder.UseInMemoryDatabase("default");
         });
    // add the integration service
    builder.Services.AddScoped<IStubbedIntegrationService, StubbedIntegrationService>();

    builder.Services.AddControllers();

    // A few things like debug log for HTTP and Swagger, only activated in development.
    if (builder.Environment.IsEnvironment("Development"))
    {


        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.ResponseBody; // set to something to log full body etc.
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.CombineLogs = false;

        });

        // this is only for swagger API/Documentation, Not required, should not be activated in any production setup.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        var filePath = Path.Combine(AppContext.BaseDirectory, "API.xml");
        builder.Services.AddSwaggerGen(c =>
             {

                 c.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Title = "Game Platform Integration",
                     Version = "v1",
                     Description = @"This API is called by the Yolted ‘Game System’ to verify and settle bets.
                    <br><br>
                    This is a transparent integration with ‘buy-in’ functionality. The ‘buy-in’ functionality is ‘on’ by default when playing BLITZ. It is highly recommended that you suppor this functionality at your casino to reduce the number of transactions made to increase BLITZ usage. 
                    <br><br>
MetaData is provided to allow you to calculate bonus or loyalty points
                    <br><br>
                  
IMPORTANT! DebitCredit, Debit, and Credit must be implemented to minimize the number of calls made. 
 <br><br>
For example, a game shall send DebitCredit requests until a feature is won by a player, whereby it will send separate Debit and Credit requests. This is to prevent the result of a game round that has a chance to win big being viewed in the operator’s balance before the game has reflected the result.
 <br><br>
                    If you have any questions feel free to contact us.
                    "

                 });
                 c.AddSecurityDefinition("API Key", new OpenApiSecurityScheme
                 {
                     Name = "X-Api-Key",
                     Description = "Authenticate with Apikey provided in header. This is also sent in the json data if you prefer to validate not on header.",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.ApiKey,
                     BearerFormat = "X-Api-Key : MYSECRETKEY"
                 });
                 c.ExampleFilters();

                 c.IncludeXmlComments(filePath, true);


                 var d = XDocument.Load(filePath);

                 c.SchemaFilter<DescribeEnumMembers>(d);


             });

        builder.Services.AddSwaggerExamplesFromAssemblyOf<StartGameRequestExample>();
    }


    /// CORS! This is important, you need to make sure you get this right in production if you host anything facing web applications.
    builder.Services.AddCors(o =>
        {
            o.AddPolicy("AllowAll", builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
                    });
        });

    var app = builder.Build();


    app.UseCors("AllowAll");

    // for static hosting. needed for custom Swagger Files.
    app.UseDefaultFiles();
    app.UseStaticFiles();


    if (builder.Environment.IsEnvironment("Development"))
    {
        app.UseHttpLogging();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
       {
           c.SwaggerEndpoint("/swagger/v1/swagger.json", "Integration API");
       });

    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}