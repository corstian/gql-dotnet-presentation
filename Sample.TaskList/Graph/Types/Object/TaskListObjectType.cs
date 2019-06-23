using GraphQL.Types;
using TList = Sample.TaskList.Data.TaskList;

namespace Sample.TaskList.Graph.Types.Object
{
    public class TaskListObjectType : ObjectGraphType<TList>
    {
        public TaskListObjectType()
        {
            Field<GuidGraphType>()
                .Name("cursor")
                .Resolve(c => c.Source.Id);

            Field<StringGraphType>()
                .Name("name")
                .Resolve(c => c.Source.Name);

            Field<ListGraphType<TaskObjectType>>()
                .Name("tasks")
                .Resolve(c => c.Source.Tasks);
        }
    }
}
