using HepsiburadaCase.Data.Abstract;
using HepsiburadaCase.Data.Entity;

namespace HepsiburadaCase.Data.Repository {
    public class OrderRepository : BaseRepository<Order>, IOrderRepository {
        private readonly ApplicationDbContext _context;

        public OrderRepository (ApplicationDbContext context) : base (context) {
            _context = context;
        }
    }
}