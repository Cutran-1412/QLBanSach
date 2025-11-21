using QLBanSach.Models;

namespace QLBanSach.Data.SachRepository
{
    public class SachRepository : ISacRepository
    {
        private readonly AppDbContext _context;

        public SachRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Sach sach)=>_context.Sach.Add(sach);

        public void Delete(Sach sach)=>_context.Sach.Remove(sach);

        public IEnumerable<Sach> GetAll()=>_context.Sach.ToList();

        public Sach GetById(string id) => _context.Sach.Find(id);

        public IEnumerable<Sach> GetByTl(string theloai)=>_context.Sach.Where(s=>s.MaTheLoai==theloai).ToList();

        public void Save()=>_context.SaveChanges();

        public void Update(Sach sach)=>_context.Sach.Update(sach);
    }
}
