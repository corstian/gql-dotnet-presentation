using GraphQL.Types;
using Sample.TaskList.Graph.Schema.Mutations;

namespace Sample.TaskList.Graph
{
    public class TaskListMutation : ObjectGraphType<object>
    {
        public TaskListMutation()
        {
            Field<TaskMutations>()
                .Name("task")
                .Resolve(c => new { });

            Field<TaskListMutations>()
                .Name("list")
                .Resolve(c => new { });
        }
    }
}
