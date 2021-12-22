using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.EventCounterCollector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WCFConsumerHelloWorldApp.Services;

namespace WCFConsumerHelloWorldApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var minThreads = Configuration.GetValue<int>("MinThreads");
            ThreadPool.SetMinThreads(minThreads, minThreads);

            services.ConfigureTelemetryModule<EventCounterCollectionModule>(
                (module, o) =>
                {
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "time-in-gc"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-0-size"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-0-gc-count"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-1-size"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-1-gc-count"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-2-size"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-2-gc-count"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "threadpool-completed-items-count"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "threadpool-queue-length"));
                    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "threadpool-thread-count"));
                }
            );

            services.AddSingleton<WCFClientHelper>();
            services.AddSingleton<ITelemetryInitializer, CloudRoleNameTelemetryInitializer>();

            services.AddApplicationInsightsTelemetry();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
