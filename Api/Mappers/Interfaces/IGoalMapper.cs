using Api.Controllers.Models;
using Api.Domain.Entities;
using System.Collections.Generic;

namespace Api.Controllers.Mappers.Interfaces
{
    public interface IGoalMapper
    {
        List<GoalModel> Map(List<Goal> goals);
        Goal Map(GoalModel goalModel);
        GoalModel Map(Goal goal);
    }
}