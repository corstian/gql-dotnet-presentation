using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Sample.TaskList.Data;
using Sample.TaskList.Graph.Types.Object;
using System.Linq;

namespace Sample.TaskList.Graph
{
    public class TaskListQuery : ObjectGraphType<object>
    {
        public TaskListQuery()
        {
            Name = "Query";

            Field<ListGraphType<TaskObjectType>>()
                .Name("tasks")
                .Resolve(context =>
                {
                    using (var db = new TaskDbContext())
                    {
                        return db.Tasks.ToList();
                    }
                });

            Field<ListGraphType<TaskListObjectType>>()
                .Name("lists")
                .Resolve(context =>
                {
                    using (var db = new TaskDbContext())
                    {
                        return db.TaskLists
                            .Include(q => q.Tasks)
                            .ToList();
                    }
                });
        }
    }
}
