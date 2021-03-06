﻿namespace Sample.TaskList.Graph
{
    public class TaskListSchema : GraphQL.Types.Schema
    {
        /// <summary>
        /// The schema which represents the root of the GraphQL endpoint.
        /// </summary>
        /// <param name="resolver">A resolver should be passed to the schema in order to be able
        /// to discover the graph types which are registered with the DI container.</param>
        public TaskListSchema(GraphQL.IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TaskListQuery>();
            Mutation = resolver.Resolve<TaskListMutation>();
            Subscription = resolver.Resolve<TaskListSubscription>();
        }
    }
}
