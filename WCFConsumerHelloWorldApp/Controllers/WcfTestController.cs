using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using ServiceReference1;
using System.Threading;
using WCFConsumerHelloWorldApp.Services;

namespace WCFConsumerHelloWorldApp.Controllers
{
    [ApiController]
    public class WcfTestController : ControllerBase
    {
        private readonly ILogger<WcfTestController> _logger;
        private readonly WCFClientHelper _wcfClientHelper;

        public WcfTestController(ILogger<WcfTestController> logger, WCFClientHelper wcfClientHelper)
        {
            _logger = logger;
            _wcfClientHelper = wcfClientHelper;
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

        [HttpGet]
        [Route("WcfTest/threads")]
        public ActionResult GetThreadsAsync()
        {
            int t1, t2;
            ThreadPool.GetMinThreads(out t1, out t2);
            var result = $"Processors: {Environment.ProcessorCount}, MinThreads: {t1}";
            return Ok(result);
        }

        [HttpGet]
        [Route("WcfTest/WcfHelper/GetData")]
        public async Task<ActionResult> GetDataAsync()
        {
            Random r = new Random();
            int n = r.Next();

            var client = _wcfClientHelper.Create<IService1>();
            var result = await _wcfClientHelper.ChannelDisposalAsync(client, client.GetDataAsync(n));

            return Ok(result);
        }
    }
}
