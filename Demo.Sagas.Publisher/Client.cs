using System;
using MassTransit;

namespace Demo.Sagas.Publisher
{
    public class Client
    {
        public void Execute()
        {
            using (var bus = ServiceBusFactory.New(b =>
                {
                    b.UseRabbitMq();
                    b.ReceiveFrom("rabbitmq://localhost/publisher");
                }))
            {
                int visitId = 0;
                Func<bool> getVisitId = () =>
                    {
                        Console.WriteLine("Enter a visit id.");
                        return int.TryParse(Console.ReadLine(), out visitId);
                    };

                while (getVisitId())
                {
                    bus.Publish(new VisitAddedOrChangedMessage(visitId));
                }
            }
        }
    }
}