using System.Net.NetworkInformation;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using FamilyManage.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.IO;
using AuthJwt.Client;

using Microsoft.AspNetCore.Components;



namespace FamilyManage.Client.WebApi
{

    public class Family
    {
        [Inject]
        static HttpClient http { get; set; }

        private const string FamilyBase = "Family";

        public static async  Task<List<Family>?> Read(int id) =>await http.GetFromJsonAsync<List<Family>?>($"{FamilyBase}/Read/{id}");
    }

}
