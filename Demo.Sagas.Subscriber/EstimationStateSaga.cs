using System;
using Demo.Sagas.Core;
using Magnum.StateMachine;
using MassTransit;
using MassTransit.Saga;

namespace Demo.Sagas.Subscriber
{
    public class EstimationStateSaga
        : SagaStateMachine<EstimationStateSaga>,
        ISaga
    {
        static EstimationStateSaga()
        {
            Define(() =>
                {
                    //Correlate(Initiated).By((saga, message) => saga.CorrelationId == message.CorrelationId);
                    Correlate(ReceiveRepriceQuote).By((saga, message) => saga.CorrelationId == message.CorrelationId);

                    Initially(
                        When(Initiated)
                        .Then((saga, message) => saga.Handle(message))
                        .Publish((saga, message) => new RepricingRequestMessage(message.CorrelationId, message.VisitId))
                        .TransitionTo(RepriceQuoteSent));
                    
                    During(RepriceQuoteSent,
                           When(ReceiveRepriceQuote)
                           .TransitionTo(RepriceQuoteReceived)
                           .Then((saga, message) => saga.Handle(message))
                           .Complete());
                });
        }

        public EstimationStateSaga()
        {}

        public EstimationStateSaga(Guid correlationId)
        {
            CorrelationId = correlationId;
            CreatedDate = DateTimeOffset.Now;
        }

        void Handle(IVisitAddedOrChangedMessage message)
        {
             Console.WriteLine("VisitAddedOrChangedMessage received.  VisitId:  {0}", message.VisitId);
        }

        void Handle(IRepricingResponseMessage message)
        {
            Console.WriteLine("RepricingResponseMessage received.  VisitId:  {0}, ChargeAmount:  {1:c}", message.VisitId, message.ChargeAmount);
        }

        #region "ISaga Methods/Properties"
        public virtual Guid CorrelationId { get; private set; }
        public virtual IServiceBus Bus { get; set; }
        #endregion

        public virtual DateTimeOffset CreatedDate { get; private set; }

        public static State Initial { get; set; }
        public static State RepriceQuoteSent { get; set; }
        public static State RepriceQuoteReceived { get; set; }
        public static State Completed { get; set; }

        public static Event<IVisitAddedOrChangedMessage> Initiated { get; set; }
        public static Event<IRepricingResponseMessage> ReceiveRepriceQuote { get; set; }
    }
}
