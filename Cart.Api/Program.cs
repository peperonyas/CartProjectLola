using Cart.Api.Middleware.Extensions;
using Cart.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CartProject.Api", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CartProject.Api v1"));
}

app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseResponseCaching();
app.UseAuthorization();
app.UseResponseWrapper();
app.UseExceptionMiddeware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();