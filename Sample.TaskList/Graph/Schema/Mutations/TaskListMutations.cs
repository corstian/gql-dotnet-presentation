using GraphQL.Types;
using Sample.TaskList.Data;
using Sample.TaskList.Graph.Types.Object;
using System;
using TList = Sample.TaskList.Data.TaskList;

namespace Sample.TaskList.Graph.Schema.Mutations
{
    public class TaskListMutations : ObjectGraphType<object>
    {
        public TaskListMutations()
        {
            Field<TaskListObjectType>()
                .Name("create")
                .Argument<StringGraphType>("name", "The name of the tasklist")
                .Resolve(context =>
                {
                    using (var db = new TaskDbContext())
                    {
                        var entry = db.TaskLists.Add(new TList
                        {
                            Id = Guid.NewGuid(),
                            Name = context.GetArgument<string>("name")
                        });

                        db.SaveChanges();

                        return entry.Entity;
                    }
                });
        }
    }
}
