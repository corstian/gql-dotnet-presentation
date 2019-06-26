using System.Reactive.Subjects;
using Sample.TaskList.Data;

namespace Sample.TaskList
{
    public static class Subjects
    {
        public static Subject<Task> TaskCompleted = new Subject<Task>();
        public static Subject<Task> TaskCreated = new Subject<Task>();
        public static Subject<Task> TaskDeleted = new Subject<Task>();
    }
}
