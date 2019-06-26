using GraphQL.Types;
using Sample.TaskList.Data;
using Sample.TaskList.Graph.Types.Object;

namespace Sample.TaskList.Graph
{
    public class TaskListSubscription : ObjectGraphType<object>
    {
        public TaskListSubscription()
        {
            Field<TaskObjectType>()
                .Name("onCreated")
                .Subscribe(c => Subjects.TaskCreated)
                .Resolve(c => c.Source as Task);

            Field<TaskObjectType>()
                .Name("onDeleted")
                .Subscribe(c => Subjects.TaskDeleted)
                .Resolve(c => c.Source as Task);

            Field<TaskObjectType>()
                .Name("onCompleted")
                .Subscribe(c => Subjects.TaskCompleted)
                .Resolve(c => c.Source as Task);
        }
    }
}
