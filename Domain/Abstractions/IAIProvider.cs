namespace ExoplanetFinderBackend.Domain.Abstractions;

public interface IAIProvider
{
	public Task<Conclusion> QueryConclusion(Experiment experiment);
}