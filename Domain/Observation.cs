using System.Text.Json.Serialization;

namespace ExoplanetFinderBackend.Domain;

/// <summary>
/// Represents a single data point collected during an observational study.
/// Contains detailed measurements and metadata relevant to the analysis
/// of celestial phenomena
/// </summary>
public class Observation
{
	[JsonIgnore]
	public Guid Id { get; init; } = Guid.NewGuid();

	[JsonPropertyName("time")]
	public double Time { get; init; }

	[JsonPropertyName("flux")]
	public double Flux { get; init; }

	[JsonPropertyName("flux_err")]
	public double FluxError { get; init; }

	[JsonPropertyName("quality")]
	public double Quality { get; init; }

	[JsonPropertyName("centroid_col")]
	public double CentroidCol { get; init; }

	[JsonPropertyName("centroid_row")]
	public double CentroidRow { get; init; }

	[JsonPropertyName("sap_flux")]
	public double SapFlux { get; init; }

	[JsonPropertyName("background")]
	public double Background { get; init; }
}