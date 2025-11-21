using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLBanSach.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TaiKhoan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    VaiTro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    MaTheLoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenTheLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.MaTheLoai);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDonHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "FK_DonHang_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    MaGioHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.MaGioHang);
                    table.ForeignKey(
                        name: "FK_GioHang_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    MaSach = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenSach = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TacGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NhaXuatBan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NamXuatBan = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaTheLoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.MaSach);
                    table.ForeignKey(
                        name: "FK_Sach_TheLoai_MaTheLoai",
                        column: x => x.MaTheLoai,
                        principalTable: "TheLoai",
                        principalColumn: "MaTheLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MaChiTiet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaSach = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.MaChiTiet);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_Sach_MaSach",
                        column: x => x.MaSach,
                        principalTable: "Sach",
                        principalColumn: "MaSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHang",
                columns: table => new
                {
                    MaChiTiet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaGioHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaSach = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHang", x => x.MaChiTiet);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_GioHang_MaGioHang",
                        column: x => x.MaGioHang,
                        principalTable: "GioHang",
                        principalColumn: "MaGioHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_Sach_MaSach",
                        column: x => x.MaSach,
                        principalTable: "Sach",
                        principalColumn: "MaSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NguoiDung",
                columns: new[] { "MaNguoiDung", "DiaChi", "Email", "HoTen", "MatKhau", "SoDienThoai", "TaiKhoan", "VaiTro" },
                values: new object[,]
                {
                    { "ND00", "Nam Đinh", "txc@gamil.com", "Trần Xuân Cư", "2004", "0931412334", "Admin", true },
                    { "ND01", "Hà Nội", "nva@gmail.com", "Nguyễn Văn An", "123456", "0911111111", "user1", false },
                    { "ND02", "Đà Nẵng", "ttb@gmail.com", "Trần Thị Bình", "123456", "0922222222", "user2", false },
                    { "ND03", "TP. Hồ Chí Minh", "lvc@gmail.com", "Lê Văn Cương", "123456", "0933333333", "user3", false },
                    { "ND04", "Hải Phòng", "ptd@gmail.com", "Phạm Thị Dịu", "123456", "0944444444", "user4", false },
                    { "ND05", "Cần Thơ", "hvt@gmail.com", "Hoàng Văn Thành", "123456", "0955555555", "user5", false },
                    { "ND06", "Huế", "dtg@gmail.com", "Đặng Thị Giang", "123456", "0966666666", "user6", false },
                    { "ND07", "Nha Trang", "bvd@gmail.com", "Bùi Văn Dũng", "123456", "0977777777", "user7", false },
                    { "ND08", "Bắc Ninh", "vty@gmail.com", "Võ Thị Yến", "123456", "0988888888", "user8", false },
                    { "ND09", "Thanh Hóa", "pvd@gmail.com", "Phan Văn Duy", "123456", "0999999999", "user9", false },
                    { "ND10", "Vũng Tàu", "tttu@gmail.com", "Tạ Thị Tú", "123456", "0900000000", "user10", false }
                });

            migrationBuilder.InsertData(
                table: "TheLoai",
                columns: new[] { "MaTheLoai", "TenTheLoai" },
                values: new object[,]
                {
                    { "SKT", "Sách Kinh Tế" },
                    { "SPT", "Sách Phát Triển Bản Thân" },
                    { "STN", "Sách Thiếu Nhi" },
                    { "TLCT", "Sách Chính Trị" },
                    { "TLKNS", "Sách Kỹ Năng Sống" },
                    { "TLLS", "Sách Lịch Sử" },
                    { "TLNN", "Sách Ngoại Ngữ" },
                    { "TLPL", "Sách Pháp Luật" },
                    { "TLTH", "Sách Tin Học" },
                    { "TLYH", "Sách Y Học" },
                    { "TTDS", "Thưởng Thức Đời Sống" },
                    { "VHNN", "Văn Học Nước Ngoài" },
                    { "VHVN", "Văn Học Việt Nam" }
                });

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "MaSach", "Anh", "Gia", "MaTheLoai", "MoTa", "NamXuatBan", "NhaXuatBan", "SoLuong", "TacGia", "TenSach" },
                values: new object[,]
                {
                    { "KT1", "KT1.png", 120000m, "SKT", "Cuốn sách truyền cảm hứng về hành trình tìm đường, khám phá bản thân và vượt qua thử thách trong cuộc sống cũng như sự nghiệp.", 2019, "NXB Trẻ", 20, "Phan Văn Trường", "Một Đời Như Kẻ Tìm Đường" },
                    { "KT10", "KT10.png", 130000m, "SKT", "Tác phẩm kinh điển về tài chính cá nhân, giúp bạn thay đổi quan điểm về tiền bạc và đầu tư.", 2017, "NXB Trẻ", 30, "Robert T. Kiyosaki", "Dạy Con Làm Giàu – Tập 1: Cha Giàu Cha Nghèo" },
                    { "KT2", "KT2.png", 150000m, "SKT", "Sách hướng dẫn chi tiết cách sử dụng Facebook trong marketing, giúp người mới dễ dàng xây dựng chiến dịch quảng bá hiệu quả.", 2020, "NXB Hồng Đức", 18, "ThS. Quang Minh", "Hướng Dẫn Tự Học Facebook Marketing" },
                    { "KT3", "KT3.png", 135000m, "SKT", "Chia sẻ kinh nghiệm thực tế trong đàm phán, thương thuyết và giao tiếp kinh doanh từ một chuyên gia thương mại quốc tế.", 2021, "NXB Trẻ", 25, "Phan Văn Trường", "Một Đời Thương Thuyết" },
                    { "KT4", "KT4.png", 105000m, "SKT", "Tập hợp những bài viết sâu sắc, truyền cảm hứng về ý chí vươn lên, thái độ sống tích cực và tư duy làm giàu đúng đắn.", 2020, "NXB Tổng Hợp TP. Hồ Chí Minh", 22, "Nhà báo Hàn Ni", "Muốn Nghèo Cũng Khó Lắm Chứ Bộ" },
                    { "KT5", "KT5.png", 115000m, "SKT", "Bí quyết bán hàng hiệu quả, cách thu hút và giữ chân khách hàng, giúp bạn trở thành người bán hàng chuyên nghiệp.", 2017, "NXB Lao Động Xã Hội", 15, "Jeffrey J. Fox", "Để Trở Thành Người Bán Hàng Xuất Sắc" },
                    { "KT6", "KT6.png", 89000m, "SKT", "Cuốn sách kinh điển chia sẻ triết lý sống và nghệ thuật bán hàng giúp thay đổi tư duy và đạt thành công bền vững.", 2018, "NXB Trẻ", 25, "Og Mandino", "Người Bán Hàng Vĩ Đại Nhất Thế Giới" },
                    { "KT7", "KT7.png", 125000m, "SKT", "Một câu chuyện ý nghĩa về lãnh đạo – khi cho đi là cách tốt nhất để đạt được thành công và ảnh hưởng tích cực.", 2020, "NXB Trẻ", 18, "Bob Burg & John David Mann", "Nhà Lãnh Đạo Dám Cho Đi" },
                    { "KT8", "KT8.png", 160000m, "SKT", "Phân tích chiến lược đầu tư, kinh doanh và triết lý thành công của hai nhà đầu tư vĩ đại nhất thế giới.", 2019, "NXB Trẻ", 20, "Mark Tier", "Bí Quyết Đầu Tư Và Kinh Doanh Chứng Khoán Của Tỷ Phú Warren Buffett & George Soros" },
                    { "KT9", "KT9.png", 98000m, "SKT", "Cung cấp kiến thức, kỹ năng và tư duy cần thiết để bắt đầu khởi nghiệp thành công trong thời đại mới.", 2021, "NXB Hồng Đức", 25, "Lê Quang Biên", "Khởi Nghiệp – Con Đường Duy Nhất Giúp Bạn Giàu Có" },
                    { "NN1", "NN1.png", 85000m, "VHNN", "Tiểu thuyết khoa học viễn tưởng kinh điển kể về hành trình dưới biển cùng thuyền trưởng Nemo.", 2018, "Nhà xuất bản Kim Đồng", 20, "Jules Verne", "Hai vạn dặm dưới biển" },
                    { "NN10", "NN10.png", 95000m, "VHNN", "Tập truyện trinh thám kinh điển kể về thám tử Sherlock Holmes và bác sĩ Watson.", 2018, "Nhà xuất bản Văn Học", 20, "Arthur Conan Doyle", "Những cuộc phiêu lưu của Sherlock Holmes" },
                    { "NN2", "NN2.png", 70000m, "VHNN", "Câu chuyện phiêu lưu kỳ ảo của cô bé Alice trong thế giới tưởng tượng đầy màu sắc.", 2019, "Nhà xuất bản Văn Học", 25, "Lewis Carroll", "Alice ở xứ sở diệu kỳ" },
                    { "NN3", "NN3.png", 95000m, "VHNN", "Câu chuyện cảm động về tình yêu và cuộc sống, được chuyển thể thành phim nổi tiếng.", 2020, "Nhà xuất bản Văn Học", 18, "Nicola Yoon", "Nếu chỉ còn một ngày để sống" },
                    { "NN4", "NN4.png", 78000m, "VHNN", "Cuốn sách giới thiệu về môn thể thao phù thủy nổi tiếng trong thế giới Harry Potter.", 2017, "Nhà xuất bản Trẻ", 30, "J.K. Rowling", "Quidditch qua các thời đại" },
                    { "NN5", "NN5.png", 120000m, "VHNN", "Tác phẩm đoạt giải Pulitzer 2019, kể về mối quan hệ giữa con người và thiên nhiên.", 2021, "Nhà xuất bản Trẻ", 15, "Richard Powers", "Vòm rừng" },
                    { "NN6", "NN6.png", 82000m, "VHNN", "Tác phẩm nổi tiếng kể về hành trình sinh tồn của chú chó Buck trong vùng Alaska lạnh giá.", 2018, "Nhà xuất bản Kim Đồng", 22, "Jack London", "Tiếng gọi nơi hoang dã" },
                    { "NN7", "NN7.png", 88000m, "VHNN", "Tác phẩm kinh điển về hành trình phiêu lưu và nghị lực của cậu bé Rémi.", 2019, "Nhà xuất bản Kim Đồng", 25, "Hector Malot", "Không gia đình" },
                    { "NN8", "NN8.png", 65000m, "VHNN", "Tuyển tập truyện ngắn nổi tiếng, ca ngợi tình người và hy vọng trong nghịch cảnh.", 2016, "Nhà xuất bản Văn Học", 28, "O. Henry", "Chiếc lá cuối cùng" },
                    { "NN9", "NN9.png", 105000m, "VHNN", "Tập đầu tiên trong loạt truyện Harry Potter, mở ra thế giới phù thủy kỳ diệu.", 2020, "Nhà xuất bản Trẻ", 35, "J.K. Rowling", "Harry Potter và hòn đá phù thủy" },
                    { "PTBT1", "PTBT1.png", 115000m, "SPT", "Cuốn sách giúp bạn rèn luyện khả năng quản lý thời gian, thiết lập mục tiêu và duy trì sự tập trung để đạt hiệu suất cao trong cuộc sống.", 2021, "NXB Công Thương", 20, "Peg Dawson & Richard Guare", "Kỷ Luật Bản Thân" },
                    { "PTBT10", "PTBT10.png", 120000m, "SPT", "Khám phá bản chất tính cách con người, giúp bạn hiểu rõ chính mình để sống đúng với giá trị và khả năng riêng.", 2021, "NXB Trẻ", 20, "Brian R. Little", "Bạn Thật Sự Là Ai?" },
                    { "PTBT2", "PTBT2.png", 150000m, "SPT", "Tác phẩm truyền cảm hứng mạnh mẽ giúp bạn khám phá tiềm năng vô hạn của bản thân và kiểm soát cảm xúc, tài chính, mối quan hệ.", 2019, "NXB Tổng Hợp TP. Hồ Chí Minh", 25, "Anthony Robbins", "Đánh Thức Con Người Phi Thường Trong Bạn" },
                    { "PTBT3", "PTBT3.png", 130000m, "SPT", "John C. Maxwell chia sẻ 15 nguyên tắc cốt lõi giúp bạn trưởng thành, học hỏi và phát triển không ngừng trên hành trình cuộc sống.", 2020, "NXB Thế Giới", 22, "John C. Maxwell", "15 Nguyên Tắc Vàng Về Phát Triển Bản Thân" },
                    { "PTBT4", "PTBT4.png", 98000m, "SPT", "Cuốn sách rèn luyện khả năng giao tiếp, thấu hiểu người khác và giữ bình tĩnh trong mọi tình huống, nâng cao trí tuệ cảm xúc.", 2021, "NXB Thanh Niên", 30, "Trương Tiểu Hằng", "Nói Chuyện Là Bản Năng, Giữ Miệng Là Tu Dưỡng, Im Lặng Là Trí Tuệ" },
                    { "PTBT5", "PTBT5.png", 125000m, "SPT", "Brian Tracy chia sẻ bí quyết hình thành thói quen và tinh thần kỷ luật – nền tảng giúp bạn đạt được mục tiêu lớn lao.", 2018, "NXB Lao Động", 26, "Brian Tracy", "Kỷ Luật Bản Thân – Thói Quen Của Kẻ Mạnh" },
                    { "PTBT6", "PTBT6.png", 89000m, "SPT", "Cuốn sách tạo động lực, giúp bạn vượt qua trì hoãn, nỗ lực hành động và biến ước mơ thành hiện thực.", 2017, "NXB Văn Học", 28, "Vĩ Nhân", "Khi Bạn Đang Mơ Thì Người Khác Đang Nỗ Lực" },
                    { "PTBT7", "PTBT7.png", 95000m, "SPT", "Khuyến khích bạn không ngừng hoàn thiện bản thân, sống tích cực và phát triển toàn diện qua từng hành động nhỏ mỗi ngày.", 2020, "NXB Thanh Niên", 24, "Hứa Yên", "Trở Thành Phiên Bản Tốt Hơn Của Chính Mình" },
                    { "PTBT8", "PTBT8.png", 99000m, "SPT", "Giúp bạn nhận ra sức mạnh nội tại, phát huy sự tự tin và năng lượng tích cực để làm chủ cuộc sống.", 2018, "NXB Trẻ", 21, "Gillian Stokes", "Khám Phá Sức Mạnh Bản Thân" },
                    { "PTBT9", "PTBT9.png", 85000m, "SPT", "Tác phẩm kinh điển hướng dẫn cách đối nhân xử thế, xây dựng mối quan hệ và ảnh hưởng tích cực đến người khác.", 2016, "NXB Tổng Hợp TP. Hồ Chí Minh", 35, "Dale Carnegie", "Đắc Nhân Tâm" },
                    { "SCT1", "tu_tuong_hcm_dao_duc.jpg", 145000m, "TLCT", "Phân tích sâu sắc tư tưởng Hồ Chí Minh về đạo đức cách mạng, lối sống giản dị và tinh thần vì dân phục vụ.", 2019, "NXB Chính Trị Quốc Gia Sự Thật", 15, "PGS. TS. Phạm Ngọc Anh", "Tư Tưởng Hồ Chí Minh Về Đạo Đức Cách Mạng" },
                    { "SCT10", "nha_nuoc_phap_quyen.jpg", 185000m, "TLCT", "Phân tích và cập nhật quan điểm, chủ trương của Đảng về xây dựng và hoàn thiện Nhà nước pháp quyền XHCN Việt Nam.", 2023, "NXB Chính Trị Quốc Gia Sự Thật", 10, "Ban Nội Chính Trung Ương", "Xây Dựng Nhà Nước Pháp Quyền XHCN" },
                    { "SCT2", "duong_kach_menh.jpg", 125000m, "TLCT", "Tác phẩm kinh điển của Chủ tịch Hồ Chí Minh, trình bày tư tưởng cách mạng, con đường giải phóng dân tộc Việt Nam.", 2018, "NXB Chính Trị Quốc Gia Sự Thật", 20, "Nguyễn Ái Quốc", "Đường Kách Mệnh" },
                    { "SCT3", "cuong_linh_xay_dung.png", 135000m, "TLCT", "Văn kiện quan trọng của Đảng, định hướng phát triển đất nước trong thời kỳ đổi mới và hội nhập quốc tế.", 2021, "NXB Chính Trị Quốc Gia Sự Thật", 12, "Ban Chấp Hành Trung Ương Đảng", "Cương Lĩnh Xây Dựng Đất Nước Thời Kỳ Quá Độ Lên Chủ Nghĩa Xã Hội" },
                    { "SCT4", "lich_su_dang_csvn.jpg", 155000m, "TLCT", "Tổng hợp tiến trình hình thành, phát triển và những mốc son lịch sử của Đảng Cộng Sản Việt Nam.", 2020, "NXB Chính Trị Quốc Gia Sự Thật", 18, "Học Viện Chính Trị Quốc Gia Hồ Chí Minh", "Lịch Sử Đảng Cộng Sản Việt Nam" },
                    { "SCT5", "tu_tuong_hcm.png", 165000m, "TLCT", "Phân tích quan điểm tư tưởng của Hồ Chí Minh.", 2022, "NXB Lý Luận Chính Trị", 10, "TS. Nguyễn Văn Khánh", "Giáo Trình Tư Tưởng Hồ Chí Minh" },
                    { "SCT6", "chu_nghia_mac_lenin.jpg", 175000m, "TLCT", "Trình bày hệ thống lý luận Mác - Lênin.", 2021, "NXB Chính Trị Quốc Gia Sự Thật", 14, "GS. TS. Lê Hữu Nghĩa", "Giáo Trình Triết Học Mác-Lênin" },
                    { "SCT7", "van_kien_dai_hoi_xiii.png", 295000m, "TLCT", "Tập hợp đầy đủ văn kiện Đại hội XIII, bao gồm chiến lược phát triển đất nước giai đoạn 2021 - 2030.", 2021, "NXB Chính Trị Quốc Gia Sự Thật", 9, "Ban Chấp Hành Trung Ương Đảng", "Văn Kiện Đại Hội XIII Của Đảng" },
                    { "SCT8", "nguyen_ly_mac_lenin.jpg", 130000m, "TLCT", "Sách giáo trình chính thức cho sinh viên đại học, trình bày các nguyên lý cơ bản của chủ nghĩa Mác - Lênin.", 2019, "NXB Chính Trị Quốc Gia Sự Thật", 16, "Bộ Giáo Dục Và Đào Tạo", "Những Nguyên Lý Cơ Bản Của Chủ Nghĩa Mác - Lênin" },
                    { "SCT9", "hcm_toan_tap_t1.jpg", 235000m, "TLCT", "Tập hợp các bài viết, bài nói của Chủ tịch Hồ Chí Minh trong giai đoạn đầu hoạt động cách mạng.", 2020, "NXB Chính Trị Quốc Gia Sự Thật", 7, "Chủ tịch Hồ Chí Minh", "Hồ Chí Minh Toàn Tập - Tập 1" },
                    { "SKNS1", "7_thoi_quen_hieu_qua.jpg", 150000m, "TLKNS", "Cuốn sách kinh điển về phát triển bản thân, giúp xây dựng thói quen tích cực để thành công và hạnh phúc.", 2021, "NXB Trẻ", 20, "Stephen R. Covey", "7 Thói Quen Hiệu Quả" },
                    { "SKNS10", "thuc_tinh_muc_dich_song.jpg", 175000m, "TLKNS", "Giúp bạn tìm thấy ý nghĩa sâu sắc của cuộc sống và sống một cách tỉnh thức, bình an.", 2021, "NXB Hồng Đức", 10, "Eckhart Tolle", "Thức Tỉnh Mục Đích Sống" }
                });

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "MaSach", "Anh", "Gia", "MaTheLoai", "MoTa", "NamXuatBan", "NhaXuatBan", "SoLuong", "TacGia", "TenSach" },
                values: new object[,]
                {
                    { "SKNS2", "danh_thuc_con_nguoi_phi_thuong.jpg", 165000m, "TLKNS", "Hướng dẫn bạn khám phá tiềm năng vô hạn bên trong, kiểm soát cảm xúc và đạt được mục tiêu cuộc sống.", 2022, "NXB Trẻ", 18, "Anthony Robbins", "Đánh Thức Con Người Phi Thường Trong Bạn" },
                    { "SKNS3", "toi_tai_gioi_ban_cung_the.jpg", 140000m, "TLKNS", "Chia sẻ phương pháp học tập và tư duy tích cực giúp bạn trẻ phát huy tối đa năng lực bản thân.", 2020, "NXB Trẻ", 22, "Adam Khoo", "Tôi Tài Giỏi, Bạn Cũng Thế!" },
                    { "SKNS4", "quang_ganh_lo_di_va_vui_song.jpg", 135000m, "TLKNS", "Hướng dẫn cách vượt qua lo âu, sống tích cực và tận hưởng cuộc sống mỗi ngày.", 2021, "NXB Tổng Hợp TP.HCM", 25, "Dale Carnegie", "Quẳng Gánh Lo Đi Và Vui Sống" },
                    { "SKNS5", "lam_chu_tu_duy_thay_doi_van_menh.jpg", 160000m, "TLKNS", "Giúp bạn thay đổi suy nghĩ tiêu cực, xây dựng tư duy tích cực để đạt thành công lâu dài.", 2022, "NXB Trẻ", 15, "Adam Khoo", "Làm Chủ Tư Duy, Thay Đổi Vận Mệnh" },
                    { "SKNS6", "kheo_an_noi_se_co_duoc_thien_ha.jpg", 145000m, "TLKNS", "Dạy bạn nghệ thuật giao tiếp khéo léo, chinh phục lòng người trong công việc và cuộc sống.", 2020, "NXB Lao Động", 17, "Trác Nhã", "Khéo Ăn Nói Sẽ Có Được Thiên Hạ" },
                    { "SKNS7", "dung_lua_chon_an_nhan_khi_con_tre.jpg", 130000m, "TLKNS", "Khích lệ tinh thần dám sống, dám thử thách và nỗ lực không ngừng để đạt được ước mơ tuổi trẻ.", 2021, "NXB Thế Giới", 19, "Cảnh Thiên", "Đừng Lựa Chọn An Nhàn Khi Còn Trẻ" },
                    { "SKNS8", "suc_manh_cua_thoi_quen.jpg", 170000m, "TLKNS", "Giải thích khoa học về cách hình thành và thay đổi thói quen để cải thiện chất lượng cuộc sống.", 2023, "NXB Lao Động", 14, "Charles Duhigg", "Sức Mạnh Của Thói Quen" },
                    { "SKNS9", "nghe_thuat_song_tich_cuc.jpg", 155000m, "TLKNS", "Truyền cảm hứng sống tích cực, tin tưởng vào bản thân và luôn hướng đến điều tốt đẹp.", 2020, "NXB Hồng Đức", 12, "Norman Vincent Peale", "Nghệ Thuật Sống Tích Cực" },
                    { "SLS1", "lich_su_vn_den_the_ky_xix.jpg", 165000m, "TLLS", "Tổng quan lịch sử Việt Nam từ thời dựng nước đến cuối thế kỷ XIX, trình bày rõ quá trình hình thành dân tộc và văn hóa Việt.", 2018, "NXB Giáo Dục Việt Nam", 20, "PGS. TS. Trần Quốc Vượng", "Lịch Sử Việt Nam Từ Nguồn Gốc Đến Thế Kỷ XIX" },
                    { "SLS10", "lich_su_dong_nam_a.jpg", 175000m, "TLLS", "Trình bày lịch sử hình thành, phát triển và giao lưu văn hóa giữa các quốc gia Đông Nam Á qua các thời kỳ.", 2023, "NXB Đại Học Quốc Gia Hà Nội", 12, "Nguyễn Văn Hồng", "Lịch Sử Đông Nam Á" },
                    { "SLS2", "dai_viet_su_ky_toan_thu.jpg", 185000m, "TLLS", "Tác phẩm lịch sử kinh điển, ghi chép toàn bộ lịch sử các triều đại Việt Nam từ thời Hồng Bàng đến đầu nhà Lê.", 2019, "NXB Khoa Học Xã Hội", 15, "Ngô Sĩ Liên", "Đại Việt Sử Ký Toàn Thư" },
                    { "SLS3", "lich_su_the_gioi_co_dai.jpg", 150000m, "TLLS", "Trình bày sự ra đời và phát triển của các nền văn minh cổ đại như Ai Cập, Hy Lạp, La Mã, Trung Hoa, Ấn Độ.", 2020, "NXB Đại Học Quốc Gia Hà Nội", 18, "GS. Nguyễn Quang Ngọc", "Lịch Sử Thế Giới Cổ Đại" },
                    { "SLS4", "chien_tranh_vn_1945_1975.jpg", 175000m, "TLLS", "Phân tích các giai đoạn và sự kiện trọng đại trong hai cuộc kháng chiến chống Pháp và chống Mỹ của dân tộc Việt Nam.", 2021, "NXB Chính Trị Quốc Gia Sự Thật", 12, "TS. Lưu Văn Sỹ", "Chiến Tranh Việt Nam 1945-1975" },
                    { "SLS5", "lich_su_the_gioi_hien_dai.jpg", 190000m, "TLLS", "Khảo sát sự hình thành thế giới hiện đại, hai cuộc chiến tranh thế giới, Chiến tranh Lạnh và toàn cầu hóa.", 2022, "NXB Giáo Dục Việt Nam", 14, "PGS. TS. Trịnh Nhu", "Lịch Sử Thế Giới Hiện Đại" },
                    { "SLS6", "viet_su_giai_thoai.jpg", 160000m, "TLLS", "Tập hợp những giai thoại lịch sử hấp dẫn, phản ánh tinh thần và trí tuệ Việt Nam qua các thời kỳ.", 2021, "NXB Trẻ", 17, "Nguyễn Khắc Thuần", "Việt Sử Giai Thoại" },
                    { "SLS7", "lich_su_van_minh_the_gioi.jpg", 220000m, "TLLS", "Tác phẩm nổi tiếng thế giới, mô tả quá trình phát triển văn minh nhân loại qua các thời kỳ.", 2017, "NXB Văn Học", 10, "Will Durant", "Lịch Sử Văn Minh Thế Giới" },
                    { "SLS8", "lich_su_viet_nam.jpg", 155000m, "TLLS", "Tổng hợp toàn diện tiến trình lịch sử Việt Nam từ thời dựng nước, giữ nước đến thời kỳ hiện đại.", 2019, "NXB Khoa Học Xã Hội", 16, "PGS. TS. Phạm Văn Sơn", "Lịch Sử Việt Nam" },
                    { "SLS9", "lich_su_cac_trieu_dai.jpg", 140000m, "TLLS", "Ghi chép lịch sử chi tiết các triều đại phong kiến Việt Nam từ nhà Ngô, Đinh, Tiền Lê đến Nguyễn.", 2018, "NXB Văn Học", 20, "GS. Trần Trọng Kim", "Lịch Sử Các Triều Đại Việt Nam" },
                    { "SNN1", "eng1.jpg", 460000m, "TLNN", "Sách ngữ pháp tiếng Anh nổi tiếng dành cho người học từ trình độ cơ bản đến trung cấp.", 2019, "Cambridge University Press", 20, "Raymond Murphy", "English Grammar In Use (4th Edition)" },
                    { "SNN10", "han1.jpg", 145000m, "TLNN", "Giáo trình tiếng Hàn thông dụng cho người mới bắt đầu.", 2019, "NXB Văn Học", 14, "Trường Đại Học Ngôn Ngữ Quốc Gia Seoul", "Tiếng Hàn Tổng Hợp Dành Cho Người Việt - Sơ Cấp 1" },
                    { "SNN2", "eng2.jpg", 607000m, "TLNN", "Phiên bản mới nhất với bài tập và đáp án chi tiết.", 2021, "Cambridge University Press", 15, "Raymond Murphy", "English Grammar In Use Book With Answers (5th Edition)" },
                    { "SNN3", "eng_vn1.jpeg", 72000m, "TLNN", "Cuốn sách ngữ pháp tiếng Anh được biên soạn dành cho học sinh, sinh viên Việt Nam, có bài tập minh họa và đáp án.", 2020, "NXB Đại Học Quốc Gia Hà Nội", 25, "Mai Lan Hương", "Ngữ Pháp Tiếng Anh" },
                    { "SNN4", "eng_vn2.jpg", 109500m, "TLNN", "Sách ngữ pháp tiếng Anh căn bản, phiên bản tái bản 2019 – 532 trang, trình bày các điểm ngữ pháp thiết yếu cho người học.", 2019, "NXB Đại Học Quốc Gia Hà Nội", 18, "Trần Mạnh Tường", "Ngữ Pháp Tiếng Anh Căn Bản" },
                    { "SNN5", "ielts_vocab.jpg", 320000m, "TLNN", "Tài liệu luyện từ vựng IELTS theo từng chủ đề kèm bài tập.", 2017, "Cambridge University Press", 12, "Pauline Cullen", "Vocabulary For IELTS" },
                    { "SNN6", "BusinessEnglish.jpg", 248000m, "TLNN", "Sách hướng dẫn tiếng Anh giao tiếp công sở: chào hỏi, tự giới thiệu, cuộc họp, thuyết trình, đàm phán, từ vựng chuyên ngành.", 2021, "Nhân Trí Việt", 20, "Proud Poet Editorial Team", "Business English – Tiếng Anh cho người đi làm" },
                    { "SNN7", "3000words.jpg", 95000m, "TLNN", "Danh sách 3000 từ vựng cơ bản và nâng cao, dễ học nhớ lâu.", 2019, "NXB Đại Học Quốc Gia Hà Nội", 30, "The Windy", "3000 Từ Vựng Tiếng Anh Thông Dụng Nhất" },
                    { "SNN8", "trung1.jpg", 120000m, "TLNN", "Sách học tiếng Trung cơ bản cho người mới bắt đầu.", 2018, "NXB Đại Học Quốc Gia Hà Nội", 22, "Đại Học Ngôn Ngữ Bắc Kinh", "Giáo Trình Hán Ngữ 1" },
                    { "SNN9", "nhat1.jpg", 135000m, "TLNN", "Giáo trình tiếng Nhật cơ bản, bản dịch và ngữ pháp tiếng Việt.", 2020, "NXB Trẻ", 16, "3A Corporation", "Tiếng Nhật Cho Người Bắt Đầu (Minna No Nihongo I)" },
                    { "SPL1", "bo_luat_ds_2015.jpg", 155000m, "TLPL", "Bản in đầy đủ Bộ luật Dân sự năm 2015, quy định quan hệ dân sự, quyền và nghĩa vụ của cá nhân, tổ chức tại Việt Nam.", 2022, "NXB Chính trị Quốc gia Sự thật", 20, "Quốc Hội Việt Nam", "Bộ luật Dân sự Việt Nam 2015" },
                    { "SPL10", "hien_phap_2013.jpg", 105000m, "TLPL", "Văn bản pháp luật cao nhất, quy định chế độ chính trị, quyền và nghĩa vụ cơ bản của công dân, tổ chức bộ máy nhà nước.", 2021, "NXB Chính trị Quốc gia Sự thật", 30, "Quốc Hội Việt Nam", "Hiến pháp nước Cộng hòa Xã hội Chủ nghĩa Việt Nam 2013" },
                    { "SPL2", "bo_luat_hs_2015.jpg", 165000m, "TLPL", "Tập hợp toàn văn Bộ luật Hình sự năm 2015 và các sửa đổi năm 2017, quy định các tội phạm và hình phạt.", 2021, "NXB Chính trị Quốc gia Sự thật", 18, "Quốc Hội Việt Nam", "Bộ luật Hình sự Việt Nam 2015 (Sửa đổi 2017)" },
                    { "SPL3", "luat_dat_dai.jpg", 120000m, "TLPL", "Quy định quyền sở hữu, sử dụng đất đai, thủ tục cấp giấy chứng nhận quyền sử dụng đất và các tranh chấp đất đai.", 2020, "NXB Lao động", 22, "Quốc Hội Việt Nam", "Luật Đất đai 2013 (Sửa đổi, bổ sung 2018)" },
                    { "SPL4", "luat_hn_gd.jpg", 95000m, "TLPL", "Văn bản quy định quan hệ hôn nhân, gia đình, quyền và nghĩa vụ giữa vợ chồng, cha mẹ và con.", 2020, "NXB Chính trị Quốc gia Sự thật", 25, "Quốc Hội Việt Nam", "Luật Hôn nhân và Gia đình 2014" },
                    { "SPL5", "luat_dn_2020.jpg", 130000m, "TLPL", "Cập nhật đầy đủ Luật Doanh nghiệp 2020, hướng dẫn chi tiết về thành lập, tổ chức và quản lý doanh nghiệp tại Việt Nam.", 2021, "NXB Chính trị Quốc gia Sự thật", 15, "Quốc Hội Việt Nam", "Luật Doanh nghiệp 2020" },
                    { "SPL6", "luat_ld_2019.jpg", 110000m, "TLPL", "Quy định về hợp đồng lao động, quyền lợi, nghĩa vụ của người lao động và người sử dụng lao động.", 2020, "NXB Lao động", 20, "Quốc Hội Việt Nam", "Luật Lao động 2019" },
                    { "SPL7", "luat_gd_2019.png", 89000m, "TLPL", "Nêu rõ hệ thống giáo dục quốc dân, quyền và nghĩa vụ học tập của công dân, chính sách phát triển giáo dục.", 2020, "NXB Giáo dục Việt Nam", 17, "Quốc Hội Việt Nam", "Luật Giáo dục 2019" },
                    { "SPL8", "luat_bvmt_2020.png", 140000m, "TLPL", "Quy định các nguyên tắc, chính sách bảo vệ môi trường, trách nhiệm của cơ quan, tổ chức, cá nhân.", 2021, "NXB Tài nguyên Môi trường", 16, "Quốc Hội Việt Nam", "Luật Bảo vệ Môi trường 2020" },
                    { "SPL9", "luat_pctn.jpg", 135000m, "TLPL", "Văn bản quy định biện pháp phòng ngừa, phát hiện, xử lý tham nhũng và trách nhiệm của các tổ chức, cá nhân.", 2019, "NXB Chính trị Quốc gia Sự thật", 12, "Quốc Hội Việt Nam", "Luật Phòng chống tham nhũng 2018" },
                    { "STH1", "tin_hoc_co_ban.jpg", 95000m, "TLTH", "Sách nhập môn tin học: Windows, Word, Excel, Internet.", 2021, "NXB Giáo Dục", 20, "Nhiều tác giả", "Tin Học Cho Người Mới Bắt Đầu" },
                    { "STH10", "mos_powerpoint2019.jpg", 155000m, "TLTH", "Tài liệu luyện thi chứng chỉ MOS PowerPoint 2019 chính thức của IIG Việt Nam, cung cấp hướng dẫn chi tiết, bài tập thực hành và chiến lược làm bài hiệu quả.", 2020, "NXB Thông Tin Và Truyền Thông", 8, "IIG Việt Nam", "MOS PowerPoint 2019" },
                    { "STH2", "word2019.webp", 115000m, "TLTH", "Hướng dẫn chi tiết các chức năng của Word 2019, từ cơ bản đến nâng cao.", 2020, "NXB Thống Kê", 15, "Nguyễn Minh Hoàng", "Tự Học Microsoft Word 2019" },
                    { "STH3", "chatgpt_thucchien.jpg", 169000m, "TLTH", "Hướng dẫn thực chiến cách sử dụng ChatGPT – kỹ thuật prompt, ứng dụng AI trong công việc, viết nội dung, sáng tạo và tối ưu hiệu suất.", 2024, "NXB Dân Trí", 18, "Dịch Dương; Phan Trạch Bân; Lý Thế Minh", "ChatGPT Thực Chiến" }
                });

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "MaSach", "Anh", "Gia", "MaTheLoai", "MoTa", "NamXuatBan", "NhaXuatBan", "SoLuong", "TacGia", "TenSach" },
                values: new object[,]
                {
                    { "STH4", "khoahoccaptoc_ai.png", 189000m, "TLTH", "Cuốn sách giới thiệu nhanh và dễ hiểu về trí tuệ nhân tạo (AI), bao gồm các ứng dụng thực tế, học máy, ChatGPT và xu hướng công nghệ hiện nay.", 2024, "NXB Thanh Niên", 12, "Lê Quang Huy", "Khóa Học Cấp Tốc Về AI" },
                    { "STH5", "python_co_ban.webp", 159000m, "TLTH", "Sách nhập môn Python cho sinh viên CNTT, gồm lý thuyết, ví dụ minh họa và bài tập thực hành.", 2022, "NXB Thông Tin Và Truyền Thông", 14, "Nguyễn Văn Hiếu", "Lập Trình Python Cơ Bản" },
                    { "STH6", "java_co_ban.jpg", 175000m, "TLTH", "Tài liệu học Java từ cơ bản đến nâng cao: OOP, GUI, và kết nối cơ sở dữ liệu.", 2021, "NXB Thông Tin Và Truyền Thông", 10, "Phạm Hữu Khang", "Lập Trình Java Cơ Bản" },
                    { "STH7", "mos_excel2019.jpg", 165000m, "TLTH", "Tài liệu luyện thi chứng chỉ MOS Excel 2019, bao gồm hướng dẫn thao tác và bài tập mẫu.", 2020, "NXB Thông Tin Và Truyền Thông", 16, "IIG Việt Nam", "MOS Excel 2019" },
                    { "STH8", "sql_server.png", 210000m, "TLTH", "Sách lý thuyết và thực hành SQL Server: truy vấn, trigger, procedure và tối ưu hóa.", 2018, "NXB Khoa Học Và Kỹ Thuật", 9, "Nguyễn Thanh Tùng", "Hệ Quản Trị Cơ Sở Dữ Liệu SQL Server" },
                    { "STH9", "tin_hoc_van_phong.png", 99000m, "TLTH", "Word, Excel, PowerPoint dành cho sinh viên và nhân viên văn phòng.", 2020, "NXB Giáo Dục", 22, "Nguyễn Thị Thu Thủy", "Giáo Trình Tin Học Văn Phòng" },
                    { "SYH1", "giai_phau_nguoi.jpg", 245000m, "TLYH", "Kiến thức chi tiết về cấu trúc cơ thể người cho sinh viên và bác sĩ.", 2018, "NXB Y Học", 10, "GS. TS. Đỗ Xuân Hợp", "Giải Phẫu Người" },
                    { "SYH10", "dinh_duong_suc_khoe.png", 165000m, "TLYH", "Trình bày vai trò của dinh dưỡng trong duy trì sức khỏe và phòng ngừa bệnh.", 2023, "NXB Phụ Nữ Việt Nam", 16, "TS. Nguyễn Thị Thu Hà", "Dinh Dưỡng Và Sức Khỏe" },
                    { "SYH2", "sinh_ly_hoc.jpg", 198000m, "TLYH", "Các chức năng sinh lý của cơ thể người.", 2019, "NXB Y Học", 12, "Nguyễn Văn Chương", "Sinh Lý Học" },
                    { "SYH3", "duoc_ly_hoc.jpg", 225000m, "TLYH", "Kiến thức nền tảng về dược lý học, cơ chế tác dụng và tác dụng phụ.", 2020, "NXB Y Học", 15, "TS. Phạm Văn Dũng", "Dược Lý Học Lâm Sàng" },
                    { "SYH4", "benh_hoc_noi_khoa.jpg", 275000m, "TLYH", "Tổng hợp các bệnh lý nội khoa và hướng dẫn điều trị.", 2021, "NXB Y Học", 8, "PGS. TS. Trần Văn Ngọc", "Bệnh Học Nội Khoa" },
                    { "SYH5", "chan_doan_hinh_anh.jpg", 265000m, "TLYH", "Hướng dẫn đọc X-quang, CT, MRI trong chẩn đoán bệnh.", 2022, "NXB Y Học", 10, "PGS. TS. Nguyễn Duy Huề", "Chẩn Đoán Hình Ảnh" },
                    { "SYH6", "dieu_duong_co_ban.jpg", 185000m, "TLYH", "Quy trình chăm sóc và kỹ năng thực hành cơ bản cho điều dưỡng.", 2020, "NXB Y Học", 14, "Nguyễn Thị Lan", "Điều Dưỡng Cơ Bản" },
                    { "SYH7", "benh_truyen_nhiem.jpg", 195000m, "TLYH", "Phân tích các bệnh truyền nhiễm phổ biến và vắc xin.", 2021, "NXB Y Học", 9, "TS. Lê Minh Tuấn", "Bệnh Truyền Nhiễm" },
                    { "SYH8", "y_hoc_co_truyen.webp", 210000m, "TLYH", "Nguyên lý Đông y và trị liệu bằng thảo dược.", 2019, "NXB Dân Trí", 11, "Lương Y Nguyễn Văn Minh", "Y Học Cổ Truyền Toàn Tập" },
                    { "SYH9", "so_cuu_cap_cuu.webp", 155000m, "TLYH", "Hướng dẫn thực hành sơ cứu cho trẻ em.", 2022, "NXB Thanh Niên", 18, "BS. Trần Hữu Tài", "Cẩm Nang Sơ Cấp Cứu Trẻ Em" },
                    { "TN1", "TN1.png", 58000m, "STN", "Tác phẩm thiếu nhi đạt giải, kể về tuổi thơ vùng Thất Sơn.", 2020, "NXB Phụ Nữ Việt Nam", 20, "Phạm Công Luận", "Chú Bé Thất Sơn" },
                    { "TN10", "TN10.png", 78000m, "STN", "Phần trong bộ truyện dài nổi tiếng dành cho thiếu nhi.", 2020, "NXB Kim Đồng", 25, "Nguyễn Nhật Ánh", "Kính Vạn Hoa - Tập 5" },
                    { "TN2", "TN2.png", 72000m, "STN", "Phiêu lưu xuyên không gian đầy hấp dẫn.", 2018, "NXB Văn Học", 25, "Madeleine L'Engle", "Nếp Gấp Thời Gian" },
                    { "TN3", "TN3.png", 65000m, "STN", "Tác phẩm kinh điển về lòng nhân ái.", 2019, "NXB Văn Học", 30, "Edmondo De Amicis", "Những Tấm Lòng Cao Cả" },
                    { "TN4", "TN4.png", 85000m, "STN", "Câu chuyện triết lý nhẹ nhàng đầy cảm xúc.", 2015, "NXB Hội Nhà Văn", 40, "Antoine de Saint-Exupéry", "Hoàng Tử Bé" },
                    { "TN5", "TN5.png", 120000m, "STN", "Bách khoa tri thức hình ảnh cho trẻ.", 2021, "NXB Dân Trí", 15, "Kingfisher", "Bách Khoa Tri Thức Cho Trẻ Em" },
                    { "TN6", "TN6.png", 45000m, "STN", "Sách tương tác giáo dục cho trẻ 4-5 tuổi.", 2020, "NXB Kim Đồng", 35, "YOSBOOK - Beibei Xiong", "Mẹ Hỏi Bé Trả Lời" },
                    { "TN7", "TN7.png", 89000m, "STN", "Mùa hè hài hước của Greg Heffley.", 2022, "NXB Văn Học", 28, "Jeff Kinney", "Nhật Ký Chú Bé Nhút Nhát: Mùa Hè Tuyệt Vời" },
                    { "TN8", "TN8.png", 56000m, "STN", "Khám phá thế giới vi sinh vật.", 2021, "NXB Thanh Niên", 22, "Tôn Nguyên Vĩ", "10 Vạn Câu Hỏi Vì Sao – Vi Sinh Vật" },
                    { "TN9", "TN9.png", 56000m, "STN", "Tìm hiểu về các loài động vật bay.", 2021, "NXB Thanh Niên", 22, "Tôn Nguyên Vĩ", "10 Vạn Câu Hỏi Vì Sao – Động Vật Bay" },
                    { "TTDS1", "TTDS1.png", 85000m, "TTDS", "Hướng dẫn chăm sóc trẻ tự kỷ trong gia đình.", 2021, "NXB Phụ Nữ", 20, "Nguyễn Thanh Liêm", "Nuôi Dạy Trẻ Có Rối Loạn Phổ Tự Kỷ" },
                    { "TTDS10", "TTDS10.png", 56000m, "TTDS", "Giáo dục giới tính cho thiếu niên.", 2017, "NXB Kim Đồng", 30, "Koe Sung-ae, Kim Daeshik, Pang Myung-Geol", "Những Điều Cần Biết Về Giới Tính" },
                    { "TTDS2", "TTDS2.png", 78000m, "TTDS", "Sách hướng dẫn phụ huynh và trẻ cùng thực hành những bài học về lòng yêu thương và nhân ái mỗi ngày.", 2022, "Nhà Xuất Bản Phụ Nữ Việt Nam", 25, "Đậu Thị Nhung", "30 Ngày Thực Hành Lòng Yêu Thương" },
                    { "TTDS3", "TTDS3.png", 69000m, "TTDS", "Tổng hợp 100 bí quyết giúp cha mẹ nuôi dạy con trai tự tin, độc lập và có trách nhiệm.", 2018, "Nhà Xuất Bản Phụ Nữ", 30, "Khánh Ngọc", "100 Bí Quyết Nuôi Dạy Con Trai Thành Công" },
                    { "TTDS4", "TTDS4.png", 55000m, "TTDS", "Sách hướng dẫn trẻ em phát triển trí sáng tạo thông qua các hoạt động tạo hình thú cưng dễ thương.", 2020, "Nhà Xuất Bản Thế Giới", 40, "Sandy Trần", "Tạo Hình Thế Giới - Vật Nuôi Trong Nhà" },
                    { "TTDS5", "TTDS5.png", 65000m, "TTDS", "Giới thiệu về các loài động vật nuôi gần gũi trong cuộc sống, giúp trẻ hiểu biết và yêu quý thiên nhiên.", 2019, "Nhà Xuất Bản Kim Đồng", 35, "Nhiều tác giả", "Động Vật Nuôi" },
                    { "TTDS6", "TTDS6.png", 72000m, "TTDS", "Cung cấp kiến thức khoa học và tâm lý giúp các bạn gái hiểu rõ bản thân trong giai đoạn dậy thì.", 2019, "Nhà Xuất Bản Phụ Nữ", 25, "Hà Minh", "Cẩm Nang Tuổi Dậy Thì Dành Cho Bạn Gái" },
                    { "TTDS7", "TTDS7.png", 68000m, "TTDS", "Cuốn sách hướng dẫn trẻ em và phụ huynh đối thoại cởi mở, tích cực về giới tính và tình cảm.", 2021, "Nhà Xuất Bản Phụ Nữ Việt Nam", 28, "Võ Thị Minh Huệ", "Nói Chuyện Giới Tính Không Khó" },
                    { "TTDS8", "TTDS8.png", 72000m, "TTDS", "Cung cấp kiến thức giúp các bạn nam tuổi dậy thì hiểu và chăm sóc bản thân đúng cách.", 2019, "Nhà Xuất Bản Phụ Nữ", 25, "Hà Minh", "Cẩm Nang Tuổi Dậy Thì Dành Cho Bạn Trai" },
                    { "TTDS9", "TTDS9.png", 78000m, "TTDS", "Chia sẻ những thay đổi tâm sinh lý của tuổi mới lớn một cách gần gũi và hài hước.", 2020, "Nhà Xuất Bản Trẻ", 20, "Đỗ Hồng Ngọc", "Bỗng Nhiên Mà Họ Lớn" },
                    { "VH1", "VH1.png", 98000m, "VHVN", "Truyện dài khắc họa sinh động văn hóa và đời sống con người Tây Nguyên.", 2024, "NXB Trẻ", 10, "Trung Trung Đỉnh", "Con Thiêng Của Rừng" },
                    { "VH10", "VH10.png", 95000m, "VHVN", "Tác phẩm thiếu nhi kinh điển về hành trình phiêu lưu và trưởng thành.", 2020, "NXB Kim Đồng", 20, "Tô Hoài", "Dế Mèn Phiêu Lưu Ký" },
                    { "VH11", "VH11.png", 85000m, "VHVN", "Truyện ngắn tiêu biểu phản ánh hiện thực xã hội Việt Nam trước Cách mạng.", 2019, "NXB Văn Học", 15, "Nam Cao", "Chí Phèo" },
                    { "VH12", "VH12.png", 100000m, "VHVN", "Kiệt tác thi ca của đại thi hào Nguyễn Du.", 2020, "NXB Trẻ", 20, "Nguyễn Du", "Truyện Kiều" },
                    { "VH13", "VH13.png", 95000m, "VHVN", "Tác phẩm đậm chất suy tư và nhân sinh về tuổi trẻ.", 2023, "NXB Trẻ", 10, "Lê Khải Việt", "Khi Trẻ Người Ta Nghĩ Khác" },
                    { "VH14", "VH14.png", 90000m, "VHVN", "Tác phẩm trào phúng nổi tiếng châm biếm xã hội đương thời.", 2018, "NXB Văn Học", 10, "Vũ Trọng Phụng", "Số Đỏ" }
                });

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "MaSach", "Anh", "Gia", "MaTheLoai", "MoTa", "NamXuatBan", "NhaXuatBan", "SoLuong", "TacGia", "TenSach" },
                values: new object[,]
                {
                    { "VH15", "VH15.png", 95000m, "VHVN", "Tiểu thuyết hiện thực phê phán phản ánh nỗi khổ của người nông dân.", 2018, "NXB Văn Học", 10, "Ngô Tất Tố", "Tắt Đèn" },
                    { "VH16", "VH16.png", 87000m, "VHVN", "Truyện ngắn xúc động về thân phận con người trong nạn đói 1945.", 2018, "NXB Văn Học", 10, "Kim Lân", "Vợ Nhặt" },
                    { "VH17", "VH17.png", 100000m, "VHVN", "Tập thơ phản ánh tinh thần lạc quan cách mạng của Chủ tịch Hồ Chí Minh.", 2019, "NXB Văn Học", 12, "Hồ Chí Minh", "Nhật Ký Trong Tù" },
                    { "VH18", "VH18.png", 105000m, "VHVN", "Truyện dài nổi tiếng, câu chuyện cảm động về tuổi học trò và tình đầu.", 2021, "NXB Trẻ", 20, "Nguyễn Nhật Ánh", "Mắt Biếc" },
                    { "VH19", "VH19.png", 95000m, "VHVN", "Hai truyện vừa nổi tiếng, khắc họa thân phận con người miền Tây.", 2019, "NXB Trẻ", 18, "Nguyễn Ngọc Tư", "Cánh Đồng Bất Tận" },
                    { "VH2", "VH2.png", 99000m, "VHVN", "Câu chuyện cảm động về hành trình hội nhập của người Việt nơi đất khách.", 2024, "NXB Thế Giới", 10, "Cát Thảo Nguyễn", "Đến Nơi Rồi" },
                    { "VH20", "VH20.png", 87000m, "VHVN", "Tác phẩm phản ánh thân phận phụ nữ trong xã hội xưa.", 2019, "NXB Văn Học", 10, "Vũ Trọng Phụng", "Làm Đĩ" },
                    { "VH3", "VH3.png", 95000m, "VHVN", "Tản văn về những góc nhìn và suy tư của thế hệ trẻ hiện đại.", 2023, "NXB Trẻ", 8, "Lê Khải Việt", "Khi Trẻ Người Ta Nghĩ Khác" },
                    { "VH4", "VH4.png", 90000m, "VHVN", "Tiểu thuyết phản ánh hiện thực xã hội qua ngòi bút sắc sảo.", 2023, "NXB Trẻ", 9, "Đỗ Phấn", "Mặt Rỗng" },
                    { "VH5", "VH5.png", 120000m, "VHVN", "Hợp tuyển văn thơ, tranh và họa chào đón mùa xuân Ất Tỵ 2025.", 2025, "NXB Dân Trí", 15, "Nhiều Tác Giả", "Sách Tết Ất Tỵ 2025" },
                    { "VH6", "VH6.png", 88000m, "VHVN", "Truyện cảm động về tình mẹ con và sức mạnh của yêu thương.", 2023, "NXB Kim Đồng", 12, "Hải Anh & Pauline Guitton", "Sóng" },
                    { "VH7", "VH7.png", 85000m, "VHVN", "Tập tản văn sâu sắc về tình yêu, tuổi trẻ và chia ly.", 2022, "NXB Trẻ", 10, "Phan", "Trước Khi Chúng Ta Nói Lời Chia Tay" },
                    { "VH8", "VH8.png", 97000m, "VHVN", "Tập truyện ngắn giàu cảm xúc và chiều sâu nhân văn.", 2021, "NXB Phụ Nữ Việt Nam", 8, "Y Ban", "Trên Đỉnh Giời" },
                    { "VH9", "VH9.png", 92000m, "VHVN", "Tập truyện ngắn hiện đại, phản ánh đời sống đô thị và con người.", 2021, "NXB Phụ Nữ Việt Nam", 9, "Hồ Anh Thái", "Trượt Chân Trên Tầng Cao" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaDonHang",
                table: "ChiTietDonHang",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaSach",
                table: "ChiTietDonHang",
                column: "MaSach");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaGioHang",
                table: "ChiTietGioHang",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaSach",
                table: "ChiTietGioHang",
                column: "MaSach");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaNguoiDung",
                table: "DonHang",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_MaNguoiDung",
                table: "GioHang",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_MaTheLoai",
                table: "Sach",
                column: "MaTheLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "ChiTietGioHang");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "TheLoai");
        }
    }
}
