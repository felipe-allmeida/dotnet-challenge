using BikeRental.API.FunctionalTests.Utils;
using System.Text;
using System.Text.Json;

namespace BikeRental.API.FunctionalTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task SignIn(this HttpClient src, string user_name, string password)
        {
            var httpResponse = await src.PostAsync(API.SignIn(), new { user_name, password }.ToStringContent(API.JsonSerializerSettings));

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = httpResponse.Deserialize();

                src.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response["access_token"].GetValue<string>());
            }
        }

        public static StringContent ToStringContent(this object data, JsonSerializerOptions? options = null)
        {
            var json = data == null ? string.Empty : JsonSerializer.Serialize(data, options);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static MultipartFormDataContent ToMultipartFormDataContent(this Dictionary<string, object> src)
        {
            var formDataBoundary = string.Format("----------{0:N}", Guid.NewGuid());
            var formData = new MultipartFormDataContent(formDataBoundary);

            foreach (var kvp in src)
            {
                if (kvp.Value is string)
                {
                    formData.Add(new StringContent((string)kvp.Value), kvp.Key);
                }
                else if (kvp.Value.GetType() == typeof(string[]))
                {
                    var arr = kvp.Value as string[];
                    foreach (var item in arr!)
                    {
                        formData.Add(new StringContent(item), kvp.Key);
                    }
                }
                else if (kvp.Value.GetType() == typeof(MockFile))
                {
                    var content = new ByteArrayContent(((MockFile)kvp.Value).Bytes);
                    formData.Add(content, kvp.Key, ((MockFile)kvp.Value).FileName);
                }
            }

            return formData;
        }
    }

}
