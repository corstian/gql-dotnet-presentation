using GraphQL.Types;
using Sample.TaskList.Data;
using System;

namespace Sample.TaskList.Graph.Types.Object
{
    public class TaskObjectType : ObjectGraphType<Task>
    {
        public TaskObjectType()
        {
            Field<GuidGraphType>()
                .Name("cursor")
                .Resolve(context => context.Source.Id);

            Field<StringGraphType>()
                .Name("description")
                .Resolve(c => c.Source.Description);

            Field<DateTimeGraphType>()
                .Name("created")
                .Resolve(c => c.Source.Created);

            Field<DateTimeGraphType>()
                .Name("finished")
                .Resolve(c => c.Source.Finished);

            Field<BooleanGraphType>()
                .Name("isCompleted")
                .Resolve(c => c.Source.Finished != DateTime.MinValue);  // I'm sorry I was too lazy to make the datetime field nullablegit
        }
    }
}
