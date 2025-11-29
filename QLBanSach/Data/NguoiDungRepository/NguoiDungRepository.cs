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

        public void Delete(NguoiDung nguoidung) => _context.Remove(nguoidung);

        public IEnumerable<NguoiDung> GetAll()=>_context.NguoiDung.ToList();

        public NguoiDung GetById(string id) => _context.NguoiDung.Find(id);

        public NguoiDung GetByName(string name) => _context.NguoiDung.FirstOrDefault(x => x.TaiKhoan == name);

        public IEnumerable<NguoiDung> GetPaged(int page, int pageSize)=> _context.NguoiDung
        .OrderBy(x => x.MaNguoiDung)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

        public List<NguoiDung> GetPagedFiltered(string keyword, string email, string sdt, string diachi, string vaitro, int page, int pageSize)
        {
            var query = _context.NguoiDung.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.TaiKhoan.Contains(keyword) || x.HoTen.Contains(keyword));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email));

            if (!string.IsNullOrEmpty(sdt))
                query = query.Where(x => x.SoDienThoai.Contains(sdt));

            if (!string.IsNullOrEmpty(diachi))
                query = query.Where(x => x.DiaChi.Contains(diachi));

            if (!string.IsNullOrEmpty(vaitro))
            {
                bool roleValue = vaitro == "true";
                query = query.Where(x => x.VaiTro == roleValue);
            }

            return query
                .OrderBy(x => x.MaNguoiDung)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalCount()=> _context.NguoiDung.Count();

        public int GetTotalCountFiltered(string keyword, string email, string sdt, string diachi, string vaitro)
        {
            var query = _context.NguoiDung.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.TaiKhoan.Contains(keyword) || x.HoTen.Contains(keyword));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email));

            if (!string.IsNullOrEmpty(sdt))
                query = query.Where(x => x.SoDienThoai.Contains(sdt));

            if (!string.IsNullOrEmpty(diachi))
                query = query.Where(x => x.DiaChi.Contains(diachi));

            if (!string.IsNullOrEmpty(vaitro))
            {
                bool roleValue = vaitro == "true";
                query = query.Where(x => x.VaiTro == roleValue);
            }

            return query.Count();
        }

        public bool LogIn(string user, string password)=> _context.NguoiDung.Any(x => x.TaiKhoan == user && x.MatKhau == password);

        public void Save() => _context.SaveChanges();

        public void Update(NguoiDung nguoidung) => _context.NguoiDung.Update(nguoidung);
        public bool EmailExists(string gmail)
        {
            return _context.NguoiDung.Any(x => x.Email == gmail);
        }

        public bool TaiKhoanExists(string taiKhoan)
        {
            return _context.NguoiDung.Any(x => x.TaiKhoan == taiKhoan);
        }
    }
}
