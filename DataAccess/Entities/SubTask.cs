using Newtonsoft.Json;

namespace DataAccess.Entities
{
    public class SubTask
    {
        public int SubTaskId { get; set; }

        public string Text { get; set; }

        public bool IsFinished { get; set; }

        public int TaskId { get; set; }

        [JsonIgnore]
        public virtual Task Task { get; set; }
    }
}