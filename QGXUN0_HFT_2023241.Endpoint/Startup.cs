using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Database;
using QGXUN0_HFT_2023241.Repository.ModelRepository;
using QGXUN0_HFT_2023241.Repository.Template;

namespace QGXUN0_HFT_2023241.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<BookDbContext>();

            services.AddTransient<IRepository<Author>, AuthorRepository>();
            services.AddTransient<IRepository<Book>, BookRepository>();
            services.AddTransient<IRepository<Collection>, CollectionRepository>();
            services.AddTransient<IRepository<Publisher>, PublisherRepository>();
            services.AddTransient<IRepository<BookAuthorConnector>, BookAuthorConnectorRepository>();
            services.AddTransient<IRepository<BookCollectionConnector>, BookCollectionConnectorRepository>();

            services.AddTransient<IAuthorLogic, AuthorLogic>();
            services.AddTransient<IBookLogic, BookLogic>();
            services.AddTransient<ICollectionLogic, CollectionLogic>();
            services.AddTransient<IPublisherLogic, PublisherLogic>();

            services.AddSignalR();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
                endpoints.MapHub<SignalHub>("/api");
            });
        }
    }
}
