using System.Text.Json.Serialization;

namespace ExoplanetFinderBackend.Domain;

/// <summary>
/// Represents a summary or deduction derived from a series of scientific observations.
/// The Conclusion class encompasses a set of assumptions and serves as the endpoint
/// or outcome of analytical processes tied to observational studies.
/// </summary>
public class Conclusion
{
	[JsonIgnore]
	public Guid Id { get; init; } = Guid.NewGuid();

	[JsonPropertyName("planets")]
	public ICollection<Assumption> Assumptions { get; init; } = [];
}