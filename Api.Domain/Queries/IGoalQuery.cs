using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Queries
{
    public interface IGoalQuery
    {
        Task<List<Goal>> GetAllAsync();

        Task<Goal> GetAsync(Guid goalId);
    }
}
