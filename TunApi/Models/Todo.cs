using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TunApi.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}