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

        public async Task<Goal> CreateAsync(Goal goal)
        {
            goal.Id = Guid.NewGuid();

            var created = await GoalRepository.CreateAsync(goal);

            return created
                ? goal
                : null;
        }

        public async Task<Goal> GetAsync(Guid goalId)
        {
            //validate guid empty
            var goal = await GoalRepository.GetAsync(goalId);

            return goal;
        }

        public async Task<bool> UpdateAsync(Goal goal)
        {
            var updated = await GoalRepository.UpdateAsync(goal);

            return updated;
        }

        public async Task<bool> DeleteAsync(Guid goalId)
        {
            if (goalId.Equals(Guid.Empty))
                return false;

            var deleted = await GoalRepository.DeleteAsync(goalId);

            return deleted;
        }
    }
}
