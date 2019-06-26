using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Sample.TaskList.Data;
using Sample.TaskList.Graph.Types.Object;
using System;
using System.Linq;

namespace Sample.TaskList.Graph.Schema.Mutations
{
    public class TaskMutations : ObjectGraphType
    {
        public TaskMutations()
        {
            Field<TaskObjectType>()
                .Name("create")
                .Argument<StringGraphType>("list", "The guid of the list to add this task to")
                .Argument<NonNullGraphType<StringGraphType>>("description", "Task input")
                .Resolve(context =>
                {
                    Guid.TryParse(context.GetArgument<string>("list"), out var listGuid);

                    using (var db = new TaskDbContext())
                    {
                        var task = new Task
                        {
                            Id = Guid.NewGuid(),
                            Description = context.GetArgument<string>("description"),
                            Created = DateTime.UtcNow
                        };

                        if (listGuid != null && listGuid != Guid.Empty)
                        {
                            var list = db.TaskLists
                                .Include(q => q.Tasks)
                                .FirstOrDefault(q => q.Id == listGuid);

                            list.Tasks.Add(task);
                        }

                        var entry = db.Tasks.Add(task);

                        db.SaveChanges();

                        Subjects.TaskCreated.OnNext(task);

                        return entry.Entity;
                    }
                });

            Field<TaskObjectType>()
                .Name("addTaskToList")
                .Argument<NonNullGraphType<StringGraphType>>("list", "The guid of the list to which the task should be added")
                .Argument<NonNullGraphType<StringGraphType>>("task", "The guid of the task which is to be added to the list")
                .Resolve(context =>
                {
                    Guid.TryParse(context.GetArgument<string>("task"), out var taskGuid);
                    Guid.TryParse(context.GetArgument<string>("list"), out var listGuid);

                    using (var db = new TaskDbContext())
                    {
                        var task = db.Tasks.Find(taskGuid);
                        var list = db.TaskLists.Find(listGuid);

                        list.Tasks.Add(task);

                        db.SaveChanges();

                        return task;
                    }
                });

            Field<TaskObjectType>()
                .Name("markCompleted")
                .Argument<NonNullGraphType<StringGraphType>>("task", "The id of the task to mark as being completed")
                .Resolve(context =>
                {
                    Guid.TryParse(context.GetArgument<string>("task"), out var taskGuid);

                    using (var db = new TaskDbContext())
                    {
                        var task = db.Tasks.Find(taskGuid);

                        task.Finished = DateTime.UtcNow;

                        db.SaveChanges();

                        Subjects.TaskCompleted.OnNext(task);

                        return task;
                    }
                });

            Field<TaskObjectType>()
                .Name("remove")
                .Argument<NonNullGraphType<StringGraphType>>("task", "The id of the task to remove")
                .Resolve(context =>
                {
                    Guid.TryParse(context.GetArgument<string>("task"), out var taskGuid);

                    using (var db = new TaskDbContext())
                    {
                        var task = db.Tasks.Find(taskGuid);

                        db.Tasks.Remove(task);

                        db.SaveChanges();

                        return task;
                    }
                });
        }
    }
}
