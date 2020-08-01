using Api.Domain.Caches;
using Api.Domain.Entities;
using Api.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Api.Repositories.Repositories
{
    public class GoalQueryWithCache : IGoalQuery
    {
        public ICache CacheService { get; }
        public IGoalQuery InnerGoalQuery { get; }

        public GoalQueryWithCache(
            ICache cacheService,
            IGoalQuery goalQuery)
        {
            CacheService = cacheService
                ?? throw new ArgumentNullException(nameof(cacheService));
            InnerGoalQuery = goalQuery
                ?? throw new ArgumentNullException(nameof(goalQuery));
        }

        public async Task<Goal> GetAsync(Guid goalId)
        {
            var goalKey = $"{typeof(Goal).Name}-{goalId}";

            var cachedGoal = await CacheService.GetAsync<Goal>(goalKey);

            if (cachedGoal is Goal goalFromCache)
                return goalFromCache;

            var goal = await InnerGoalQuery.GetAsync(goalId);

            await CacheService.SetAsync<Goal>(goalKey, goal);

            return goal;
        }
    }
}
