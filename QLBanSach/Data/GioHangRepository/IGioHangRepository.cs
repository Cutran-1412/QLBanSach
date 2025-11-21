using QLBanSach.Models;

namespace QLBanSach.Data.GioHangRepository
{
    public interface IGioHangRepository
    {
        IEnumerable<GioHang> GetData();
        GioHang GetById(string id);
        void Add(GioHang giohang);
        void Update(GioHang giohang);
        void Delete(GioHang giohang);
        void Save();
        GioHang GetByIDND(string manguoidung);
    }
}
