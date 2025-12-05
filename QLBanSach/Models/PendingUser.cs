namespace QLBanSach.Models
{
    public class PendingUser
    {
        public string TaiKhoan { get; set; }            // Tên đăng nhập
        public string MatKhau { get; set; }             // Mật khẩu (chưa hash)
        public string HoTen { get; set; }               // Họ tên
        public string Email { get; set; }               // Email
        public string SoDienThoai { get; set; }         // Số điện thoại
        public string DiaChi { get; set; }              // Địa chỉ
        public bool VaiTro { get; set; }                // Vai trò (false = user, true = admin)
        public string OTP { get; set; }                 // Mã OTP
        public DateTime OTPExpire { get; set; }
    }
}
