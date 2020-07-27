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
        // todo: Rename, removing async from methods
        public ICacheService CacheService { get; } // isso daqui ta mais pra um repository, trocar
        public IGoalRepository GoalRepository { get; }

        public GoalService(
            ICacheService cacheService,
            IGoalRepository goalRepository)
        {
            CacheService = cacheService
                ?? throw new ArgumentNullException(nameof(cacheService));
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
            if (goalId == Guid.Empty)
                return null;

            var goalKey = $"{typeof(Goal).Name}-{goalId}";
            var cachedGoal = await CacheService.GetAsync<Goal>(goalKey);

            if (cachedGoal != null)
                return cachedGoal;

            var goal = await GoalRepository.GetAsync(goalId);

            if (goal != null)
                await CacheService.SetAsync<Goal>(goalKey, goal);

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
