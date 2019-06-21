using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Sample.TaskList.Data;

namespace Sample.TaskList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new TaskDbContext())
            {
                context.Database.EnsureCreated();
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
