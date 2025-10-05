using ExoplanetFinderBackend.Domain;

using Microsoft.EntityFrameworkCore;

namespace ExoplanetFinderBackend.Infrastructure.Persistence;

public class ExperimentsDbContext : DbContext
{
	public DbSet<Experiment> Experiments { get; init; }

	public DbSet<KnownStarExperiment> KnownStarExperiments { get; init; }
	public DbSet<CustomWaveExperiment> CustomWaveExperiments { get; init; }

	public DbSet<Assumption> Assumptions  { get; init; }

	public ExperimentsDbContext(DbContextOptions<ExperimentsDbContext> options)
		: base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Experiment>().UseTptMappingStrategy();

		modelBuilder.Entity<CustomWaveExperiment>().Navigation(cwe => cwe.Observations).AutoInclude();

		base.OnModelCreating(modelBuilder);
	}
}