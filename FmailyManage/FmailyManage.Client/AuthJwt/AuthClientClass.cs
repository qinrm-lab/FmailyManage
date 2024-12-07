using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;

namespace AuthJwt.Client
{
    public class AuthClientClassCustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public AuthClientClassCustomAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigationManager) : base(provider, navigationManager)
        {
            provider.RequestAccessToken();
            /* ConfigureHandler(
                 //authorizedUrls:new[]
             );*/
        }

    }

    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        string name = @"d:\tt\log.txt";
        private static string? authToken = null;
        private static DateTime? authExpiry=DateTime.UtcNow;
        //Logger<TokenAuthenticationStateProvider> _logger;
        private HttpClient _http;

        public TokenAuthenticationStateProvider(HttpClient http)
        {
            _http = http;
            Console.WriteLine("in    token");
        }

        public async Task SetTokenAsync(string token, DateTime expiry = default)
        {
            if( token == null)
            {
                authToken = null;
                authToken = null;
            }
            else
            {
                authToken = token;
                authExpiry = expiry;
            }
           Console.WriteLine("token notify");
            //NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            await GetAuthenticationStateAsync();
        }
        public async Task<string> GetTokenAsync()
        {
            Console.WriteLine("gettoken   001");
            if(authExpiry != null)
            {
                Console.WriteLine("gettoken   002");
                if (authExpiry > DateTime.Now)
                {
                    Console.WriteLine($"gettoken   003  {authToken}");
                    return authToken;
                }
                else
                {
                    Console.WriteLine($"gettoken   004  token==={authToken}==");
                    //await SetTokenAsync(null);
                    authToken = null;
                }
            }
            Console.WriteLine("gettoken   021");
            return null;
        }

        public override  Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            Console.WriteLine("state    1");
            string token="";
            try
            {
                 token =  GetTokenAsync().Result;
                /*var identity = string.IsNullOrEmpty(token)
                    ? new ClaimsIdentity()
                    : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");*/
                Console.WriteLine($"state    2 x={token}");
            }catch(Exception ex)
            {
                Console.WriteLine($"token={ex.Message}");
            }
            _http.DefaultRequestHeaders.Authorization=null;
            var identity = new ClaimsIdentity();
            Console.WriteLine("state    3");

            if (! string.IsNullOrEmpty(token) )
            {
                identity= new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token);//token.Replace("\"", "")
            }
            Console.WriteLine("state    4");

            Console.WriteLine($"identity null={string.IsNullOrEmpty(token)} name={identity.Name} ok");
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            Console.WriteLine($"state    43 token={authToken}");
            NotifyAuthenticationStateChanged(Task.FromResult(state));
           // return state;
            return Task.FromResult<AuthenticationState>( state);
           // return new AuthenticationState(new ClaimsPrincipal(identity));
        }

       

        private static IEnumerable<Claim>? ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        /* public class CustomMessageHandler: AuthorizationMessageHandler
         {
             public CustomMessageHandler(IAccessTokenProvider accessTokenProvider)
         }*/

    }
}
