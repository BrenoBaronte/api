using Api.Domain.Entities;
using Api.Domain.Repositories;
using Api.Domain.Services;
using System;
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
                ?? throw new ArgumentNullException(nameof(goalRepository));
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            var goals = await GoalRepository.GetAllAsync();

            return goals;
        }

        public async Task<bool> CreateAsync(Goal goal)
        {
            goal.Id = Guid.NewGuid();

            var created = await GoalRepository.CreateAsync(goal);

            return created;
        }

        public async Task<Goal> GetAsync(Guid goalId)
        {
            var goal = await GoalRepository.GetAsync(goalId);

            return goal;
        }

        public async Task<bool> UpdateAsync(Goal goal)
        {
            var updated = await GoalRepository.UpdateAsync(goal);

            return updated;
        }
    }
}
