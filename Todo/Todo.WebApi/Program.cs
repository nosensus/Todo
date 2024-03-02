using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo.WebApi.Data;
using Todo.WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddDbContext<TodoDbContext>(option => {
	option.UseNpgsql(builder.Configuration.GetConnectionString("TodoDataBase"));
});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup => {
	setup.SwaggerDoc("v1", new OpenApiInfo {
		Title = "Todo",
		Version = "v1"
	});
});
builder.Services.AddCors(options => {
	options.AddPolicy("AllowAllHeaders",
		builder => {
			builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI(c => {
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
	});
}

app.UseSwagger();
app.UseSwaggerUI(c => {
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
});

app.UseCors("AllowAllHeaders");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
