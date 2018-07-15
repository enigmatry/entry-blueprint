using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Api
{
    public class JsonHttpClient : IDisposable
    {
        private readonly HttpClient _client;

        public JsonHttpClient(HttpClient createClient)
        {
            _client = createClient;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            return await response.DeserializeWithStatusCodeCheckAsync<T>();
        }

        public async Task<T> GetAsync<T>(Uri uri,
            params KeyValuePair<string, string>[] parameters)
        {
            var resourceUri = uri.AppendParameters(parameters);

            HttpResponseMessage response = await _client.GetAsync(resourceUri);
            return await response.DeserializeWithStatusCodeCheckAsync<T>();
        }

        public async Task PutAsJsonAsync<T>(string uri, T content)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(uri, CreateJsonContent(content));
            await response.EnsureSuccessStatusCodeAsync();
        }

        public async Task<TResponse> PutAsJsonAsync<T, TResponse>(string uri, T content)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(uri, CreateJsonContent(content));
            return await response.DeserializeWithStatusCodeCheckAsync<TResponse>();
        }
         public async Task PostAsJsonAsync<T>(string uri, T content)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(uri, CreateJsonContent(content));
            await response.EnsureSuccessStatusCodeAsync();
        }

        public async Task<TResponse> PostAsJsonAsync<T, TResponse>(string uri, T content)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(uri, CreateJsonContent(content));
            return await response.DeserializeWithStatusCodeCheckAsync<TResponse>();
        }

        private static HttpContent CreateJsonContent(object content)
        {
            string json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}