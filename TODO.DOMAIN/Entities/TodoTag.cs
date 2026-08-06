using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Entities
{
    public class TodoTag
    {
        public int TodoId { get; set; }

        public int TagId { get; set; }
    }
}
