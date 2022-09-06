using CardService;

namespace Account.Helper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var res = PasswordUtils.CreatePasswordHash("12345");
            Console.ReadKey();
        }
    }
}