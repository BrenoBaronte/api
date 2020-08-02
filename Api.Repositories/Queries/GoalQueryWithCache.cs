using Api.Domain.Caches;
using Api.Domain.Entities;
using Api.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories.Queries
{
    public class GoalQueryWithCache : IGoalQuery
    {
        public ICache Cache { get; }
        public IGoalQuery InnerGoalQuery { get; }

        public GoalQueryWithCache(
            ICache cacheService,
            IGoalQuery goalQuery)
        {
            Cache = cacheService
                ?? throw new ArgumentNullException(nameof(cacheService));
            InnerGoalQuery = goalQuery
                ?? throw new ArgumentNullException(nameof(goalQuery));
        }

        public async Task<Goal> GetAsync(Guid goalId)
        {
            var goalKey = $"{typeof(Goal).Name}-{goalId}";

            var cachedGoal = await Cache.GetAsync<Goal>(goalKey);

            if (cachedGoal is Goal goalFromCache)
                return goalFromCache;

            var goal = await InnerGoalQuery.GetAsync(goalId);

            await Cache.SetAsync<Goal>(goalKey, goal);

            return goal;
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            const string allGoalsKey = "All-Goals";

            var cachedGoals = await Cache.GetAsync<List<Goal>>(allGoalsKey);

            if (cachedGoals is List<Goal> goalsFromCache)
                return goalsFromCache;

            var goals = await InnerGoalQuery.GetAllAsync();

            await Cache.SetAsync<List<Goal>>(allGoalsKey, goals);

            return goals;
        }
    }
}
