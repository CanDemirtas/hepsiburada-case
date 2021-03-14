using HepsiburadaCase.Data.Abstract;
using HepsiburadaCase.Data.Entity;

namespace HepsiburadaCase.Data.Repository
{
    public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
    {
        private readonly ApplicationDbContext _context;

        public CampaignRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
