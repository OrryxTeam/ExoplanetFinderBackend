using System.Text.Json.Serialization;

namespace ExoplanetFinderBackend.Domain;

/// <summary>
/// Represents a preliminary hypothesis or inference derived from observational data.
/// Assumptions provide foundational insights used to draw conclusions about potential
/// exoplanets and their characteristics.
/// </summary>
public class Assumption
{
	[JsonIgnore]
	public Guid Id { get; init; } = Guid.NewGuid();

	[JsonPropertyName("planet")]
	public string PlanetName { get; init; }

	[JsonPropertyName("probability")]
	public double Probability { get; init; }

	[JsonPropertyName("period_days")]
	public double OrbitDays { get; init; }

	[JsonPropertyName("distance_au")]
	public double DistanceToStar { get; init; }

	[JsonPropertyName("radius_earth")]
	public double EarthRadius { get; init; }
}