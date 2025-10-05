using ExoplanetFinderBackend.Domain;
using ExoplanetFinderBackend.Domain.Abstractions;
using ExoplanetFinderBackend.Infrastructure.Persistence;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ExoplanetFinderBackend.Controllers;

[ApiController]
[Route("experiments")]
public class ExperimentsController : ControllerBase
{
	/// <summary>
	/// Represents the database context for accessing and managing experiment-related entities in the application.
	/// Provides a session with the database through Entity Framework Core for operations on experiments,
	/// assumptions, and other related data.
	/// </summary>
	private readonly ExperimentsDbContext _experimentsDbContext;

	/// <summary>
	/// Represents a service that provides AI-driven conclusions for experiments.
	/// This abstraction is used to interact with AI models or systems to analyze experiment data
	/// and return relevant conclusions, aiding in completing or validating experiments.
	/// </summary>
	private readonly IAIProvider _aiProvider;

	/// <summary>
	/// Provides an in-memory caching mechanism for storing and retrieving data
	/// to improve performance and reduce redundant operations by temporarily
	/// holding frequently accessed or expensive-to-retrieve data in memory.
	/// </summary>
	private readonly IMemoryCache _memoryCache;

	public ExperimentsController(
		ExperimentsDbContext experimentsDbContext,
		IAIProvider aiProvider,
		IMemoryCache memoryCache)
	{
		_experimentsDbContext = experimentsDbContext;
		_aiProvider = aiProvider;
		_memoryCache = memoryCache;
	}

	/// <summary>
	/// Creates a new experiment and stores it in the database.
	/// </summary>
	/// <param name="experiment">
	/// The instance of the CustomWaveExperiment containing the details of the experiment to be created.
	/// </param>
	/// <returns>
	/// A Task resulting in an IActionResult that returns the created experiment details.
	/// Returns Created with a reference to the new experiment upon success.
	/// </returns>
	[HttpPost("custom-wave")]
	public async Task<IActionResult> CreateExperiment([FromBody] CustomWaveExperiment experiment)
	{
		await QueryAiConclusion(experiment);

		_experimentsDbContext.Experiments.Add(experiment);

		await _experimentsDbContext.SaveChangesAsync();

		return Created($"experiments/{experiment.Id}", experiment);
	}

	/// <summary>
	/// Creates a new experiment using a known star and saves it to the database.
	/// </summary>
	/// <param name="experiment">
	/// The instance of KnownStarExperiment containing the details of the experiment to be created.
	/// </param>
	/// <returns>
	/// A Task resulting in an IActionResult that contains the created experiment details.
	/// Returns Created with a reference to the new experiment upon successful creation.
	/// </returns>
	[HttpPost("known-star")]
	public async Task<IActionResult> CreateExperiment([FromBody] KnownStarExperiment experiment)
	{
		await QueryAiConclusion(experiment);

		_experimentsDbContext.Experiments.Add(experiment);

		await _experimentsDbContext.SaveChangesAsync();

		return Created($"experiments/{experiment.Id}", experiment);
	}

	/// <summary>
	/// Deletes an existing experiment identified by its unique identifier.
	/// </summary>
	/// <param name="id">
	/// The unique identifier of the experiment to be deleted.
	/// </param>
	/// <returns>
	/// An IActionResult indicating the result of the delete operation. Returns NotFound if
	/// the experiment does not exist, otherwise returns Ok with the deleted experiment details.
	/// </returns>
	[HttpDelete]
	public async Task<IActionResult> DeleteExperiment([FromRoute] Guid id)
	{
		Experiment? experiment = await _experimentsDbContext.Experiments.FirstOrDefaultAsync(e => e.Id == id);

		if (experiment is null) return NotFound();

		return Ok(experiment);
	}

	/// <summary>
	/// Retrieves a list of all experiments available in the system.
	/// </summary>
	/// <returns>
	/// An IActionResult containing a collection of experiments. Returns Ok with the list of experiments
	/// if any are found, otherwise returns NoContent if no experiments exist.
	/// </returns>
	[HttpGet]
	public async Task<IActionResult> GetExperiments()
	{
		List<Experiment> experiments = await _experimentsDbContext.Experiments.ToListAsync();

		return experiments.Count > 0 ? Ok(experiments) : NoContent();
	}

	/// <summary>
	/// Retrieves an experiment identified by its unique identifier.
	/// </summary>
	/// <param name="id">
	/// The unique identifier of the experiment to be retrieved.
	/// </param>
	/// <returns>
	/// An IActionResult containing the requested experiment. Returns NotFound if
	/// the experiment does not exist, otherwise returns Ok with the experiment details.
	/// </returns>
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetExperiment([FromRoute] Guid id)
	{
		Experiment? cachedExperiment = await _memoryCache.GetOrCreateAsync(id, async entry =>
		{
			Experiment? experiment = await _experimentsDbContext.Experiments
				.Include(e => e.Conclusion)
				.ThenInclude(c => c.Assumptions)
				.FirstOrDefaultAsync(e => e.Id == id);

			entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

			return experiment switch
			{
				CustomWaveExperiment customWaveExperiment => customWaveExperiment,
				KnownStarExperiment knownStarExperiment => knownStarExperiment,
				_ => experiment
			};
		});

		if (cachedExperiment is null) return NotFound();

		return Ok(cachedExperiment);
	}

	/// <summary>
	/// Sends a request to an AI model to analyze the given experiment
	/// and updates the experiment with the conclusion provided by the AI.
	/// </summary>
	/// <param name="experiment">
	/// The experiment instance for which the AI conclusion is to be obtained.
	/// </param>
	/// <returns>
	/// A Task representing the asynchronous operation of querying the AI model
	/// and updating the experiment with the obtained conclusion.
	/// </returns>
	private async Task QueryAiConclusion(Experiment experiment)
	{
		Conclusion aiConclusion = await _aiProvider.QueryConclusion(experiment);

		experiment.Conclude(aiConclusion);
	}
}