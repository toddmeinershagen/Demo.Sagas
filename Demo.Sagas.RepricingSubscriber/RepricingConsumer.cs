using System;
using System.Threading;
using Demo.Sagas.Core;
using MassTransit;

namespace Demo.Sagas.RepricingSubscriber
{
    public class RepricingConsumer : Consumes<IRepricingRequestMessage>.All, IBusService
    {
        private IServiceBus _bus;
        private UnsubscribeAction _unsubscribeAction;

        public void Consume(IRepricingRequestMessage message)
        {
            Console.WriteLine("RepricingRequestMessage received.  VisitId:  {0}", message.VisitId);
            Thread.Sleep(10000);
            _bus.Publish(new RepricingResponseMessage(message.CorrelationId, message.VisitId, 1.25m));
        }

        public void Dispose()
        {
            //_bus.Dispose();
        }

        public void Start(IServiceBus bus)
        {
            _bus = bus;
            _unsubscribeAction = bus.SubscribeConsumer(() => this);
        }

        public void Stop()
        {
            _unsubscribeAction();
        }
    }
}