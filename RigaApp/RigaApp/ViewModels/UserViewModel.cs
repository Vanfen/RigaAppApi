using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RigaApp
{
    class UserViewModel
    {
        private const string WebApiServiceBaseAddress = "https://rigaappapi20191216120747.azurewebsites.net/";
        private const string WebApiServiceObjectAddress = "api/users";

        private readonly HttpClient _httpClient;

        public UserViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(WebApiServiceBaseAddress) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Login()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(WebApiServiceObjectAddress);
            
        }


    }
}
