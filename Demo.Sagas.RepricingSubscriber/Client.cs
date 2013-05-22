using System;
using MassTransit;

namespace Demo.Sagas.RepricingSubscriber
{
    public class Client
    {
        public void Execute()
        {
            using (var bus = ServiceBusFactory.New(sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom("rabbitmq://localhost/repricingsubscriber");
                }))
            {
                var consumer = new RepricingConsumer();
                consumer.Start(bus);

                Console.WriteLine("Hit any key to end.");
                Console.ReadLine();   
            }
        }
    }
}