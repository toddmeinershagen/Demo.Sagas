using System;
using Demo.Sagas.Core;
using MassTransit;
using MassTransit.Saga;

namespace Demo.Sagas.Subscriber
{
    public class EstimationSaga : 
        ISaga, 
        InitiatedBy<IVisitAddedOrChangedMessage>,
        Orchestrates<IRepricingResponseMessage>
    {
        public EstimationSaga()
        {}

        public EstimationSaga(Guid correlationId)
        {
            CorrelationId = correlationId;
            CreatedDate = DateTimeOffset.Now;
        }

        public void Consume(IVisitAddedOrChangedMessage message)
        {
            Console.WriteLine("VisitAddedOrChangedMessage received.  VisitId:  {0}", message.VisitId);
            Bus.Publish(new RepricingRequestMessage(CorrelationId, message.VisitId));
        }

#region "ISaga Method/Properties"
        public Guid CorrelationId { get; private set; }
        public IServiceBus Bus { get; set; }
#endregion

        public void Consume(IRepricingResponseMessage message)
        {
            Console.WriteLine("RepricingResponseMessage received.  VisitId:  {0}, ChargeAmount:  {1:c}", message.VisitId, message.ChargeAmount);
        }

        public DateTimeOffset CreatedDate { get; private set; }
    }

    public class EstimationSagaMap : MassTransit.NHibernateIntegration.SagaClassMapping<EstimationSaga>
    {
        public EstimationSagaMap()
        {
            RegisterPropertyMapping(x => x.CreatedDate, x => x.NotNullable(true));
        }
    }
}
