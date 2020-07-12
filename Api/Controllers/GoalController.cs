using Api.Controllers.Mappers.Interfaces;
using Api.Controllers.Models;
using Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] GoalModel goalModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var goal = GoalMapper.Map(goalModel);
            var goalCreated = await GoalService.CreateAsync(goal);

            return goalCreated
                ? Created(string.Empty, goalModel) as IActionResult
                : StatusCode((int)HttpStatusCode.NotModified);
        }
    }
}