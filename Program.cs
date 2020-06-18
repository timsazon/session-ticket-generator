using CommandLine;
using Steamworks;
using System;
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
                      SteamClient.Shutdown();

                      var hexTicket = BitConverter.ToString(ticket.Data).Replace("-", "");
                      Console.WriteLine($"Session ticket = {hexTicket}");
                  });
        }
    }
}
