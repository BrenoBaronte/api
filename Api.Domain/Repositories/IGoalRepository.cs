using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public interface IGoalRepository
    {
        Task<bool> CreateAsync(Goal goal);
        Task<bool> UpdateAsync(Goal goal);
        Task<bool> DeleteAsync(Guid goalId);
    }
}
