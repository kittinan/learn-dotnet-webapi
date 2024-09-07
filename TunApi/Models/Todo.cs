using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TunApi.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property for related TodoFiles
        [JsonIgnore]
        public ICollection<TodoFile>? TodoFiles { get; set; }
    }
}