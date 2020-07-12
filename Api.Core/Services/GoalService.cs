using Api.Domain.Entities;
using Api.Domain.Repositories;
using Api.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class GoalService : IGoalService
    {
        public IGoalRepository GoalRepository { get; }

        public GoalService(
            IGoalRepository goalRepository)
        {
            GoalRepository = goalRepository
                ?? throw new System.ArgumentNullException(nameof(goalRepository));
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            var goals = await GoalRepository.GetAllAsync();

            return goals;
        }
    }
}
