using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(config =>
// {
// 	config.SwaggerDoc("v1", new OpenApiInfo
// 	{
// 		Version = "v1",
// 		Title = "ToDo API",
// 		Description = "A simple example ASP.NET Core Web API",
// 		TermsOfService = new Uri("example.com/terms"),
// 		Contact = new OpenApiContact
// 		{
// 			Name = "Aleksandr",
// 			Email = "example@example.com",
// 		},
// 		License = new OpenApiLicense
// 		{
// 			Name = "MIT",
// 			Url = new Uri("https://example.com/license"),
// 		}
// 	});
//
// 	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
// 	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
// 	config.IncludeXmlComments(xmlPath);
// });
builder.Services.AddCors(options => {
	options.AddPolicy("AllowAllHeaders",
		builder => {
			builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1"); });
}

app.UseCors("AllowAllHeaders");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
