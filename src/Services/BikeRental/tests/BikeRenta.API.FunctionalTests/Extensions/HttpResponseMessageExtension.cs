using System.Text.Json;
using System.Text.Json.Nodes;

namespace BikeRenta.API.FunctionalTests.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static JsonNode? Deserialize(this HttpResponseMessage response, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<JsonNode>(response.Content.ReadAsStringAsync().Result, options);
        }

        public static T? Deserialize<T>(this HttpResponseMessage response, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result, options ?? API.JsonSerializerSettings);
        }
    }

}
