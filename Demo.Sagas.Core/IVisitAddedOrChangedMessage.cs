using System;
using MassTransit;

namespace Demo.Sagas.Core
{
    public interface IVisitAddedOrChangedMessage : CorrelatedBy<Guid>
    {
        int VisitId { get; set; }
    }
}