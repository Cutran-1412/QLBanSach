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

        public void Add(TheLoai theloai) => _context.TheLoai.Add(theloai);

        public void Delete(TheLoai theloai) => _context.TheLoai.Remove(theloai);

        public IEnumerable<TheLoai> GetAll() => _context.TheLoai.ToList();

        public TheLoai GetById(string id) => _context.TheLoai.Find(id);

        public void Save() => _context.SaveChanges();

        public void Update(TheLoai theloai) => _context.TheLoai.Update(theloai);
    }
}
