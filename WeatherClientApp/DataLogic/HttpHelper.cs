using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace WeatherClientApp.DataLogic
{
    public static class HttpHelper
    {
        private readonly static HttpClient _client = new HttpClient();
        
        public static async Task<JobResult<TResponseContent>> ExecuteGetRequestAsync<TResponseContent>(
           string url,
           CancellationToken cancellationToken)
        {
            try
            {
                var response = await _client.GetAsync(url, cancellationToken).ConfigureAwait(false);
                HttpStatusCode? status = response.StatusCode;
                if (status == HttpStatusCode.OK)
                {
                    string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrEmpty(json))
                        return JobResult<TResponseContent>.CreateSuccessful(Deserialize<TResponseContent>(json));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return JobResult<TResponseContent>.CreateFailed();
            }

            return JobResult<TResponseContent>.CreateFailed();
        }

        private static T Deserialize<T>(string json)
        {
            if (json == null)
                throw new NotSupportedException();

            if (typeof(T) == typeof(string) && !json.StartsWith("\""))
                return (T)(object)json;

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
