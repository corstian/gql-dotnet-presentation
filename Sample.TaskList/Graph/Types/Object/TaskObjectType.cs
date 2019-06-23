using GraphQL.Types;
using Sample.TaskList.Data;

namespace Sample.TaskList.Graph.Types.Object
{
    public class TaskObjectType : ObjectGraphType<Task>
    {
        public TaskObjectType()
        {
            Field<IntGraphType>()
                .Name("cursor")
                .Resolve(context => context.Source.Id);

            Field<StringGraphType>()
                .Name("description")
                .Resolve(c => c.Source.Description);

            Field<DateTimeGraphType>()
                .Name("created")
                .Resolve(c => c.Source.Finished);

            Field<DateTimeGraphType>()
                .Name("finished")
                .Resolve(c => c.Source.Finished);
        }
    }
}
