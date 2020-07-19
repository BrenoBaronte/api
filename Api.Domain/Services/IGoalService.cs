using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Services
{
    public interface IGoalService
    {
        Task<List<Goal>> GetAllAsync();
        Task<Goal> GetAsync(Guid goalId);
        Task<bool> CreateAsync(Goal goal);
        Task<bool> UpdateAsync(Goal goal);
        Task<bool> DeleteAsync(Guid goalId);
    }
}
