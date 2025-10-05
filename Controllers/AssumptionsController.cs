using ExoplanetFinderBackend.Domain;
using ExoplanetFinderBackend.Infrastructure.Persistence;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExoplanetFinderBackend.Controllers;

[ApiController]
[Route("assumptions")]
public class AssumptionsController : ControllerBase
{
	private readonly ExperimentsDbContext _experimentsDbContext;

	public AssumptionsController(ExperimentsDbContext experimentsDbContext)
	{
		_experimentsDbContext = experimentsDbContext;
	}

	[HttpGet]
	public async Task<IActionResult> GetAssumptions()
	{
		List<Assumption> assumptions = await _experimentsDbContext.Assumptions.ToListAsync();

		return assumptions.Any() ? Ok(assumptions) : NoContent();
	}
}