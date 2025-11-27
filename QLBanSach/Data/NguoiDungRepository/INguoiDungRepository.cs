using QLBanSach.Models;

namespace QLBanSach.Data.NguoiDungRepository
{
    public interface INguoiDungRepository
    {
        IEnumerable<NguoiDung> GetAll();
        NguoiDung GetById(string id);
        NguoiDung GetByName(string name);
        void Add(NguoiDung NguoiDung);
        void Save();
        bool LogIn(string user,string password);
        void Update(NguoiDung nguoidung);
        void Delete(NguoiDung nguoidung);
        IEnumerable<NguoiDung> GetPaged(int page, int pageSize);
        int GetTotalCount();
    }
}
