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

            if (goals == null)
                return goalsModels;

            foreach (var goal in goals)
                goalsModels.Add(ToGoalModel(goal));

            return goalsModels;
        }

        public Goal Map(GoalModel goalModel)
        {
            if (goalModel == null)
                return new Goal();

            var goal = ToGoal(goalModel);

            return goal;
        }

        public GoalModel Map(Goal goal)
        {
            if (goal == null)
                return new GoalModel();

            var goalModel = ToGoalModel(goal);

            return goalModel;
        }

        private static Goal ToGoal(GoalModel goalModel)
        {
            return new Goal
            {
                Id = goalModel.Id,
                Title = goalModel.Title,
                Count = goalModel.Count
            };
        }

        private static GoalModel ToGoalModel(Goal goal)
        {
            var goalModel = new GoalModel
            {
                Id = goal.Id,
                Title = goal.Title,
                Count = goal.Count
            };

            return goalModel;
        }
    }
}
