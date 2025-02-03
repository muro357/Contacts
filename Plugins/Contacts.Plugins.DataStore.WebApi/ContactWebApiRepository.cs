using Contacts.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.WebApi
{
    public class ContactWebApiRepository : IContactRepository
    {
        
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        public ContactWebApiRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            
        }

        public async Task AddContactAsync(CoreBusiness.Contact contact)
        {
            string json = JsonSerializer.Serialize(contact,_serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts");
            await _httpClient.PostAsync(uri,content);
        }

        public async Task DeleteContactAsync(int contactId)
        {
            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
            await _httpClient.DeleteAsync(uri);
        }

        public async Task<List<CoreBusiness.Contact>> GetContactAsync(string filter)
        {
            var contacts = new List<CoreBusiness.Contact>();

            Uri uri = string.IsNullOrWhiteSpace(filter) ?
                new Uri($"{Constants.WebApiBaseUrl}/contacts") : 
                new Uri($"{Constants.WebApiBaseUrl}/contacts?s={filter}");

            var response = await _httpClient.GetAsync(uri);

            if(response.IsSuccessStatusCode) 
            { 
                string content = await response.Content.ReadAsStringAsync();
                contacts = JsonSerializer.Deserialize<List<CoreBusiness.Contact>>(content, _serializerOptions);
            }

            return contacts;
        }

        public async Task<CoreBusiness.Contact> GetContactByIdAsync(int contactId)
        {
            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
            Contact contact = null;
            var response = await _httpClient.GetAsync(uri);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                contact = JsonSerializer.Deserialize<Contact>(content, _serializerOptions);
            }

            return contact;

        }

        public async Task UpdateContactAsync(int contactId, CoreBusiness.Contact contact)
        {
            string json = JsonSerializer.Serialize(contact, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
            await _httpClient.PutAsync(uri, content);
        }
    }
}
