using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanSach.Models
{
    public class Sach
    {
        [Key]
        [MaxLength(20)]
        public string MaSach { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống")]
        [MaxLength(200)]
        public string TenSach { get; set; }

        [Required(ErrorMessage ="Tên tác giả không được để trống")]
        [MaxLength(100)]
        public string TacGia { get; set; }

        [Required(ErrorMessage = "Nhà xuất bản không được để trống")]
        [MaxLength(100)]
        public string NhaXuatBan { get; set; }

        [Range(1800, 2100, ErrorMessage = "Năm xuất bản không hợp lệ")]
        public int NamXuatBan { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public decimal Gia { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải >= 0")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage ="Mô tả không được để trống")]
        [MaxLength(255)]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Ảnh không được để trống")]
        [MaxLength(255)]
        public string Anh { get; set; }


        [Required(ErrorMessage ="Mã thể loại không được để trống")]
        [MaxLength(20)]
       
        public string MaTheLoai { get; set; }

        [ForeignKey(nameof(MaTheLoai))]
        public virtual TheLoai? TheLoai { get; set; }

        public virtual ICollection<ChiTietGioHang>? ChiTietGioHangs { get; set; }
        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}
