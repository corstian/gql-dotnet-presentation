using System;
using System.Collections.Generic;

namespace Sample.TaskList.Data
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Finished { get; set; }

        public List<Task> SubTasks { get; set; }
    }
}
