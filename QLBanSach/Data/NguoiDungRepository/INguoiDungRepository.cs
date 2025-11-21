using QLBanSach.Models;

namespace QLBanSach.Data.NguoiDungRepository
{
    public interface INguoiDungRepository
    {
        IEnumerable<NguoiDung> GetAll();
        NguoiDung GetByName(string name);
        void Add(NguoiDung NguoiDung);
        void Save();
        bool LogIn(string user,string password);
        
    }
}
