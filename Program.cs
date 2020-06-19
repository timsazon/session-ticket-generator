using CommandLine;
using Steamworks;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SessionTicketGenerator
{
    class Program
    {
        [Verb("steam")]
        public class SteamOptions
        {

            [Option('a', "app-id", Required = true, HelpText = "App ID.")]
            public uint AppId { get; set; }
        }

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<SteamOptions, object>(args)
                  .WithParsedAsync<SteamOptions>(async o =>
                  {
                      SteamClient.Init(o.AppId);
                      var ticket = await SteamUser.GetAuthSessionTicketAsync();

                      var hex = new StringBuilder(ticket.Data.Length * 2);
                      foreach (byte b in ticket.Data)
                          hex.AppendFormat("{0:x2}", b);

                      Console.WriteLine($"Session ticket = {hex}");

                      Console.WriteLine($"Press any key to cancel the ticket...");
                      Console.ReadKey();

                      SteamClient.Shutdown();
                  });
        }
    }
}
