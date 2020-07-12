using System;

namespace Api.Domain.Entities
{
    public class Goal
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
