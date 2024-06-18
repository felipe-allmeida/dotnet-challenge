using BuildingBlocks.Common;

namespace BikeRental.Domain.Models.DeliveryRequestNotificationAggregate
{
    public interface IDeliveryRequestNotificationRepository : IRepository<DeliveryRequestNotification>
    {
        Task<DeliveryRequestNotification?> GetDeliveryRequestNotification(Guid deliveryRequestId, long deliveryRiderId);
    }
}
