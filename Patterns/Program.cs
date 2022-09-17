namespace Patterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Adapter
            var myClient = new MyClient();
            myClient.Request(new Target());
            myClient.Request(new Adapter());
            #endregion

            #region Facade
            Facade facade = new Facade(new SystemA(), new SystemB(), new SystemC());
            facade.DoWork3();
            #endregion

            #region Decorator
            FinalWorkComponent1 finalWorkComponent1 = new FinalWorkComponent1();
            finalWorkComponent1.DoWork();

            finalWorkComponent1.SetComponent(new WorkComponent());
            finalWorkComponent1.DoWork();
            #endregion

            #region MailMessage
            GlobalSettings settings = LazySingleton.Instance;
            var message = MailMessageBuilder
                .Create()
                .From("test@yandex.ru")
                .To("to_sample@yandex.ru")
                .Subject("test subject")
                .Body("content")
                .Build();
            #endregion

            Console.ReadLine();
        }
    }
}