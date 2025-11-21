using QLBanSach.Models;

namespace QLBanSach.Data.ChiTietDonHangRepository
{
    public interface IChiTietDonHangRepository
    {
        IEnumerable<ChiTietDonHang> GetAll();

        ChiTietDonHang GetById(string maChiTiet);

        IEnumerable<ChiTietDonHang> GetByMaDonHang(string maDonHang);

        void Add(ChiTietDonHang chiTiet);

        void Update(ChiTietDonHang chiTiet);

        void Delete(ChiTietDonHang chiTiet);

        void Save();
    }
}
