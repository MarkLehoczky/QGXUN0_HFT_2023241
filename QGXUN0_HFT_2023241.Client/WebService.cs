using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace QGXUN0_HFT_2023241.Client
{
    class WebService
    {
        private HttpClient client;

        public WebService(string url, string endpoint = "")
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

        public IEnumerable<T> GetList<T>(string endpoint)
        {
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();

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

        public T Delete<T>(string endpoint, object item)
        {
            HttpResponseMessage response = client.DeleteAsync($"{endpoint}/{item}").GetAwaiter().GetResult();

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
