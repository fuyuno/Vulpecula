using System;
using System.Diagnostics;

namespace Vulpecula.Streaming.Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Hoge();
            Console.ReadLine();
        }

        private static void Hoge()
        {
            var croudia = new Croudia("9045893ce86855d121bb9825ae907d2c79d38cc10f78827dfaaed09b3c57a5c5",
                "5b7a9a1bbe8069d27c335f59e578f9c4ba4f402d21be9d2da7b3fbe9ec879a04 ");
            Process.Start(croudia.OAuth.GetAuthorizeUrl());

            var token = croudia.OAuth.Token(Console.ReadLine());
            croudia.AccessToken = token.AccessToken;

            CroudiaStreaming.TimeSpan = TimeSpan.FromSeconds(10);

            foreach (var status in croudia.Statuses.GetPublicTimelineAsStreaming())
            {
                Console.WriteLine($"{status.User.Name} @{status.User.ScreenName}");
                Console.WriteLine(status.Text);
                Console.WriteLine("-------------------------------------------------------------------");
            }
        }
    }
}