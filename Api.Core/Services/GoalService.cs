using Api.Domain.Entities;
using Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class GoalService : IGoalService
    {
        public async Task<List<Goal>> GetAllAsync()
        {
            return new List<Goal>
            {
                new Goal
                {
                    Id = new Guid(),
                    Title = "English",
                    Count = 137
                },
                new Goal
                {
                    Id = new Guid(),
                    Title = "Workout",
                    Count = 76
                }
            };
        }
    }
}
