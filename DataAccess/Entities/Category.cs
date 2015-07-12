using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        public virtual List<Task> Tasks { get; set; }
    }
}