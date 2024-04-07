using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QGXUN0_HFT_2023242.WPFClient.Services
{
    public class WebService
    {
        private HttpClient client;

        public WebService(string url = "http://localhost:43016/", string endpoint = "")
        {
            while (!Ping(url + endpoint)) ;

            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try { client.GetAsync(endpoint).GetAwaiter().GetResult(); }
            catch (HttpRequestException) { throw new ArgumentException("Endpoint is not available"); }
        }

        public T Get<T>(string endpoint)
        {
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public T Get<T>(string endpoint, object item)
        {
            HttpResponseMessage response = client.GetAsync($"{endpoint}/{item}").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<T> GetAsync<T>(string endpoint)
        {
            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<T> GetAsync<T>(string endpoint, object item)
        {
            HttpResponseMessage response = await client.GetAsync($"{endpoint}/{item}");

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public IEnumerable<T> GetList<T>(string endpoint)
        {
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<T>>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<IEnumerable<T>> GetListAsync<T>(string endpoint)
        {
            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<T>>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public T Post<T>(string endpoint, object item)
        {
            HttpResponseMessage response = client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public T Post<T>(string endpoint, params object[] items)
        {
            HttpResponseMessage response = client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<T> PostAsync<T>(string endpoint, object item)
        {
            HttpResponseMessage response = await client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<T> PostAsync<T>(string endpoint, params object[] items)
        {
            HttpResponseMessage response = await client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public T Put<T>(string endpoint, object item)
        {
            HttpResponseMessage response = client.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public T Put<T>(string endpoint, params object[] items)
        {
            HttpResponseMessage response = client.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<T> PutAsync<T>(string endpoint, object item)
        {
            HttpResponseMessage response = await client.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<T> PutAsync<T>(string endpoint, params object[] items)
        {
            HttpResponseMessage response = await client.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public T Delete<T>(string endpoint, object item)
        {
            HttpResponseMessage response = client.DeleteAsync($"{endpoint}/{item}").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<T> DeleteAsync<T>(string endpoint, object item)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{endpoint}/{item}");

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            else
                throw new ArgumentException(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        private bool Ping(string url)
        {
            try { new WebClient().DownloadData(url); return true; }
            catch { return false; }
        }
    }
}
