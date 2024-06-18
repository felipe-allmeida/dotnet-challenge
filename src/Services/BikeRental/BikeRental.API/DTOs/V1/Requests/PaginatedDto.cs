namespace BikeRental.API.DTOs.V1.Requests
{
    public record PaginatedDto
    {
        public int Skip { get; init; } = 0;
        public int Take { get; init; } = 10;
    }
}
