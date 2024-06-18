using BikeRental.API.FunctionalTests.Factories;
using BikeRental.API.FunctionalTests.Utils;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BikeRental.API.FunctionalTests.AdminTests
{
    [TestCaseOrderer("BikeRental.API.FunctionalTests.Utils.PriorityOrderer", "BikeRental.API.FunctionalTests")]
    public class BikeScenarios : IClassFixture<MockedAuthWebApplicationFactory>
    {
        private readonly MockedAuthWebApplicationFactory _factory;
        private HttpClient _client;


        public BikeScenarios(MockedAuthWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: TestAuthHandler.AuthenticationScheme);
        }

        [Fact, TestPriority(1)]
        public async void Get_bikes_endpoint_should_return_OK_status_code()
        {
            var response = await _client.GetAsync(API.AdminArea.Bikes());
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(1)]
        public async void Get_bike_by_id_endpoint_should_return_OK_status_code()
        {
            var response = await _client.GetAsync(API.AdminArea.Bikes(1));
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(2)]
        public async void Create_bike_endpoint_should_return_OK_status_code()
        {
            var response = await _client.PostAsJsonAsync(API.AdminArea.Bikes(), new
            {
                Plate = "ZZZZ999",
                Model = "Honda",
                Year = 2024
            });
            response.EnsureSuccessStatusCode();

            var bike = await response.Content.ReadFromJsonAsync<JsonElement>();

            var bikeId = bike.GetProperty("id").GetInt64();

            _factory.Data.Add("bikeId", bikeId);
        }

        [Fact, TestPriority(3)]
        public async void Update_bike_plate_endpoint_should_return_OK_status_code()
        {
            var bikeId = _factory.Data["bikeId"];
            var response = await _client.PatchAsJsonAsync(API.AdminArea.BikePlate((long)bikeId), new
            {
                Plate = "ZZZZ998",
            });
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(4)]
        public async void Delete_bike_endpoint_should_return_OK_status_code()
        {
            var bikeId = _factory.Data["bikeId"];
            var response = await _client.DeleteAsync(API.AdminArea.Bikes((long)bikeId));
            response.EnsureSuccessStatusCode();
        }
    }
}
