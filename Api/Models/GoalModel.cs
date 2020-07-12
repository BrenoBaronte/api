using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Models
{
    public class GoalModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Count { get; set; }
    }
}
