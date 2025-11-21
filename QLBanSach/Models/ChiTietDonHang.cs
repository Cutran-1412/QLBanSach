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

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        public decimal DonGia { get; set; }
    }
}
