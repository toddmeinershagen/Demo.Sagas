using System;
using Demo.Sagas.Core;

namespace Demo.Sagas.Publisher
{
    public class VisitAddedOrChangedMessage : IVisitAddedOrChangedMessage
    {
        public VisitAddedOrChangedMessage(int visitId)
        {
            CorrelationId = Guid.NewGuid();
            VisitId = visitId;
        }

        public int VisitId { get; set; }
        public Guid CorrelationId { get; private set; }
    }
}