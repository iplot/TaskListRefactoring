using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DataAccess.Entities
{
    public class Task
    {
        public int TaskId { get; set; }

        public string Text { get; set; }

        public bool IsFinished { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [InverseProperty("Task")]
        public List<SubTask> SubTasks { get; set; }
    }
}