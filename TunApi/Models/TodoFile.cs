using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TunApi.Models
{
    public class TodoFile
    {
        public int Id { get; set; }

        [Required]
        public string FilePath { get; set; }

        public DateTime UploadedAt { get; set; }

        // Foreign key to Todo
        public int TodoId { get; set; }

        // Navigation property
        public Todo Todo { get; set; }
    }
}