using ExoplanetFinderBackend.Domain;
using ExoplanetFinderBackend.Domain.Abstractions;

namespace ExoplanetFinderBackend.Infrastructure.Services;

public class MockAiProvider : IAIProvider
{
	private readonly HttpClient _httpClient;

	public MockAiProvider(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public Task<Conclusion> QueryConclusion(Experiment experiment)
	{
		// TODO: create a post request to the AI model and retrieve the conclusion
		Conclusion aiConclusion = new()
		{
			Assumptions = [
				new Assumption
				{
					PlanetName = "b",
					Probability = 0.92,
					OrbitDays = 2.18,
					DistanceToStar = 0.023,
					EarthRadius = 1.2
				},
				new Assumption
				{
					PlanetName = "b",
					Probability = 0.78,
					OrbitDays = 5.42,
					DistanceToStar = 0.041,
					EarthRadius = 1.5
				}
			]
		};

		return Task.FromResult(aiConclusion);
	}
}