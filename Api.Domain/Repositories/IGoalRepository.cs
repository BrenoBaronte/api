using Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public interface IGoalRepository
    {
        Task<List<Goal>> GetAllAsync();
    }
}
