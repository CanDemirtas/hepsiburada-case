using HepsiburadaCase.Data.Abstract;
using HepsiburadaCase.Data.Entity;

namespace HepsiburadaCase.Data.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
