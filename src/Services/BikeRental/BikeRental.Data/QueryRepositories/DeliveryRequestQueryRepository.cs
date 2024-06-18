using BikeRental.Domain.Models.DeliveryRequestAggregate;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Data.QueryRepositories
{
    public class DeliveryRequestQueryRepository : IDeliveryRequestQueryRepository
    {
        private readonly BikeRentalContext _context;
        private readonly DbSet<DeliveryRequest> _set;

        public DeliveryRequestQueryRepository(BikeRentalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = context.Set<DeliveryRequest>();
        }

        public IQueryable<DeliveryRequest> GetAll()
        {
            return _set.AsQueryable().AsNoTracking();
        }
    }
}
