using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using ActivosFijosDotNetCore.Models;

namespace ActivosFijosDotNetCore.Services
{
    public class ApiCaller: IDisposable
    {
        private HttpClient _client=new HttpClient();
        public ApiCaller(IConfiguration configuration)
        {
            _client.BaseAddress =new Uri(configuration.GetValue<string>("ContabilidadURl"));
        }
        public async Task<bool> ConsumoContabilidad(ApiRequest request)
        {
            using (var response = await _client.PostAsJsonAsync("accounting/post", request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ApiResponse>();
                    if (apiResponse.Mensaje.ToLower().Equals("ok"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
