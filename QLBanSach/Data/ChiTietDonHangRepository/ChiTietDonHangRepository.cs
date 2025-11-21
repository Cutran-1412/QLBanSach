using QLBanSach.Models;

namespace QLBanSach.Data.ChiTietDonHangRepository
{
    public class ChiTietDonHangRepository :IChiTietDonHangRepository
    {
        private readonly AppDbContext _context;

        public ChiTietDonHangRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ChiTietDonHang> GetAll()=> _context.ChiTietDonHangs.ToList();
        public ChiTietDonHang GetById(string maChiTiet)=>_context.ChiTietDonHangs.Find(maChiTiet);

        public IEnumerable<ChiTietDonHang> GetByMaDonHang(string maDonHang)=> _context.ChiTietDonHangs
                           .Where(ct => ct.MaDonHang == maDonHang)
                           .ToList();

        public void Add(ChiTietDonHang chiTiet)=> _context.ChiTietDonHangs.Add(chiTiet);
        public void Update(ChiTietDonHang chiTiet)=>_context.ChiTietDonHangs.Update(chiTiet);
        public void Delete(ChiTietDonHang chiTiet)=>_context.ChiTietDonHangs.Remove(chiTiet);
        public void Save()=>_context.SaveChanges();
    }
}
