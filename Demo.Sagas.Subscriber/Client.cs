using System;
using MassTransit;
using MassTransit.NHibernateIntegration.Saga;

namespace Demo.Sagas.Subscriber
{
    public class Client
    {
        public void Execute()
        {
            var provider = new MassTransit.NHibernateIntegration.SqlServerSessionFactoryProvider("Data Source=LocalSqlServer;Initial catalog=SagaRepository;Integrated Security=True;", typeof(EstimationSagaMap));
            provider.UpdateSchema();
            using (var bus = ServiceBusFactory.New(sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom("rabbitmq://localhost/subscriber");
                    //sbc.Subscribe(subs => subs.Saga(new InMemorySagaRepository<EstimationSaga>()).Permanent());
                    sbc.Subscribe(subs => subs.Saga(new NHibernateSagaRepository<EstimationSaga>(provider.GetSessionFactory())).Permanent());
                }))
            {
                Console.WriteLine("Hit any key to end.");
                Console.ReadLine();
            }
        }
    }
}