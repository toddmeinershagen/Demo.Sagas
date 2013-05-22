namespace Demo.Sagas.Subscriber
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
