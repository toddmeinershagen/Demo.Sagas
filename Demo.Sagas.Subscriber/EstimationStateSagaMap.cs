using MassTransit.NHibernateIntegration;

namespace Demo.Sagas.Subscriber
{
    public class EstimationStateSagaMap : SagaStateMachineClassMapping<EstimationStateSaga>
    {
        public EstimationStateSagaMap()
        {
            RegisterPropertyMapping(x => x.CreatedDate, x => x.NotNullable(true));
        }
    }
}