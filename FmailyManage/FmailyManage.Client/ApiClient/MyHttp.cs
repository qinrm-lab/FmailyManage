using static System.Net.Mime.MediaTypeNames;


using System.Text.Json;
using System.Net.Http;
using FamilyManage.Shared.Data;
using FamilyManage.Shared.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;


namespace FamilyManage.Client.ApiClient
{
    using Http = HttpClient;
   // [Inject]
    
    public class MyHttp : HttpClient
    {
        private const string address = "/api/template/save";
        private HttpClient Http { get; init; }

        public MyHttp(HttpClient httpClient)
        {
            Http=httpClient;    
        }
        public MyHttp()
        {
        }
        public MyHttp(HttpMessageHandler handler) : base(handler) 
        {
        }

        /* public async Task<HttpResponseMessage> PostTableAsync(GridTableHttp gth)
         {

          HttpResponseMessage result=await   Http.PostAsJsonAsync<GridTableHttp>(address, gth);
             try
             {
                 result.EnsureSuccessStatusCode();
                 if(result.StatusCode==HttpStatusCode.OK )
                 {
                    GridTableHttp? resultTable=await result.Content.ReadFromJsonAsync<GridTableHttp>();
                 if(resultTable==null)
                         return result
                 }
                 else
                 {
                     return result;
                 }
             }
             catch(Exception ex)
             {

             }
         }*/
    }
}
