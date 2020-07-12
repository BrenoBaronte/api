using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public interface IGoalRepository
    {
        Task<List<Goal>> GetAllAsync();
        Task<bool> CreateAsync(Goal goal);
        Task<Goal> GetAsync(Guid goalId);
    }
}
