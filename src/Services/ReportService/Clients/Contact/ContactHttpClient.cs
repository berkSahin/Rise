using Newtonsoft.Json;
using ReportService.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportService.Clients.Contact
{
    public class ContactHttpClient : IContactHttpClient
    {
        #region Fields

        private readonly HttpClient _httpClient;

        #endregion Fields

        #region Ctor

        public ContactHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://contact/");
            _httpClient = httpClient;
        }

        #endregion Ctor

        #region Methods

        public async Task<IEnumerable<ContactDTO>> GetContactsAsync()
        {
            var response = await _httpClient.GetAsync("/Contact/GetContactsWithInfos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ContactDTO>>(content);
            }
            else
            {
                throw new HttpRequestException();
            }
        }

        #endregion Methods
    }
}