using System;
using MassTransit;

namespace Demo.Sagas.Core
{
    public interface IRepricingResponseMessage : CorrelatedBy<Guid>
    {
        int VisitId { get; }
        decimal ChargeAmount { get; }
    }
}