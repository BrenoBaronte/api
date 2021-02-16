using Api.Domain.Entities;
using Api.Domain.Queries;
using Api.Domain.Repositories;
using Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class GoalService : IGoalService
    {
        public IGoalCommand GoalRepository { get; }
        public IGoalQuery GoalQuery { get; }

        public GoalService(
            IGoalCommand goalRepository,
            IGoalQuery goalQuery)
        {
            GoalRepository = goalRepository
                ?? throw new ArgumentNullException(nameof(goalRepository));
            GoalQuery = goalQuery
                ?? throw new ArgumentNullException(nameof(goalQuery));
        }

        public async Task<Goal> CreateAsync(Goal goal)
        {
            goal.Id = Guid.NewGuid();

            var created = await GoalRepository.CreateAsync(goal);

            return created
                ? goal
                : null;
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            var goals = await GoalQuery.GetAllAsync();

            return goals;
        }

        public async Task<Goal> GetAsync(Guid goalId)
        {
            if (goalId == Guid.Empty)
                return null;

            var goal = await GoalQuery.GetAsync(goalId);

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
