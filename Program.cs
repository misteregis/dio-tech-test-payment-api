using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using PaymentAPI.Resources;
using PaymentAPI.Schemas;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var controllers = services.AddControllers();

controllers.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
controllers.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (_, factory) =>
        factory.Create(typeof(DataAnnotations));
});

controllers.AddNewtonsoftJson();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Payment API",
        Description = "Projeto para a realização de teste técnico da DIO.",
        Contact = new OpenApiContact
        {
            Name = "Link do projeto — @Pottencial",
            Url = new Uri("https://gitlab.com/Pottencial/tech-test-payment-api")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);

    options.SchemaFilter<SwaggerEnumFilter>(xmlPath);
});

var app = builder.Build();

// Configura o pipeline de solicitação http.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();