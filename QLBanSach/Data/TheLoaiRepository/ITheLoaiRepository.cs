using QLBanSach.Models;

namespace QLBanSach.Data.TheLoaiRepository
{
    public interface ITheLoaiRepository
    {
        IEnumerable<TheLoai> GetAll();
        TheLoai GetById(string id);
        void Add(TheLoai theloai);
        void Update(TheLoai theloai);
        void Delete(TheLoai theloai);
        void Save();
    }
}
