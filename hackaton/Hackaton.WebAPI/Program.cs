using Hackaton.Application;
using Hackaton.Persistence;
using Hackaton.Service;
using Hackaton.WebAPI.Extensions;
using Hackaton.WebAPI.MiddleWare;
using Hackaton.WebAPI.Presentation;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddServices();
builder.Services.ConfigureVersioning();
//builder.Services.ConfigureSwagger();

builder.Services.AddControllers().AddApplicationPart(typeof(AssemblyReference).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    //app.UseSwaggerUI(options =>
    //{
    //    foreach(var description in provider.ApiVersionDescriptions)
    //    {
    //        options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    //        options.DocumentTitle = "Employee Microservice Swagger";
    //    }
    //});
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();

app.ConfigureExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
