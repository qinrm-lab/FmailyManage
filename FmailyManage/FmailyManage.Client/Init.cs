using FamilyManage.Shared;
using FamilyManage.Shared.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
//using Npgsql.Internal.TypeHandlers;
using Microsoft.Extensions.Http;
using FamilyManage.Client.ApiClient;


namespace FamilyManage.Client
{
    /// <summary>
    /// 启动时候初始化功能
    /// </summary>
     public static class Init
    {
        /// <summary>
        /// program启动时候的用户初始化代码尽量放这里
        /// </summary>
        public static void StartUp(ref WebAssemblyHostBuilder builder)
        {
            string baseAddress=builder.HostEnvironment.BaseAddress;
            builder.Services.AddScoped(sp => new MyHttp { BaseAddress = new Uri(baseAddress) });


            initJson(builder);

            CommonVars.InitVars();
            //var doc = XDocument.Load("index.xhml");
            //find all images

            InitAuthentication(ref builder);
        }

        /// <summary>
        /// 添加认证功能
        /// </summary>
        /// <param name="builder"></param>
        private static void InitAuthentication(ref WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddAuthenticationStateDeserialization();

        }

        /// <summary>
        /// 阻止json循环引用
        /// </summary>
        /// <param name="builder"></param>
        private static void initJson(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton< JsonSerializerOptions>(new JsonSerializerOptions
            {
                //防止http转换json的时候循环引用
                ReferenceHandler = ReferenceHandler.Preserve,
            });
          
         /*   builder.Services.AddTransient(sp => new SystemTextJsonHandler System.Text.Json.jsonh(sp.GetRequiredService<JsonSerializerOptions>(),));
            builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
                .AddTypedClient<HttpClient>()
                .();*/
        }
    }
}
