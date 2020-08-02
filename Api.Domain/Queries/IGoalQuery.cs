using Api.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Api.Domain.Queries
{
    public interface IGoalQuery
    {
        Task<Goal> GetAsync(Guid goalId);
    }
}
