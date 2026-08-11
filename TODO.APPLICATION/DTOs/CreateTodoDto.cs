using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.DTOs
{
    public class CreateTodoDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public int PriorityId { get; set; }

        public DateTime? DueDate { get; set; }

        public List<int> TagIds { get; set; } = [];
    }
}
