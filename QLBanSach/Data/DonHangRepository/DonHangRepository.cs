using Microsoft.CodeAnalysis.CSharp.Syntax;
using QLBanSach.Models;

namespace QLBanSach.Data.DonHangRepository
{
    public class DonHangRepository : IDonHangRepository
    {
        private readonly AppDbContext _context;

        public DonHangRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DonHang> GetAll()=>_context.DonHangs.ToList();

        public IEnumerable<DonHang> GetIDNguoiDung(string manguoidung)=>_context.DonHangs
                           .Where(dh => dh.MaNguoiDung == manguoidung)
                           .ToList();
        public void Add(DonHang donHang)=>_context.DonHangs.Add(donHang);

        public void Update(DonHang donHang)=>_context.DonHangs.Update(donHang);

        public void Delete(DonHang donHang)=>_context.DonHangs.Remove(donHang);

        public void Save()=>_context.SaveChanges();
    }
}
