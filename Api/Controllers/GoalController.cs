using Api.Controllers.Mappers.Interfaces;
using Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class GoalController : ControllerBase
    {
        public IGoalService GoalService { get; }
        public IGoalMapper GoalMapper { get; }

        public GoalController(
            IGoalService goalService,
            IGoalMapper goalMapper)
        {
            GoalService = goalService
                ?? throw new ArgumentNullException(nameof(goalService));
            GoalMapper = goalMapper
                ?? throw new ArgumentNullException(nameof(goalMapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var goals = await GoalService.GetAllAsync();
            var goalsModels = GoalMapper.Map(goals);

            return Ok(goalsModels);
        }
    }
}