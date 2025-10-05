using System.Text.Json.Serialization;

namespace ExoplanetFinderBackend.Domain;

/// <summary>
/// Encapsulates a scientific study conducted through a series of observations.
/// The class contains metadata and a collection of data points to facilitate research
/// and analysis workflows related to observational phenomena.
/// </summary>
public abstract class Experiment
{
	public Guid Id { get; init; } = Guid.NewGuid();

	[JsonPropertyName("name")]
	public string? Name { get; init; }

	[JsonPropertyName("conclusion")]
	public Conclusion? Conclusion { get; private set; }

	[JsonPropertyName("conducted_at")]
	public DateTime ConductedAt { get; init; } = DateTime.UtcNow;

	public void Conclude(Conclusion conclusion)
	{
		Conclusion = conclusion;
	}
}

public class KnownStarExperiment : Experiment
{
	[JsonPropertyName("star_id")]
	public string? StarId { get; init; }
}

public class CustomWaveExperiment : Experiment
{
	[JsonPropertyName("observations")]
	public ICollection<Observation> Observations { get; init; } = [];
}