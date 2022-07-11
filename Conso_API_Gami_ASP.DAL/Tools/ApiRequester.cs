using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Conso_API_Gami_ASP.DAL.Tools
{
    public class ApiRequester
    {
        public HttpClient client { get; private set; }
        private readonly string baseAdress;

        public ApiRequester(string baseAdress)
        {
            client = new HttpClient();
            this.baseAdress = baseAdress;
            client.BaseAddress = new Uri(baseAdress);
        }

        public TEntity Get<TEntity>(string url, string token = null)
        {
            if (!(string.IsNullOrWhiteSpace(token)))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using(HttpResponseMessage response = client.GetAsync(url).Result)
            {
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();

                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TEntity>(json);
            }
        }

        public bool Delete(string url, string token = null)
        {
            if (!(string.IsNullOrWhiteSpace(token)))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage response = client.DeleteAsync(url).Result)
            {
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                return true;
            }
        }

        public bool Post<TEntity>(string url, TEntity entity, string token = null)
        {
            if (!(string.IsNullOrWhiteSpace(token)))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string toSend = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(toSend, Encoding.UTF8, "application/json");

            using(HttpResponseMessage response = client.PostAsync(url, content).Result)
            {
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                return true;
            }
        }

        public bool Put<TEntity>(string url, TEntity entity, string token = null)
        {
            if (!(string.IsNullOrWhiteSpace(token)))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string toSend = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(toSend, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = client.PutAsync(url, content).Result)
            {
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                return true;
            }
        }
    }
}
