using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanSach.Models
{
    public class DonHang
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string MaDonHang { get; set; }


        [Required]
        [StringLength(20)]
        public string MaNguoiDung { get; set; }

        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NguoiDung? NguoiDung { get; set; }

        [Required]
        public DateTime NgayDat { get; set; }

        [Required]
        [StringLength(100)]
        public string TrangThai { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0")]
        public decimal TongTien { get; set; }

        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}
