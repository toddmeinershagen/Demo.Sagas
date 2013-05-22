namespace Demo.Sagas.RepricingSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.Execute();
        }
    }
}
