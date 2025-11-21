using QLBanSach.Models;

namespace QLBanSach.Data.SachRepository
{
    public interface ISacRepository
    {
        IEnumerable<Sach> GetAll();
        Sach GetById(string id);
        void Add(Sach sach);
        void Update(Sach sach);
        void Delete(Sach sach);
        void Save();
        IEnumerable<Sach> GetByTl(string theloai);
    }
}
