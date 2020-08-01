using Api.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public interface IGoalQuery
    {
        Task<Goal> GetAsync(Guid goalId);
    }
}
