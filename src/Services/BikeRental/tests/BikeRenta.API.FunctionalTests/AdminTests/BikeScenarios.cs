using BikeRenta.API.FunctionalTests.Factories;
using BikeRenta.API.FunctionalTests.Utils;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BikeRenta.API.FunctionalTests.AdminTests
{
    [TestCaseOrderer("BikeRenta.API.FunctionalTests.Utils.PriorityOrderer", "BikeRenta.API.FunctionalTests")]
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
        public async Task Get_bikes_endpoint_should_return_OK_status_code()
        {
            var response = await _client.GetAsync("/api/v1/bikes");
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(1)]
        public async Task Get_bike_by_id_endpoint_should_return_OK_status_code()
        {
            var response = await _client.GetAsync(API.AdminArea.Bikes(1));
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(2)]
        public async Task Create_bike_endpoint_should_return_OK_status_code()
        {
            var response = await _client.PostAsJsonAsync("/api/v1/bikes", new
            {
                Plate = "TEST999",
                Model = "Honda",
                Year = 2024
            });
            response.EnsureSuccessStatusCode();

            var bike = await response.Content.ReadFromJsonAsync<dynamic>();
            _factory.Data.Add("bikeId", bike.id);
        }

        [Fact, TestPriority(3)]
        public async Task Update_bike_plate_endpoint_should_return_OK_status_code()
        {
            var bikeId = _factory.Data["bikeId"];
            var response = await _client.PatchAsJsonAsync($"/api/v1/bikes/{bikeId}/plate", new
            {
                Plate = "TEST998",
            });
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(4)]
        public async Task Delete_bike_endpoint_should_return_OK_status_code()
        {
            var bikeId = _factory.Data["bikeId"];
            var response = await _client.DeleteAsync($"/api/v1/bikes/{bikeId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
