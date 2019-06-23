using GraphQL.Types;
using Sample.TaskList.Data;
using Sample.TaskList.Graph.Types.Object;
using System;

namespace Sample.TaskList.Graph.Schema.Mutations
{
    public class TaskMutations : ObjectGraphType
    {
        public TaskMutations()
        {
            Field<TaskObjectType>()
                .Name("create")
                .Argument<StringGraphType>("description", "Task input")
                .Resolve(context =>
                {
                    using (var db = new TaskDbContext())
                    {
                        var entry = db.Tasks.Add(new Task
                        {
                            Id = Guid.NewGuid(),
                            Description = context.GetArgument<string>("description"),
                            Created = DateTime.UtcNow
                        });

                        db.SaveChanges();

                        return entry.Entity;
                    }
                });
        }
    }
}
