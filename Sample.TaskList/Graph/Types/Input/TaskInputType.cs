using GraphQL.Types;
using Sample.TaskList.Data;

namespace Sample.TaskList.Graph.Types.Input
{
    public class TaskInputType : InputObjectGraphType<Task>
    {
        public TaskInputType()
        {
            Field(q => q.Description);
        }
    }
}
