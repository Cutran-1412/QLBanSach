using QLBanSach.Models;

namespace QLBanSach.Data.TheLoaiRepository
{
    public class TheLoaiRepository : ITheLoaiRepository
    {
        private readonly AppDbContext _context;

        public TheLoaiRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TheLoai> GetAll() => _context.TheLoai.ToList();
    }
}
