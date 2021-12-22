using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace WCFConsumerHelloWorldApp.Services
{
    public class WCFClientHelper
    {
        public WCFClientHelper()
        {
            
        }

        public virtual T Create<T>() where T : class
        {
            ChannelFactory<T> factory = null;
            var endpoint = new EndpointAddress("https://webappvccx42a.azurewebsites.net/Service1.svc");

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.OpenTimeout = TimeSpan.FromSeconds(5);
            binding.CloseTimeout = TimeSpan.FromSeconds(5);
            binding.SendTimeout = TimeSpan.FromSeconds(5);
            binding.MaxReceivedMessageSize = 200000000;
            binding.MaxBufferPoolSize = 200000000;
            binding.MaxBufferSize = 200000000;
            binding.ReaderQuotas = new XmlDictionaryReaderQuotas()
            {
                MaxDepth = 32,
                MaxArrayLength = 16384,
                MaxBytesPerRead = 4096,
                MaxNameTableCharCount = 16384,
                MaxStringContentLength = 8192
            };

            factory = new ChannelFactory<T>(binding, endpoint);

            T client = factory.CreateChannel();
            return client;
        }

        public virtual async Task<T> ChannelDisposalAsync<T>(object client, Task<T> codeBlock)
        {
            var channel = client as IClientChannel;

            try
            {
                return await codeBlock;
            }
            finally
            {
                if (channel == null)
                {
                    
                }

                if (channel.State == CommunicationState.Faulted)
                {
                    channel.Abort();
                }
                else
                {
                    channel.Close();
                }
            }
        }
    }
}