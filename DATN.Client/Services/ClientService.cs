using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DATN.Client.Services
{
    public class ClientService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private string _accessToken;

        public ClientService(IHttpContextAccessor httpContextAccessor, IConfiguration? configuration = null, HttpClient? client = null)
        {
            _httpContextAccessor = httpContextAccessor;
            _client = client ?? new HttpClient();
            _client.DefaultRequestHeaders.Clear();

            //if (httpContextAccessor.HttpContext != null)
            //{
            //    string accessToken = SessionHelper.GetObjectFromJson<string>(httpContextAccessor.HttpContext.Session, "AccessToken");

            //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //}

            _configuration = configuration;
            string ApiDomain = _configuration.GetValue<string>("AppSettings:ApiDomain");
            _client.BaseAddress = new Uri(ApiDomain);
        }

        public void SetAccessToken(string accessToken)
        {
            _accessToken = accessToken;

            if (!string.IsNullOrEmpty(_accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
        }


        public async Task<T?> Get<T>(string relativeUrl)
        {
            try
            {
                var res = await _client.GetAsync(relativeUrl);
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }
        public async Task<List<T>>GetList<T>(string url)
        {
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(data);
            }
            return null;
        }
        public async Task<T?> Post<T>(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PostAsync(relativeUrl, GetBody(data));

                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }
        public async Task<T?> Put<T>(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PutAsync(relativeUrl, GetBody(data));
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> Patch<T>(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PatchAsync(relativeUrl, GetBody(data));
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> Delete<T>(string relativeUrl)
        {
            try
            {
                var res = await _client.DeleteAsync(relativeUrl);
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }

        public async Task<string?> Get(string relativeUrl)
        {
            try
            {
                var res = await _client.GetAsync(relativeUrl);
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch
            {
                return default;
            }
        }

        public async Task<string?> Post(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PostAsync(relativeUrl, GetBody(data));
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return default;
            }
            
        }

        public async Task<string?> Patch(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PatchAsync(relativeUrl, GetBody(data));
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch
            {
                return default;
            }
        }

        public async Task<string?> Delete(string relativeUrl)
        {
            try
            {
                var res = await _client.DeleteAsync(relativeUrl);
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch
            {
                return default;
            }
        }

        private static StringContent? GetBody(object? data)
        {
            if (data == null) return null;
            var body = JsonConvert.SerializeObject(data);
            return new StringContent(body, Encoding.UTF8, "application/json");
        }

        internal Task<string?> Get<T>(string v, int id)
        {
            throw new NotImplementedException();
        }
    }
}
