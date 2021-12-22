using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace WCFConsumerHelloWorldApp
{
    public class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly IConfiguration _configuration;

        public CloudRoleNameTelemetryInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(ITelemetry telemetry)
        {
            // set custom role name here
            telemetry.Context.Cloud.RoleName = _configuration["ApplicationInsights:InstanceName"];
        }
    }
}
