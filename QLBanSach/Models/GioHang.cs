using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanSach.Models
{
    public class GioHang
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string MaGioHang { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNguoiDung { get; set; }
        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NguoiDung? NguoiDung { get; set; }

        public DateTime NgayTao { get; set; }
        [Required]
        public virtual ICollection<ChiTietGioHang>? ChiTietGioHangs { get; set; }
    }
}
