using System;
using Demo.Sagas.Core;

namespace Demo.Sagas.Subscriber
{
    public class RepricingResponseMessage : IRepricingResponseMessage
    {
        public RepricingResponseMessage(Guid correlationId, int visitId, decimal chargeAmount)
        {
            CorrelationId = correlationId;
            VisitId = visitId;
            ChargeAmount = chargeAmount;
        }

        public Guid CorrelationId { get; private set; }
        public int VisitId { get; private set; }
        public decimal ChargeAmount { get; private set; }
    }
}