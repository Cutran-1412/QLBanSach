using System.ComponentModel.DataAnnotations;

namespace QLBanSach.Models
{
    public class TheLoai
    {
        [Key]
        [Required(ErrorMessage ="Không để mã thể loại trống")]
        [StringLength(20)]
        [Display(Name ="Mã thể loại")]
        public string MaTheLoai { get; set; }

        [Required(ErrorMessage ="Không để tên mã thể loại trống")]
        [StringLength(100)]
        [Display(Name ="Tên thể loại")]
        public string TenTheLoai { get; set; }

        public virtual ICollection<Sach>? Sachs { get; set; }
    }
}
