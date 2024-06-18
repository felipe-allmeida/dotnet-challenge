using BikeRental.Domain.Models.DeliveryRequestNotificationAggregate;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Data.QueryRepositories
{
    public class DeliveryRequestNotificationQueryRepository : IDeliveryRequestNotificationQueryRepository
    {
        private readonly BikeRentalContext _context;
        private readonly DbSet<DeliveryRequestNotification> _set;

        public DeliveryRequestNotificationQueryRepository(BikeRentalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = context.Set<DeliveryRequestNotification>();
        }

        public IQueryable<DeliveryRequestNotification> GetAll()
        {
            return _set.AsQueryable().AsNoTracking();
        }
    }
}
