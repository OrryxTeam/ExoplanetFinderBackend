using Microsoft.EntityFrameworkCore;

namespace ExoplanetFinderBackend.Infrastructure.Persistence.Extensions;

public static class DbContextExtensions
{
	public static async Task<IEnumerable<string>> ApplyMigrations<TContext>(this IServiceProvider serviceProvider) where TContext : DbContext
	{
		using IServiceScope scope = serviceProvider.CreateScope();
		TContext dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
		IEnumerable<string> pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
		await dbContext.Database.MigrateAsync();

		return pendingMigrations;
	}
}