using ExoplanetFinderBackend.Domain.Abstractions;

using ExoplanetFinderBackend.Infrastructure.Persistence;
using ExoplanetFinderBackend.Infrastructure.Persistence.Extensions;
using ExoplanetFinderBackend.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

builder.Services.AddHttpClient<IAIProvider, MockAiProvider>();
builder.Services.AddDbContext<ExperimentsDbContext>(opt =>
	opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.Services.ApplyMigrations<ExperimentsDbContext>();
app.UseCors("AllowAll");
app.Run();