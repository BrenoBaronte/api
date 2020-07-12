using Api.Controllers.Mappers.Interfaces;
using Api.Controllers.Models;
using Api.Domain.Entities;
using System.Collections.Generic;

namespace Api.Mappers
{
    public class GoalMapper : IGoalMapper
    {
        public List<GoalModel> Map(List<Goal> goals)
        {
            var goalsModels = new List<GoalModel>();

            foreach (var goal in goals)
            {
                goalsModels.Add(new GoalModel
                {
                    Title = goal.Title,
                    Count = goal.Count
                });
            }

            return goalsModels;
        }
    }
}
