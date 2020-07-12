using Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Services
{
    public interface IGoalService
    {
        Task<List<Goal>> GetAllAsync();
    }
}
