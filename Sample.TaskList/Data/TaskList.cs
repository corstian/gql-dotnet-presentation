using System;
using System.Collections.Generic;

namespace Sample.TaskList.Data
{
    public class TaskList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
