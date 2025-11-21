using System.ComponentModel.DataAnnotations;

namespace QLBanSach.Models
{
    public class TheLoai
    {
        [Key]
        [Required(ErrorMessage ="Không để mã thể loại trống")]
        [StringLength(20)]
        public string MaTheLoai { get; set; }

        [Required(ErrorMessage ="Không để tên mã thể loại trống")]
        [StringLength(100)]
        public string TenTheLoai { get; set; }

        public virtual ICollection<Sach>? Sachs { get; set; }
    }
}
