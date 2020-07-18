using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public interface IGoalRepository
    {
        Task<List<Goal>> GetAllAsync();
        Task<Goal> GetAsync(Guid goalId);
        Task<bool> CreateAsync(Goal goal);
        Task<bool> UpdateAsync(Goal goal);
    }
}
