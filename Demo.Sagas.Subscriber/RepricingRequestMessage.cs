using System;
using Demo.Sagas.Core;

namespace Demo.Sagas.Subscriber
{
    public class RepricingRequestMessage : IRepricingRequestMessage
    {
        public RepricingRequestMessage(Guid correlationId, int visitId)
        {
            CorrelationId = correlationId;
            VisitId = visitId;
        }

        public Guid CorrelationId { get; private set; }
        public int VisitId { get; private set; }
    }
}