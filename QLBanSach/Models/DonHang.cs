using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace QLBanSach.Models
{
    public class DonHang
    {
        [Key]
        [Required]
        [StringLength(20)]
        [Display(Name = "Mã đơn hàng")]
        public string MaDonHang { get; set; }


        [Required]
        [StringLength(20)]
        [Display(Name ="Mã người dùng")]
        public string MaNguoiDung { get; set; }

        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NguoiDung? NguoiDung { get; set; }


        [Required(ErrorMessage = "Địa chỉ nhận hàng không được để trống")]
        [StringLength(200, ErrorMessage = "Địa chỉ nhận hàng tối đa 200 ký tự")]
        [Display(Name ="Địa chỉ nhận hàng")]
        public string DiaChiNhanHang { get; set; }

        [Required]
        [Display(Name ="Ngày đặt")]
        public DateTime NgayDat { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Trạng thái")]
        public string TrangThai { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0")]
        [Display(Name ="Tổng tiền")]
        public decimal TongTien { get; set; }

        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}
