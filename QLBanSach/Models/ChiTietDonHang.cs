using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanSach.Models
{
    public class ChiTietDonHang
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string MaChiTiet { get; set; }

        [Required]
        [StringLength(20)]
        public string MaDonHang { get; set; }

        [ForeignKey(nameof(MaDonHang))]
        public virtual DonHang? DonHang { get; set; }

        [Required]
        [StringLength(20)]
        public string MaSach { get; set; }

        [ForeignKey(nameof(MaSach))]
        public virtual Sach? Sach { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

    }
}
