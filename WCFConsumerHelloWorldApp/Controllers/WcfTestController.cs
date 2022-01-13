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
        private readonly Service1Client _service1Client;
        private readonly WCFClientHelper _wcfClientHelper;

        public WcfTestController(ILogger<WcfTestController> logger, WCFClientHelper wcfClientHelper, Service1Client service1Client)
        {
            _logger = logger;
            _wcfClientHelper = wcfClientHelper;
            _service1Client = service1Client;
        }

        [HttpGet]
        [Route("WcfTest/GetData/{number:int}")]
        public async Task<ActionResult> GetAsync(int number)
        {

            if (_service1Client.State == CommunicationState.Faulted || _service1Client.State == CommunicationState.Closed || _service1Client.State == CommunicationState.Closing)
            {
                await _service1Client.OpenAsync();
            }

            string result = await _service1Client.GetDataAsync(number);

            return Ok(result);

        }

        [HttpGet]
        [Route("WcfTest/GetData")]
        public async Task<ActionResult> GetAsync()
        {
            Random r = new Random();
            int n = r.Next();

            if (_service1Client.State == CommunicationState.Faulted || _service1Client.State == CommunicationState.Closed || _service1Client.State == CommunicationState.Closing)
            {
                await _service1Client.OpenAsync();
            }

            string result = await _service1Client.GetDataAsync(n);

            return Ok(result);
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
