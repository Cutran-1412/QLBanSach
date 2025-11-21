using QLBanSach.Models;
using System.Xml.Linq;

namespace QLBanSach.Data.NguoiDungRepository
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly AppDbContext _context;

        public NguoiDungRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(NguoiDung NguoiDung)=>_context.NguoiDung.Add(NguoiDung);

        public IEnumerable<NguoiDung> GetAll()=>_context.NguoiDung.ToList();    

        public NguoiDung GetByName(string name) => _context.NguoiDung.FirstOrDefault(x => x.TaiKhoan == name);

        public bool LogIn(string user, string password)=> _context.NguoiDung.Any(x => x.TaiKhoan == user && x.MatKhau == password);

        public void Save() => _context.SaveChanges();
    }
}
