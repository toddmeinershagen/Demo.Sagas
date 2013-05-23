namespace Demo.Sagas.Subscriber
{
    public class EstimationSagaMap : MassTransit.NHibernateIntegration.SagaClassMapping<EstimationSaga>
    {
        public EstimationSagaMap()
        {
            RegisterPropertyMapping(x => x.CreatedDate, x => x.NotNullable(true));
        }
    }
}