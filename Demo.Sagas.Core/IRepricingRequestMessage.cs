using System;
using MassTransit;

namespace Demo.Sagas.Core
{
    public interface IRepricingRequestMessage : CorrelatedBy<Guid>
    {
        int VisitId { get; } 
    }
}