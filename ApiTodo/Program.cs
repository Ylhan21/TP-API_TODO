
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
// SeedData.InitAgenda();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
c.EnableAnnotations();
});

builder.Services.AddDbContext<TodoContext>();


builder.Services.AddControllers()
// Prevent circular references when serializing objects to JSON
   .AddJsonOptions(x =>
       x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
   );


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();
app.Run();
