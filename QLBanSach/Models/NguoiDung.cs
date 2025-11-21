using System.ComponentModel.DataAnnotations;

namespace QLBanSach.Models
{
    public class NguoiDung
    {
        [Key]
        [Required]
        [MaxLength(20)]
        public string MaNguoiDung { get; set; }


        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [MaxLength(100, ErrorMessage = "Tài khoản không được vượt quá 100 ký tự")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [MaxLength(200)]
        public string HoTen { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(11)]
        public string SoDienThoai { get; set; }

        [Required]
        [MaxLength(300)]
        public string DiaChi { get; set; }

        [Required]
        public bool VaiTro { get; set; }

        public virtual ICollection<GioHang>? GioHangs { get; set; }
        public virtual ICollection<DonHang>? DonHangs { get; set; }
    }
}
