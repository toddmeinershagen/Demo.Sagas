using System;
using MassTransit;
using MassTransit.Saga;

namespace Demo.Sagas.Subscriber
{
    public class Client
    {
        public void Execute()
        {
            using (var bus = ServiceBusFactory.New(sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom("rabbitmq://localhost/subscriber");
                    sbc.Subscribe(subs => subs.Saga(new InMemorySagaRepository<EstimationSaga>()).Permanent());
                }))
            {
                Console.WriteLine("Hit any key to end.");
                Console.ReadLine();
            }
        }
    }
}