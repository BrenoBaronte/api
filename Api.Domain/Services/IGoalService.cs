using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Services
{
    public interface IGoalService
    {
        Task<List<Goal>> GetAllAsync();
        Task<bool> CreateAsync(Goal goal);
        Task<Goal> GetAsync(Guid goalId);
    }
}
