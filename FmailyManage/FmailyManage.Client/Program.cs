using FamilyManage.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FmailyManage.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);


            Init.StartUp(ref builder);


            await builder.Build().RunAsync();
        }
    }
}
