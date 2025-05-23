using API;
using API.Data;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200","https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();  

//Should be after Mapcontroller
using var scope = app.Services.CreateScope();
var services=scope.ServiceProvider;
try
{
var context=services.GetRequiredService<DataContext>();
await context.Database.MigrateAsync();
await Seed.SeedUsers(context);
}
catch(Exception ex)
{
    var logger=services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}
//and before app.run()
app.Run();
