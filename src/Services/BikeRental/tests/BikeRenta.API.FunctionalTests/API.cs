using BikeRental.API.Infrastructure.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BikeRenta.API.FunctionalTests
{
    public static class API
    {
        public static JsonSerializerOptions JsonSerializerSettings
        {
            get
            {
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                    DictionaryKeyPolicy = new SnakeCaseNamingPolicy(),
                };

                jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new SnakeCaseNamingPolicy(), allowIntegerValues: true));

                return jsonSerializerOptions;
            }
        }

        public static string Accounts() => "api/v1/accounts";
        public static string SignIn() => "api/v1/accounts/signin";
        public static string RefreshToken() => "api/v1/accounts/refresh-token";
        public static string Delete() => "api/v1/accounts";

        public static class AdminArea 
        {
            public static string Bikes() => "api/v1/admin/bikes";
            public static string Bikes(long id) => $"api/v1/admin/bikes/{id}";
            public static string BikePlate(long id) => $"api/v1/admin/bikes/{id}/plate";

            public static string DeliveryRequests() => "api/v1/admin/delivery-requests";
            public static string DeliveryRequestNotifications(Guid id) => $"api/v1/admin/delivery-requests/{id}/notifications";
        }

        public static class UserArea
        {
            public static string DeliveryRiders() => "api/v1/user/delivery-riders";
            public static string DeliveryRiders(Guid id) => $"api/v1/user/delivery-riders/{id}";
            public static string DeliveryRidersMe() => $"api/v1/user/delivery-riders/me";
            public static string DeliveryRidersMeCnh() => $"api/v1/user/delivery-riders/me/cnh";

            public static string AcceptDeliveryRequest(Guid id) => $"api/v1/user/delivery-requests/{id}/accept";
            public static string FinishDeliveryRequest(Guid id) => $"api/v1/user/delivery-requests/{id}/finish";

            public static string Rentals() => "api/v1/user/rentals";
            public static string RentalInfo() => $"api/v1/user/rentals/info";
            public static string RentalStatus(Guid id) => $"api/v1/user/rentals/{id}/status";
        }

    }
}
