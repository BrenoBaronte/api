using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Models
{
    public class GoalModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
