using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using ServiceReference1;

namespace WCFConsumerHelloWorldApp.Controllers
{
    [ApiController]
    public class WcfTestController : ControllerBase
    {
        private readonly ILogger<WcfTestController> _logger;

        public WcfTestController(ILogger<WcfTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("WcfTest/GetData/{number}")]
        public async Task<ActionResult> GetAsync(int number)
        {
            Service1Client service =
                new Service1Client(Service1Client.EndpointConfiguration.BasicHttpsBinding_IService1);
            var result = service.GetDataAsync(number);

            return Ok(await result);
        }

        [HttpGet]
        [Route("WcfTest/GetData")]
        public async Task<ActionResult> GetAsync()
        {
            Service1Client service =
                new Service1Client(Service1Client.EndpointConfiguration.BasicHttpsBinding_IService1);

            Random r = new Random();
            int n = r.Next();

            var result = service.GetDataAsync(n);

            return Ok(await result);
        }
    }
}
