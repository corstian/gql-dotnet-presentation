using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Sample.TaskList.Data;
using Sample.TaskList.Graph.Types.Object;
using System;
using System.Linq;
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

            Field<TaskListObjectType>()
                .Name("delete")
                .Argument<NonNullGraphType<StringGraphType>>("list", "The id of the list you want to delete")
                .Resolve(context =>
                {
                    Guid.TryParse(context.GetArgument<string>("list"), out var listGuid);

                    using (var db = new TaskDbContext())
                    {
                        var entry = db.TaskLists
                            .Include(q => q.Tasks)
                            .Single(q => q.Id == listGuid);

                        db.TaskLists.Remove(entry);

                        db.SaveChanges();

                        return entry;
                    }
                });
        }
    }
}
