using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Data
{
    public static class InitializationData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Promotion
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion { Id = 1, Name = "Giảm giá 10%", Description = "Khuyến mãi giảm giá 10% cho tất cả các sản phẩm", Percent = 10, From = DateTime.Now, To = DateTime.Now.AddMonths(1), IsActive = false },
                new Promotion { Id = 2, Name = "Mua 1 tặng 1", Description = "Chương trình mua 1 tặng 1 cho sản phẩm đã chọn", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(2), IsActive = false },
                new Promotion { Id = 3, Name = "Miễn phí vận chuyển", Description = "Miễn phí vận chuyển cho đơn hàng trên 500,000 VNĐ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(3), IsActive = false },
                new Promotion { Id = 4, Name = "Tặng voucher 100,000 VNĐ", Description = "Nhận voucher trị giá 100,000 VNĐ cho mỗi đơn hàng", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(4), IsActive = false },
                new Promotion { Id = 5, Name = "Giảm 50% cho sản phẩm mới", Description = "Khuyến mãi giảm giá 50% cho tất cả sản phẩm mới ra mắt", Percent = 50, From = DateTime.Now, To = DateTime.Now.AddMonths(5), IsActive = false },
                new Promotion { Id = 6, Name = "Tặng quà cho đơn hàng trên 1,000,000 VNĐ", Description = "Nhận quà tặng hấp dẫn khi đặt hàng trên 1,000,000 VNĐ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(6), IsActive = false },
                new Promotion { Id = 7, Name = "Giảm 20% cho sản phẩm phụ kiện", Description = "Khuyến mãi giảm giá 20% cho tất cả sản phẩm phụ kiện", Percent = 20, From = DateTime.Now, To = DateTime.Now.AddMonths(7), IsActive = false },
                new Promotion { Id = 8, Name = "Đổi cũ lấy mới", Description = "Chương trình đổi sản phẩm cũ lấy sản phẩm mới với giá ưu đãi", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(8), IsActive = false },
                new Promotion { Id = 9, Name = "Mua hàng tặng phiếu mua hàng", Description = "Mua hàng tặng phiếu mua hàng trị giá 200,000 VNĐ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(9), IsActive = false },
                new Promotion { Id = 10, Name = "Đồng giảm giá 50%", Description = "Siêu sale đồng giảm 50% giá sản phẩm", Percent = 50, From = DateTime.Now, To = DateTime.Now.AddMonths(10), BannerUrl = "/Images/Component/BannerPromotion1.gif", IsActive = true },
                new Promotion { Id = 11, Name = "Sôi động cùng mùa Ơ-rô", Description = "Siêu giảm giá mùa Ơ-RÔ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(10), BannerUrl = "/Images/Component/BannerPromotion2.webp", IsActive = true }
            );
            #endregion
            #region Notification
            modelBuilder.Entity<Notification>().HasData(
                new Notification { NotificationId = 1, Content = "Welcome to our service!", Subject = "Welcome" },
                new Notification { NotificationId = 2, Content = "Your order has been shipped.", Subject = "Order Shipped" },
                new Notification { NotificationId = 3, Content = "Your order has been delivered.", Subject = "Order Delivered" },
                new Notification { NotificationId = 4, Content = "New promotions available!", Subject = "Promotions" },
                new Notification { NotificationId = 5, Content = "Your subscription is about to expire.", Subject = "Subscription Expiry" },
                new Notification { NotificationId = 6, Content = "We have updated our terms of service.", Subject = "Terms of Service Update" },
                new Notification { NotificationId = 7, Content = "Password reset request.", Subject = "Password Reset" },
                new Notification { NotificationId = 8, Content = "Your account has been activated.", Subject = "Account Activation" },
                new Notification { NotificationId = 9, Content = "Monthly newsletter available.", Subject = "Newsletter" },
                new Notification { NotificationId = 10, Content = "Your feedback is important to us.", Subject = "Feedback Request" }
            );
            #endregion
            #region NewFeed
            modelBuilder.Entity<NewFeed>().HasData(
                new NewFeed { NewFeedId = 1, ProductId = 101, Subject = "New Product Release", Content = "We are excited to announce the release of our new product." },
                new NewFeed { NewFeedId = 2, ProductId = 102, Subject = "Product Update", Content = "Our product has been updated with new features." },
                new NewFeed { NewFeedId = 3, ProductId = 103, Subject = "Special Offer", Content = "Enjoy a special offer on our latest product." },
                new NewFeed { NewFeedId = 4, ProductId = 104, Subject = "Limited Edition", Content = "Check out our new limited edition product." },
                new NewFeed { NewFeedId = 5, ProductId = 105, Subject = "Product Review", Content = "Read the latest review on our product." },
                new NewFeed { NewFeedId = 6, ProductId = 106, Subject = "Customer Feedback", Content = "We value your feedback on our product." },
                new NewFeed { NewFeedId = 7, ProductId = 107, Subject = "Product Recall", Content = "Important notice about a product recall." },
                new NewFeed { NewFeedId = 8, ProductId = 108, Subject = "Holiday Sale", Content = "Don't miss out on our holiday sale." },
                new NewFeed { NewFeedId = 9, ProductId = 109, Subject = "Product Launch Event", Content = "Join us for the launch event of our new product." },
                new NewFeed { NewFeedId = 10, ProductId = 110, Subject = "Product Survey", Content = "Participate in our product survey and win prizes." }
            );
            #endregion
            #region Magazine
            modelBuilder.Entity<Magazine>().HasData(
                new Magazine { MagazineId = 1, Image = "/Images/Magazine/Picture1.jpg", Caption = "Danh sách trúng thưởng - Tưng bừng \"Tháng Panasonic\": Cơ hội trúng 102 bộ quà giá trị lên đến 655 triệu đồng", CreateAt = DateTime.Now.AddDays(1), Status = MagazineStatus.Starting, Content = "<h2 style=\"margin-left:0px;\">Cơn sốt game chưa bao giờ hạ nhiệt! Bạn muốn chiến game \"cực phê\" với trải nghiệm đỉnh cao? Chọn ngay <a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh\">màn hình</a> chơi game ROG và nhận quà \"khủng\" - nâng cấp trải nghiệm, chiến thắng mọi thử thách!</h2><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:845/442;height:auto !important;width:auto;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/ROG.jpg\" alt=\"Chiến gam hay nhận quà đỉnh\" width=\"845\" height=\"442\"></p><h3 style=\"margin-left:0px;\">1. Thời gian khuyến mãi: Từ 13/08 đến 30/09/2024</h3><h3 style=\"margin-left:0px;\">2. Nội dung chương trình</h3><p style=\"margin-left:0px;\">Trong thời gian khuyến mãi, khi mua mua <a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k\">Màn hình Asus Gaming ROG Strix XG27ACS</a> tại Thế giới Di động và Điện máy XANH, bạn sẽ nhận ngay 01 <a href=\"https://rog.asus.com/vn/mice-mouse-pads/mice/ambidextrous/rog-strix-impact-iii-model/\">Chuột ROG STRIX IMPACT III</a>.</p><p style=\"margin-left:0px;\">Áp dụng cho cả mua online và offline.</p><p style=\"margin-left:0px;\">Chương trình có thể kết thúc sớm nếu hết quà.</p><p style=\"margin-left:0px;\">Hạn chót đăng ký nhận quà: 03/10/2024.</p><h3 style=\"margin-left:0px;\">3. Sản phẩm áp dụng</h3><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><img class=\"image_resized\" style=\"aspect-ratio:600/600;height:auto !important;width:150px;\" src=\"https://cdn.tgdd.vn/Products/Images/5697/325113/asus-xg27acs-27-inch-2k-thumb-600x600.jpg\" width=\"600\" height=\"600\"></a></p><figure class=\"image image_resized\" style=\"height:auto !important;width:40px !important;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><img style=\"aspect-ratio:40/40;\" src=\"https://cdn.tgdd.vn/ValueIcons/label-baohanh3nam.png\" width=\"40\" height=\"40\"></a></figure><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><span style=\"color:rgb(51,51,51);\"><strong>Asus Gaming 27 inch 2K XG27ACS</strong></span></a></p><p style=\"margin-left:0px;\">Ngừng kinh doanh</p><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\">Xem chi tiết</a></p><h3 style=\"margin-left:0px;\">4. Cách thức nhận quà</h3><p style=\"margin-left:0px;\">Click vào nút \"Đăng ký\" trên website sự kiện <a href=\"https://www.asus.com/vn/events/infoM/activity_ROGLCD\">https://www.asus.com/vn/events/infoM/activity_ROGLCD</a>.</p><p style=\"margin-left:0px;\">- Đính kèm hình ảnh hóa đơn mua hàng, phiếu thu, phiếu xuất kho có dấu xác nhận của cửa hàng, thể hiện rõ tên sản phẩm và ngày mua hàng trong thời gian chương trình diễn ra.</p><p style=\"margin-left:0px;\">- Đính kèm ảnh chụp số S/N của sản phẩm.</p><p style=\"margin-left:0px;\">- Điền serial của sản phẩm.</p><p style=\"margin-left:0px;\">- Điền Họ và tên người nhận quà.</p><p style=\"margin-left:0px;\">- Điền Địa chỉ nhận quà.</p><p style=\"margin-left:0px;\">- Điền Số điện thoại liên hệ.</p><p style=\"margin-left:0px;\">Hình chụp số S/N trên sản phẩm phải chụp kèm với hoá đơn, chỉ chấp nhận hình chụp số S/N trên sản phẩm, số S/N trên hộp không có hiệu lực.</p><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:732/786;height:auto !important;width:auto;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/image1%20%283%29.jpg\" alt=\"Bước 1\" width=\"732\" height=\"786\"></p><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:732/753;height:auto !important;width:100%;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/image2%20%283%29.jpg\" alt=\"Bước 2\" width=\"732\" height=\"753\"></p><h3 style=\"margin-left:0px;\">4. Thông tin lưu ý</h3><p style=\"margin-left:0px;\">- Trong trường hợp không thể đăng ký trên hệ thống, quý khách hàng vui lòng gửi thông tin nhận quà như bên dưới về địa chỉ Email: dangkyasus@gmail.com với tiêu đề: \"[Khuyen mai] LCD GAMING\".</p><p style=\"margin-left:0px;\">- ASUS sẽ kiểm tra và liên hệ gửi quà tới bạn trong 20 ngày làm việc (trừ thứ 7 &amp; Chủ nhật).</p><p style=\"margin-left:0px;\">- Chương trình không áp dụng đồng thời với các chương trình khuyến mãi khác.</p><p style=\"margin-left:0px;\">- Quà tặng không có giá trị quy đổi thành tiền mặt hoặc các giá trị khác tương đương.</p><p style=\"margin-left:0px;\">- Hóa đơn/ phiếu thu/ phiếu xuất kho phải có con dấu của cửa hàng bán lẻ.</p><p style=\"margin-left:0px;\">- Chương trình chỉ dành cho khách hàng mua lẻ từ cửa hàng, tất cả hóa đơn từ nhà phân phối đều không được tham gia chương trình khuyến mãi này.</p><p style=\"margin-left:0px;\">- Dung lượng hình ảnh hóa đơn tải lên hệ thống &lt;1Mb.</p><p style=\"margin-left:0px;\">- Trong trường hợp cần thiết nhằm bảo đảm quyền lợi khách hàng, phía ASUS sẽ cần thêm những thông tin khác để xác định khách mua hàng là chính xác như: hóa đơn đỏ, giấy tờ tùy thân có ảnh đại diện,...</p><p style=\"margin-left:0px;\">Đây là chương trình của ASUS không phải của dienmayxanh.com và thegioididong.com, mọi góp ý hay thắc mắc khác về chương trình, xin vui lòng post tại <a href=\"https://www.facebook.com/share/g/NB6YhgtTY4wDwQ47/?mibextid=K35XfP\">Hội linh kiện PC ASUS ROG Việt Nam</a>.</p>" },
                new Magazine { MagazineId = 2, Image = "/Images/Magazine/Picture2.jpg", Caption = "Mua ngay máy lạnh Daikin: Thêm năm bảo hành, an tâm chất \"Nhật\"", CreateAt = DateTime.Now.AddDays(2), Status = MagazineStatus.Starting, Content = "Content 2" },
                new Magazine { MagazineId = 3, Image = "/Images/Magazine/Picture3.jpg", Caption = "Mua sớm máy lạnh - Tặng 2 lần vệ sinh chỉ có tại Điện máy XANH | Mới 2024", CreateAt = DateTime.Now.AddDays(3), Status = MagazineStatus.Starting, Content = "Content 3" },
                new Magazine { MagazineId = 4, Image = "/Images/Magazine/Picture4.jpg", Caption = "Khai lộc thăng hạng, vượt đỉnh thăng hoa: Mua Tivi LG nhận ưu đãi đến 51.000.000đ cùng gói ứng dụng giải trí hấp dẫn", CreateAt = DateTime.Now.AddDays(4), Status = MagazineStatus.Starting, Content = "Content 4" }
            );
            #endregion
            #region Language
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "English" },
                new Language { Id = 2, Name = "VietNam" }
            );
            #endregion
            #region Contact
            modelBuilder.Entity<Contact>().HasData(
                new Contact { ContactId = 1, Email = "example1@gmail.com", Phone = "0912345678", Address = "123 Main St, City A", IsRead = false },
                new Contact { ContactId = 2, Email = "example2@gmail.com", Phone = "0912345679", Address = "456 Oak St, City B", IsRead = true },
                new Contact { ContactId = 3, Email = "example3@gmail.com", Phone = "0912345680", Address = "789 Pine St, City C", IsRead = false },
                new Contact { ContactId = 4, Email = "example4@gmail.com", Phone = "0912345681", Address = "101 Maple St, City D", IsRead = true },
                new Contact { ContactId = 5, Email = "example5@gmail.com", Phone = "0912345682", Address = "202 Birch St, City E", IsRead = false },
                new Contact { ContactId = 6, Email = "example6@gmail.com", Phone = "0912345683", Address = "303 Cedar St, City F", IsRead = true },
                new Contact { ContactId = 7, Email = "example7@gmail.com", Phone = "0912345684", Address = "404 Elm St, City G", IsRead = false },
                new Contact { ContactId = 8, Email = "example8@gmail.com", Phone = "0912345685", Address = "505 Ash St, City H", IsRead = true },
                new Contact { ContactId = 9, Email = "example9@gmail.com", Phone = "0912345686", Address = "606 Cherry St, City I", IsRead = false },
                new Contact { ContactId = 10, Email = "example10@gmail.com", Phone = "0912345687", Address = "707 Spruce St, City J", IsRead = true }
            );
            #endregion
            #region AppUser
            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.Parse("2753c921-2304-4f8d-b8d5-75229d3b20d6"),
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    FullName = "Admin",
                    Dob = new DateTime(1990, 1, 1),
                    Address = "123 Main St, City A",
                    Description = "Admin",
                    LastLoginTime = DateTime.UtcNow,
                    isActive = true
                },
                new AppUser
                {
                    Id = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"),
                    UserName = "customer@gmail.com",
                    NormalizedUserName = "CUSTOMER@GMAIL.COM",
                    Email = "customer@gmail.com",
                    NormalizedEmail = "CUSTOMER@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    FullName = "Customer",
                    Dob = new DateTime(1991, 2, 2),
                    Address = "456 Oak St, City B",
                    Description = "Customer",
                    LastLoginTime = DateTime.UtcNow,
                    isActive = true
                }
                , new AppUser
                {
                    Id = Guid.Parse("AA7C5218-4F1E-4AC6-A3B4-08DCB162E29E"),
                    UserName = "customer2@gmail.com",
                    NormalizedUserName = "CUSTOMER2@GMAIL.COM",
                    Email = "customer2@gmail.com",
                    NormalizedEmail = "CUSTOMER2@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    FullName = "Customer",
                    Dob = new DateTime(1991, 2, 2),
                    Address = "456 Oak St, City B",
                    Description = "Customer",
                    LastLoginTime = DateTime.UtcNow,
                    isActive = true
                }

            // Add more users as needed
            );
            #endregion
            #region Address
            modelBuilder.Entity<Address>().HasData(
                new Address { AddressID = 1, Description = "Hà Nội" },
                new Address { AddressID = 2, Description = "Hồ Chí Minh" },
                new Address { AddressID = 3, Description = "Đà Nẵng" }
            );
            #endregion
            #region Brand
            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, Name = "LG", Status = true, ImageUrl = "/Images/Brand/Lg.png" },
                new Brand { BrandId = 2, Name = "Samsung", Status = true, ImageUrl = "/Images/Brand/Samsung.png" },
                new Brand { BrandId = 3, Name = "Toshiba", Status = true, ImageUrl = "/Images/Brand/Toshiba.png" },
                new Brand { BrandId = 4, Name = "Sony", Status = true, ImageUrl = "/Images/Brand/Sony.png" },
                new Brand { BrandId = 5, Name = "Aqua", Status = true, ImageUrl = "/Images/Brand/Aqua.png" },
                new Brand { BrandId = 6, Name = "Xiaomi", Status = true, ImageUrl = "/Images/Brand/Xiaomi.png" }
            );
            #endregion
            #region Category
            modelBuilder.Entity<Category>().HasData(
                // Category level 0
                new Category { Id = 1, Name = "Hàng cao cấp", Level = 0, ImageUrl = "/Images/Home/Luxury.png", IsOnList = true },
                new Category { Id = 2, Name = "Tivi, Loa, dàn karaoke", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 3, Name = "Tủ lạnh, Tủ đông, Tủ mát", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 4, Name = "Máy giặt, Máy sấy quần áo", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 5, Name = "Máy lạnh, Máy nước nóng", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 6, Name = "Điện gia dụng, Sinh tố, Xay ép", Level = 0, ImageUrl = "/Images/Home/Icons-1.png", IsOnList = true },
                new Category { Id = 7, Name = "Bếp điện, Nồi cơm, Đồ bếp", Level = 0, ImageUrl = "/Images/Home//Hot.png", IsOnList = true },
                new Category { Id = 8, Name = "Máy lọc nước, Nồi chiên", Level = 0, ImageUrl = "/Images/Home//Hot.png", IsOnList = true },
                new Category { Id = 9, Name = "Máy hút bụi, Máy rửa chén", Level = 0, ImageUrl = "/Images/Home/Icons-2.png", IsOnList = true },
                new Category { Id = 10, Name = "Xe đạp, Sức khỏe, Làm đẹp", Level = 0, ImageUrl = "/Images/Home/Icons-3.png", IsOnList = true },
                new Category { Id = 11, Name = "Đồ nghề - dụng cụ sửa chữa", Level = 0, ImageUrl = "/Images/Home/Icons-4.png", IsOnList = true },
                new Category { Id = 12, Name = "Điện thoại, Laptop, Tablet", Level = 0, ImageUrl = "/Images/Home/Icons-5.png", IsOnList = true },
                new Category { Id = 13, Name = "Phụ kiện, Camera, Đồng hồ", Level = 0, ImageUrl = "/Images/Home/Icons-6.png", IsOnList = true },
                new Category { Id = 14, Name = "Máy cũ, Dịch vụ hữu ích", Level = 0, ImageUrl = "/Images/Home/Icons-7.png", IsOnList = true },

                // Category level 1
                new Category { Id = 15, Name = "Tivi", ParentCategoryId = 2, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 16, Name = "Loa", ParentCategoryId = 2, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 17, Name = "Phụ kiện Tivi", ParentCategoryId = 2, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 18, Name = "Tủ lạnh", ParentCategoryId = 3, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 19, Name = "Tủ đông", ParentCategoryId = 3, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 20, Name = "Tủ mát", ParentCategoryId = 3, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 21, Name = "Máy giặt", ParentCategoryId = 4, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 22, Name = "Máy sấy quần áo", ParentCategoryId = 4, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 23, Name = "Phụ kiện máy giặt", ParentCategoryId = 4, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 24, Name = "Máy lạnh", ParentCategoryId = 5, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 25, Name = "Máy nước nóng", ParentCategoryId = 5, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 26, Name = "Máy xay các loại", ParentCategoryId = 6, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 27, Name = "Máy ép trái cây", ParentCategoryId = 6, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 28, Name = "Điện gia dụng", ParentCategoryId = 6, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 29, Name = "Nồi", ParentCategoryId = 6, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 30, Name = "Bếp", ParentCategoryId = 6, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 31, Name = "Lò", ParentCategoryId = 6, IsVisible = false, Level = 1, IsOnList = true },
                // Category level 2

                new Category { Id = 32, Name = "Màn hình cong", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 33, Name = "Màn hình phẳng", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 34, Name = "Siêu mỏng", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 35, Name = "Tivi cao cấp", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 36, Name = "Tivi thiết kế đặc biệt", ParentCategoryId = 15, Level = 2, IsOnList = false },
                new Category { Id = 37, Name = "Loa kéo", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 38, Name = "Loa kéo điện", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 39, Name = "Loa karaoke xách tay", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 40, Name = "Loa bluetooth", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 41, Name = "Loa thanh (SoundBar)", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 42, Name = "Dàn Karaoke, Amply", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 43, Name = "Dàn âm thanh", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 44, Name = "Micro", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 45, Name = "Loa thùng", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 46, Name = "Loa mini", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 47, Name = "Loa đồ chơi", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 48, Name = "Cáp HDMI", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 49, Name = "Khung treo Tivi", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 50, Name = "Điều khiển Tivi", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 51, Name = "Android TV Box", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 52, Name = "Dán màn Tivi", ParentCategoryId = 17, Level = 2, IsOnList = false },
                new Category { Id = 53, Name = "Sticker Tivi", ParentCategoryId = 17, Level = 2, IsOnList = false }
                // Thêm các danh mục khác nếu cần
            );
            #endregion
            #region Origin
            modelBuilder.Entity<Origin>().HasData(
                new Origin { Id = 1, Name = "Trung Quốc", Description = "Xuất xứ: Trung Quốc", CreateAt = DateTime.Now },
                new Origin { Id = 2, Name = "Hàn Quốc", Description = "Xuất xứ: Hàn Quốc", CreateAt = DateTime.Now },
                new Origin { Id = 3, Name = "Mỹ", Description = "Xuất xứ: Mỹ", CreateAt = DateTime.Now },
                new Origin { Id = 4, Name = "Nhật Bản", Description = "Xuất xứ: Nhật Bản", CreateAt = DateTime.Now },
                new Origin { Id = 5, Name = "Đức", Description = "Xuất xứ: Đức", CreateAt = DateTime.Now },
                new Origin { Id = 6, Name = "Anh", Description = "Xuất xứ: Anh", CreateAt = DateTime.Now },
                new Origin { Id = 7, Name = "Pháp", Description = "Xuất xứ: Pháp", CreateAt = DateTime.Now },
                new Origin { Id = 8, Name = "Việt Nam", Description = "Xuất xứ: Việt Nam", CreateAt = DateTime.Now },
                new Origin { Id = 9, Name = "Úc", Description = "Xuất xứ: Úc", CreateAt = DateTime.Now },
                new Origin { Id = 10, Name = "Canada", Description = "Xuất xứ: Canada", CreateAt = DateTime.Now }
                // Add more origins as needed
            );
            #endregion
            #region Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Samsung Smart TV QLED QA55Q70C",
                    Description = "<h3 style=\"margin-left:0px;\">Tổng quan thiết kế&nbsp;</h3><p style=\"margin-left:0px;\">- <a href=\"https://www.dienmayxanh.com/tivi/smart-khung-tranh-the-frame-qled-samsung-4k-50-inch-qa50ls03b\">Smart Tivi Khung Tranh The Frame QLED Samsung 4K 50 inch QA50LS03B</a>&nbsp;với thiết kế đơn giản nhưng tinh tế, hòa mình một cách hoàn hảo vào bất kỳ không gian nội thất nào. Viền tivi siêu mỏng, chắc chắn hơn và mặt sau được hoàn thiện tỉ mỉ với màu sắc đơn sắc ấn tượng, The Frame làm nổi bật vẻ đẹp tinh tế của TV từ mọi góc nhìn\r\n\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><p style=\"margin-left:0px;\">Khung viền linh hoạt giúp The Frame hòa mình vào mọi không gian. Với sự đa dạng trong lựa chọn khung viền, bạn có thể thỏa sức ngắm nhìn tuyệt tác khung tranh của mình. Các khớp nối nam châm linh hoạt cho phép bạn dễ dàng chuyển đổi màu sắc và phong cách khung viền.\r\n\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><p style=\"margin-left:0px;\">- Bên cạnh giải pháp treo tường, The Frame còn có chân đế bằng nhựa chắc chắn và có thể điều chỉnh độ cao cho phép bạn nâng lên cao và hạ xuống dễ dàng.</p><p style=\"margin-left:0px;\">- Màn hình&nbsp;<a href=\"https://www.dienmayxanh.com/tivi?g=50-inch\">50 inch</a>&nbsp;phù hợp cho những không gian có diện tích vừa.</p><p style=\"margin-left:0px;\">- <strong>Bật lên là </strong><a href=\"https://www.dienmayxanh.com/tivi\"><strong>tivi</strong></a><strong>, tắt đi là khung tranh </strong>mang dáng vóc của một tác phẩm nghệ thuật,&nbsp;thích hợp&nbsp;với không gian phòng khách, phòng ngủ, phòng trưng bày,... đặc biệt phù hợp với những người yêu hội họa, yêu cái đẹp\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Công nghệ hình ảnh</h3><p style=\"margin-left:0px;\">-&nbsp;Độ phân giải<strong>&nbsp;</strong><a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/tivi-uhd-tivi-4k-la-gi-co-khac-gi-so-voi-tivi-full-578555\"><strong>4K</strong></a>&nbsp;cho hình ảnh sắc nét đến từng chi tiết.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/quantum-4k-bo-vi-xu-ly-manh-me-cua-samsung-1175879\"><strong>Bộ xử lý Quantum 4K</strong></a><strong>&nbsp;</strong>sẽ<strong>&nbsp;</strong>tự động nâng cấp chất lượng hình ảnh lên gần chuẩn 4K khi chất lượng nguồn vào chưa đạt chuẩn nhờ công nghệ trí thông minh nhân tạo AI.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cong-nghe-hinh-anh-tren-tivi-samsung-838738#quantum-hdr\"><strong>Quantum HDR</strong></a><strong>&nbsp;</strong>điều chỉnh độ sáng theo từng khung hình hiển thị giúp mọi chi tiết hình ảnh đều trở nên rõ ràng, bắt mắt.</p><p style=\"margin-left:0px;\">-<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#motion-xcelerator-turbo+\">&nbsp;<strong>Motion Xcelerator Turbo+</strong></a>&nbsp;thêm khung hình vào nội dung gốc,&nbsp;độ rõ nét của chuyển động được nâng cao, cho hình ảnh mượt mà, trải nghiệm chơi game đỉnh cao.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/tivi-samsung-2020-co-gi-moi-1240803#adaptive-picture\"><strong>Adaptive Picture</strong></a>&nbsp;tối ưu độ sáng hình ảnh theo điều kiện môi trường xung quanh, đem lại&nbsp;chất lượng hình ảnh tuyệt hảo, đồng nhất mọi nội dung dù xem ở bất kỳ thời điểm nào trong ngày.</p><p style=\"margin-left:0px;\">- Đặc biệt, chiếc tivi Lifestyle được trang bị kho tranh với hơn 1600 tác phẩm (sử dụng miễn phí trong 3 tháng đầu), tải miễn phí 25 tác phẩm trong thời gian trên.</p><p style=\"margin-left:0px;\">Mời bạn xem thêm:<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/diem-mat-nhung-do-phan-giai-pho-bien-hien-nay-tren-577178\">&nbsp;Những độ phân giải màn hình phổ biến hiện nay trên tivi</a>\r\n\r\n <i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Công nghệ âm thanh</h3><p style=\"margin-left:0px;\">- Hệ thống loa <strong>20 W</strong> sống động.</p><p style=\"margin-left:0px;\">- Công nghệ <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cong-nghe-am-thanh-object-tracking-sound-1344551#object-tracking-sound-lite\"><strong>Object Tracking Sound Lite (OTS Lite)</strong></a><strong>&nbsp;</strong>làm cho âm thanh phù hợp với hình ảnh chuyển động trên khung hình, mang đến những làn âm thanh lan tỏa rộng khắp không gian.</p><p style=\"margin-left:0px;\">- <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/tivi-samsung-2020-co-gi-moi-1240803#cong-nghe-am-thanh-ava\"><strong>Active Voice Amplifier (AVA)</strong></a> có thể điều chỉnh âm lượng hội thoại theo điều kiện môi trường bên ngoài, giúp bạn có thể vừa làm việc nhà vừa thưởng thức những bộ phim hấp dẫn.</p><p style=\"margin-left:0px;\">- <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#q-symphony\"><strong>Q-Symphony</strong></a> cầu nối giữa loa tivi và loa thanh giúp tối ưu công suất loa,&nbsp;tạo ra không gian giải trí đỉnh cao.</p><p style=\"margin-left:0px;\">- <strong>Adaptive Sound</strong> âm thanh được tinh chỉnh phù hợp với nội dung giải trí, đem đến cho người dùng trải nghiệm âm thanh hoành tráng, mạnh mẽ.\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Hệ điều hành</h3><p style=\"margin-left:0px;\">- <strong>Hệ điều hành Tizen</strong>&nbsp;trực quan, thân thiện với người dùng.</p><p style=\"margin-left:0px;\">- Hệ điều hành <a href=\"https://www.dienmayxanh.com/tivi-samsung\">tivi Samsung</a> sở hữu kho ứng dụng phong phú như: Clip TV, FPT Play, Galaxy Play (Fim+), MP3 Zing, MyTV, Netflix,&nbsp;<a href=\"https://www.thegioididong.com/game-app/pops-kids-phim-nhac-tv-show-220604\">POPS Kids</a>, Spotify, Trình duyệt web, VieON, YouTube,... đáp ứng cơ bản nhu cầu giải trí cho người dùng.</p><p style=\"margin-left:0px;\">Xem thêm:&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cach-xem-phim-online-tren-tivi-887946#xem-phim-tren-trinh-duyet-web\">Cách xem phim bằng trình duyệt web trên tivi</a>\r\n\r\n <i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Tiện ích</h3><p style=\"margin-left:0px;\">- Công nghệ&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#multi-view\"><strong>Multi View</strong></a>&nbsp;giúp xem cùng lúc nhiều nội dung nhờ khả năng chia hai màn hình tivi.</p><p style=\"margin-left:0px;\">- Ứng dụng&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/huong-dan-su-dung-ung-dung-smartthings-dieu-khien-1126015\"><strong>SmartThings</strong></a>&nbsp;hỗ trợ kết nối và sử dụng tivi cũng như các thiết bị trong nhà dễ dàng và nhanh chóng.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://xn--xem%20thm%20v%20one%20remote-csc9820u/\"><strong>One Remote</strong></a>&nbsp;có thể sạc bằng năng lượng mặt trời tiện lợi, bảo vệ môi trường.</p><p style=\"margin-left:0px;\">- Chiếu màn hình từ điện thoại nhanh chóng nhờ&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/air-play-2-tren-tivi-la-gi-nhung-tivi-nao-ho-tro-a-1172156\">AirPlay 2</a>, <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/huong-dan-ghep-noi-screen-mirroring-giua-dien-thoa-571562\">Screen Mirroring</a>,&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#tap-view\">Tap View.</a></p><p style=\"margin-left:0px;\">Xem thêm:&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cach-su-dung-tinh-nang-chia-doi-man-hinh-tivi-mult-1257139\">Cách sử dụng tính năng Multi View trên tivi Samsung</a>\r\n\r\n <i>Nhìn chung, với thiết kế khung tranh đặc biệt, mỏng, nhẹ, chắc chắn,&nbsp;bộ xử lý Quantum 4K, công nghệ OTS Lite âm thanh chuyển động theo hình ảnh, hệ điều hành Tizen OS 6.0 đa nhiệm, tiện ích phong phú sẽ là lựa chọn không thể tuyệt vời hơn dành cho các tín đồ nghệ thuật và yêu cái đẹp.</i></p>",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 1,
                    BrandId = 2,
                    Status = ProductStatus.Sale,
                    //BrandId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "LG Smart TV NanoCell 55NANO76SQA",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 4,
                    BrandId = 1,
                    Status = ProductStatus.Sale,
                    //BrandId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "LG Smart TV 65UQ8000PSC",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Samsung Smart TV QLED Khung tranh QA50LS03B",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 8,
                    BrandId = 2,
                    Status = ProductStatus.Sale,

                    //BrandId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "AQUA Android TV AQT55D67UG",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 10,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                },
                new Product
                {
                    Id = 6,
                    Name = "LG Smart TV 50UQ7550PSF",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 6,
                    Status = ProductStatus.Sale,
                    //BrandId = 2
                },
                new Product
                {
                    Id = 7,
                    Name = "Bình đun siêu tốc Delites 1.8 lít ST18S05",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                },
                new Product
                {
                    Id = 8,
                    Name = "Bình đun siêu tốc Delites 1.8 lít ST18S05",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                },
                new Product
                {
                    Id = 9,
                    Name = "Bình đun siêu tốc Delites 1.8 lít ST18S05",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                },
                new Product
                {
                    Id = 10,
                    Name = "Bình đun siêu tốc Delites 1.8 lít ST18S05",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                },
                new Product
                {
                    Id = 11,
                    Name = "Bình đun siêu tốc Delites 1.8 lít ST18S05",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                },
                new Product
                {
                    Id = 12,
                    Name = "Bình đun siêu tốc Delites 1.8 lít ST18S05",
                    Description = "Description of Product 2",
                    CreateBy = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    OriginId = 7,
                    BrandId = 1,
                    Status = ProductStatus.Sale
                    //BrandId = 2
                }
            );
            #endregion
            #region ImageType
            modelBuilder.Entity<ImageType>().HasData(
               new ImageType { Id = 1, Name = "Normal" },
               new ImageType { Id = 2, Name = "Banner" }

           );
            #endregion
            #region Image
            modelBuilder.Entity<Image>().HasData(
                new Image() { ImageId = 1, ImagePath = "/Images/Component/ListProduct/product1.webp", IsDefault = true, ProductId = 1, TypeId = 1 },
                new Image() { ImageId = 2, ImagePath = "/Images/Component/ListProduct/product2.webp", IsDefault = true, ProductId = 2, TypeId = 1 },
                new Image() { ImageId = 3, ImagePath = "/Images/Component/ListProduct/product3.webp", IsDefault = true, ProductId = 3, TypeId = 1 },
                new Image() { ImageId = 4, ImagePath = "/Images/Component/ListProduct/product4.webp", IsDefault = true, ProductId = 4, TypeId = 1 },
                new Image() { ImageId = 5, ImagePath = "/Images/Component/ListProduct/product5.webp", IsDefault = true, ProductId = 5, TypeId = 1 },
                new Image() { ImageId = 6, ImagePath = "/Images/Component/ListProduct/product6.webp", IsDefault = true, ProductId = 6, TypeId = 1 },

                new Image() { ImageId = 7, ImagePath = "/Images/Component/ListProduct/product1.webp", IsDefault = true, ProductId = 7, TypeId = 1 },
                new Image() { ImageId = 8, ImagePath = "/Images/Component/ListProduct/product2.webp", IsDefault = true, ProductId = 8, TypeId = 1 },
                new Image() { ImageId = 9, ImagePath = "/Images/Component/ListProduct/product3.webp", IsDefault = true, ProductId = 9, TypeId = 1 },
                new Image() { ImageId = 10, ImagePath = "/Images/Component/ListProduct/product4.webp", IsDefault = true, ProductId = 10, TypeId = 1 },
                new Image() { ImageId = 11, ImagePath = "/Images/Component/ListProduct/product5.webp", IsDefault = true, ProductId = 11, TypeId = 1 },
                new Image() { ImageId = 12, ImagePath = "/Images/Component/ListProduct/product6.webp", IsDefault = true, ProductId = 12, TypeId = 1 },

                new Image() { ImageId = 14, ImagePath = "/Images/Component/ListProduct/product1-1.jpg", IsDefault = false, ProductId = 1, TypeId = 1 },
                new Image() { ImageId = 15, ImagePath = "/Images/Component/ListProduct/product1-2.jpg", IsDefault = false, ProductId = 1, TypeId = 1 },
                new Image() { ImageId = 16, ImagePath = "/Images/Component/ListProduct/product1-3.jpg", IsDefault = false, ProductId = 1, TypeId = 1 },
                new Image() { ImageId = 17, ImagePath = "/Images/Component/ListProduct/product1-4.jpg", IsDefault = false, ProductId = 1, TypeId = 1 },
                new Image() { ImageId = 18, ImagePath = "/Images/Component/ListProduct/product1-5.jpg", IsDefault = false, ProductId = 1, TypeId = 1 },


                 new Image() { ImageId = 13, ImagePath = "https://www.dienmayxanh.com/flashsale", IsDefault = false, TypeId = 2 }
            );
            #endregion
            #region Attribute
            // Seed data for Attribute
            modelBuilder.Entity<Attributes>().HasData(
                new Attributes { Id = 1, Name = "Color", ProductId = 2, IsActive = true, Type = AttributeType.variation, IsShow = true },
                new Attributes { Id = 2, Name = "Size", ProductId = 2, IsActive = true, Type = AttributeType.variation, IsShow = true },
                new Attributes { Id = 3, Name = "Công xuất", ProductId = 1, IsActive = true, Type = AttributeType.variation, IsShow = true },
                new Attributes { Id = 4, Name = "Loại máy", ProductId = 1, IsActive = true, Type = AttributeType.variation, IsShow = true },
                new Attributes { Id = 5, Name = "Kích cỡ màn hình", ProductId = 1, IsActive = true, Type = AttributeType.variation, IsShow = true },
                new Attributes { Id = 6, Name = "Loại tivi", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 7, Name = "Hệ điều hành", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 8, Name = "Ứng dụng phổ biến", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 9, Name = "Công nghệ hình ảnh", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 10, Name = "Điều khiển bằng giọng nói", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 11, Name = "Remote thông minh", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 12, Name = "Chiếu hình từ điện thoại lên TV", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 13, Name = "Kích thước", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 14, Name = "Độ phân giải", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true },
                new Attributes { Id = 15, Name = "Chất liệu viền tivi", ProductId = 1, IsActive = true, Type = AttributeType.dynamic, IsShow = true }
            );
            #endregion
            #region AttributeValue
            // Seed data for AttributeValue
            modelBuilder.Entity<AttributeValue>().HasData(
                new AttributeValue { AtributeValueId = 1, Value = "Red", AttributeId = 1, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 2, Value = "Blue", AttributeId = 1, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 3, Value = "Small", AttributeId = 2, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 4, Value = "Medium", AttributeId = 2, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 5, Value = "Large", AttributeId = 2, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 6, Value = "1 HP", AttributeId = 3, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 7, Value = "1.5 HP", AttributeId = 3, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 8, Value = "2 HP", AttributeId = 3, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 9, Value = "2.5 HP", AttributeId = 3, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 10, Value = "1 chiều", AttributeId = 4, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 11, Value = "2 chiều", AttributeId = 4, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 12, Value = "43 inch", AttributeId = 5, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 13, Value = "50 inch", AttributeId = 5, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 14, Value = "55 inch", AttributeId = 5, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 15, Value = "65 inch", AttributeId = 5, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 16, Value = "75 inch", AttributeId = 5, IsActive = true, IsShow = true, Type = ValuesType.variation },
                new AttributeValue { AtributeValueId = 17, Value = "Smart TV NanoCell", AttributeId = 6, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 18, Value = "WebOS 22", AttributeId = 7, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 19, Value = "YouTube KidsYouTubeVTVcab ONVieONTV 360Trình duyệt webSpotifyPOPS KidsNhaccuatuiNetflixMyTVGalaxy Play (Fim+)FPT PlayClip TV", AttributeId = 8, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 20, Value = "Tương thích HEVCTương thích bộ mã hóa Video decoder (VP9, AV1) Nâng cấp độ phân giải 4K AI UpscalingNâng cấp hình ảnh AI Picture Pro 4KHLGHDR10 ProHDR Dynamic Tone MappingGiảm độ trễ chơi game Auto Low Latency Mode (ALLM)Dải màu rộng Nano ColorChế độ hình ảnh phù hợp nội dungChế độ game HGiGActive HDR", AttributeId = 9, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 21, Value = "Tìm kiếm giọng nói trên YouTube bằng tiếng ViệtNhận diện giọng nói LG Voice RecognitionLG Voice Search - tìm kiếm bằng giọng nói tiếng ViệtGoogle Assistant (Chưa có tiếng Việt)Alexa (Chưa có tiếng Việt)", AttributeId = 10, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 22, Value = "Magic Remote", AttributeId = 11, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 23, Value = "Screen ShareAirPlay 2", AttributeId = 12, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 24, Value = "Ngang 96.7 cm - Cao 62.9 cm - Dày 24.9 cm", AttributeId = 13, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 25, Value = "4K (Ultra HD)", AttributeId = 14, IsActive = true, IsShow = true, Type = ValuesType.dynamic },
                new AttributeValue { AtributeValueId = 26, Value = "Nhựa", AttributeId = 15, IsActive = true, IsShow = true, Type = ValuesType.dynamic }
            );
            #endregion
            #region ProductAttribute
            // Seed data for ProductAttribute
            modelBuilder.Entity<ProductAttribute>().HasData(
               new ProductAttribute { Id = 1, ProductId = 1, AttributeValueId = 12, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2022, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 2, ProductId = 1, AttributeValueId = 13, Quantity = 100, SalePrice = 11990000, PuscharPrice = 10809091, Tax = 10, ReleaseYear = 2022, IsDefault = false, AfterDiscountPrice = 11000000 },
               new ProductAttribute { Id = 3, ProductId = 1, AttributeValueId = 14, Quantity = 100, SalePrice = 11690000, PuscharPrice = 10627273, Tax = 10, ReleaseYear = 2022, IsDefault = false, AfterDiscountPrice = 11500000 },
               new ProductAttribute { Id = 4, ProductId = 1, AttributeValueId = 15, Quantity = 100, SalePrice = 13490000, PuscharPrice = 12263636, Tax = 10, ReleaseYear = 2023, IsDefault = false, AfterDiscountPrice = 13000000 },
               new ProductAttribute { Id = 5, ProductId = 1, AttributeValueId = 16, Quantity = 100, SalePrice = 20990000, PuscharPrice = 18172727, Tax = 10, ReleaseYear = 2023, IsDefault = false, AfterDiscountPrice = 19990000 },

               new ProductAttribute { Id = 6, ProductId = 2, AttributeValueId = 12, Quantity = 50, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2021, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 7, ProductId = 3, AttributeValueId = 12, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2021, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 8, ProductId = 4, AttributeValueId = 12, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2020, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 9, ProductId = 5, AttributeValueId = 12, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 10, ProductId = 6, AttributeValueId = 12, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 11, ProductId = 7, AttributeValueId = 1, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 12, ProductId = 8, AttributeValueId = 1, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 13, ProductId = 9, AttributeValueId = 1, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 14, ProductId = 10, AttributeValueId = 1, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 15, ProductId = 11, AttributeValueId = 1, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 },
               new ProductAttribute { Id = 16, ProductId = 12, AttributeValueId = 1, Quantity = 100, SalePrice = 10290000, PuscharPrice = 9354545, Tax = 10, ReleaseYear = 2023, IsDefault = true, AfterDiscountPrice = 10000000 }
            );
            #endregion
            #region ProductTranslatetion
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation { ProductTranslationId = 1, ProductEAVId = 1, LanguageId = 1, TranslatedName = "Translated Name 1", TranslatedDescription = "Translated Description 1" },
                new ProductTranslation { ProductTranslationId = 2, ProductEAVId = 2, LanguageId = 1, TranslatedName = "Translated Name 2", TranslatedDescription = "Translated Description 2" },
                new ProductTranslation { ProductTranslationId = 3, ProductEAVId = 3, LanguageId = 1, TranslatedName = "Translated Name 3", TranslatedDescription = "Translated Description 3" },
                new ProductTranslation { ProductTranslationId = 4, ProductEAVId = 4, LanguageId = 1, TranslatedName = "Translated Name 4", TranslatedDescription = "Translated Description 4" },
                new ProductTranslation { ProductTranslationId = 5, ProductEAVId = 5, LanguageId = 1, TranslatedName = "Translated Name 5", TranslatedDescription = "Translated Description 5" },
                new ProductTranslation { ProductTranslationId = 6, ProductEAVId = 6, LanguageId = 1, TranslatedName = "Translated Name 6", TranslatedDescription = "Translated Description 6" }
                // Add more seed data as needed
            );
            #endregion
            #region ProductPromotion
            modelBuilder.Entity<ProductPromotion>().HasData(
                new ProductPromotion { ProductPromotionId = 1, PromotionId = 11, ProductId = 1 },
                new ProductPromotion { ProductPromotionId = 2, PromotionId = 11, ProductId = 2 },
                new ProductPromotion { ProductPromotionId = 3, PromotionId = 11, ProductId = 3 },
                new ProductPromotion { ProductPromotionId = 4, PromotionId = 11, ProductId = 4 },
                new ProductPromotion { ProductPromotionId = 5, PromotionId = 11, ProductId = 5 },
                new ProductPromotion { ProductPromotionId = 6, PromotionId = 11, ProductId = 6 },

                new ProductPromotion { ProductPromotionId = 7, PromotionId = 11, ProductId = 7 },
                new ProductPromotion { ProductPromotionId = 8, PromotionId = 10, ProductId = 8 },
                new ProductPromotion { ProductPromotionId = 9, PromotionId = 10, ProductId = 9 },
                new ProductPromotion { ProductPromotionId = 10, PromotionId = 10, ProductId = 10 },
                new ProductPromotion { ProductPromotionId = 11, PromotionId = 10, ProductId = 11 },
                new ProductPromotion { ProductPromotionId = 12, PromotionId = 10, ProductId = 12 }
            // Add more seed data as needed
            );
            #endregion
            #region ProductAddress
            modelBuilder.Entity<ProductAddress>().HasData(
                new ProductAddress { ProductAddressID = 1, AddressID = 1, ProductID = 1, Quantity = 10 },
                new ProductAddress { ProductAddressID = 2, AddressID = 2, ProductID = 2, Quantity = 15 },
                new ProductAddress { ProductAddressID = 3, AddressID = 1, ProductID = 3, Quantity = 20 },
                new ProductAddress { ProductAddressID = 4, AddressID = 1, ProductID = 4, Quantity = 20 },
                new ProductAddress { ProductAddressID = 5, AddressID = 1, ProductID = 5, Quantity = 20 },
                new ProductAddress { ProductAddressID = 6, AddressID = 3, ProductID = 6, Quantity = 20 }
                // Add more seed data as needed
            );
            #endregion
            #region CategoryProduct
            modelBuilder.Entity<CategoryProduct>().HasData(
                new CategoryProduct { CategoryProductId = 1, CategoryId = 33, ProductId = 1 },
                new CategoryProduct { CategoryProductId = 2, CategoryId = 35, ProductId = 1 },
                new CategoryProduct { CategoryProductId = 3, CategoryId = 34, ProductId = 2 },
                new CategoryProduct { CategoryProductId = 4, CategoryId = 36, ProductId = 3 },
                new CategoryProduct { CategoryProductId = 5, CategoryId = 33, ProductId = 4 },
                new CategoryProduct { CategoryProductId = 6, CategoryId = 34, ProductId = 4 },
                new CategoryProduct { CategoryProductId = 7, CategoryId = 35, ProductId = 5 },
                new CategoryProduct { CategoryProductId = 8, CategoryId = 36, ProductId = 5 },
                new CategoryProduct { CategoryProductId = 9, CategoryId = 33, ProductId = 6 },
                new CategoryProduct { CategoryProductId = 10, CategoryId = 34, ProductId = 6 },

                new CategoryProduct { CategoryProductId = 11, CategoryId = 53, ProductId = 7 },
                new CategoryProduct { CategoryProductId = 12, CategoryId = 53, ProductId = 8 },
                new CategoryProduct { CategoryProductId = 13, CategoryId = 53, ProductId = 9 },
                new CategoryProduct { CategoryProductId = 14, CategoryId = 53, ProductId = 10 },
                new CategoryProduct { CategoryProductId = 15, CategoryId = 53, ProductId = 11 },
                new CategoryProduct { CategoryProductId = 16, CategoryId = 53, ProductId = 12 }
                // Thêm dữ liệu mẫu khác nếu cần
            );
            #endregion
            #region CategoryTranslation
            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation { CategoryTranslationId = 1, TranslatedName = "Electronics", CategoryId = 1, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 2, TranslatedName = "Mobile Phones", CategoryId = 2, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 3, TranslatedName = "Laptops", CategoryId = 3, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 4, TranslatedName = "Tablets", CategoryId = 4, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 5, TranslatedName = "Cameras", CategoryId = 5, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 6, TranslatedName = "Audio", CategoryId = 6, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 7, TranslatedName = "Home Appliances", CategoryId = 7, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 8, TranslatedName = "Refrigerators", CategoryId = 8, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 9, TranslatedName = "Washing Machines", CategoryId = 9, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 10, TranslatedName = "Water Purifiers", CategoryId = 10, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 11, TranslatedName = "Microwave Ovens", CategoryId = 11, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 12, TranslatedName = "Electric Stoves", CategoryId = 12, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 13, TranslatedName = "Rice Cookers", CategoryId = 13, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 14, TranslatedName = "Air Purifiers", CategoryId = 14, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 15, TranslatedName = "Ovens", CategoryId = 15, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 16, TranslatedName = "Kettles", CategoryId = 16, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 17, TranslatedName = "Vacuum Cleaners", CategoryId = 17, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 18, TranslatedName = "Air Conditioners", CategoryId = 18, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 19, TranslatedName = "Irons", CategoryId = 19, LanguageId = 1 },
                new CategoryTranslation { CategoryTranslationId = 20, TranslatedName = "Electric Fans", CategoryId = 20, LanguageId = 1 }
            );
            #endregion
            #region InvoiceDetail
            modelBuilder.Entity<InvoiceDetail>().HasData(
                new InvoiceDetail { InvoiceDetailId = 1, InvoiceId = 1, Quantity = 2, ProductAttributeId = 1, NewPrice = 10000000, PuscharPrice = 9354545 , OldPrice = 10290000 },
                new InvoiceDetail { InvoiceDetailId = 2, InvoiceId = 2, Quantity = 1, ProductAttributeId = 2, NewPrice = 11000000, PuscharPrice = 10809091, OldPrice= 11990000 },
                new InvoiceDetail { InvoiceDetailId = 3, InvoiceId = 3, Quantity = 1, ProductAttributeId = 3, NewPrice = 11500000, PuscharPrice = 10627273, OldPrice= 11690000 },
                new InvoiceDetail { InvoiceDetailId = 4, InvoiceId = 3, Quantity = 1, ProductAttributeId = 2, NewPrice = 11000000, PuscharPrice = 10809091, OldPrice = 11990000 }
            );
            #endregion
            #region Comment
            modelBuilder.Entity<Comment>().HasData(
                new Comment { CommentId = 1, Content = "Chất lượng khá tốt so với giá tiền", Date = DateTime.Now, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), Type = CommentType.Default, InvoiceDetailId = 1, Rating = 4, ProductId = 1 },
                new Comment { CommentId = 2, Content = "Sản phẩm tạm ổn", Date = DateTime.Now, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), Type = CommentType.Done, InvoiceDetailId = 2, Rating = 3, ProductId = 1 },
                new Comment { CommentId = 3, Content = "Chất lượng sản phẩm tuyệt vời", Date = DateTime.Now, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), Type = CommentType.Processing, Rating = 5, ProductId = 1 , InvoiceDetailId = 3}
            );
            #endregion
            #region Interested
            modelBuilder.Entity<Interested>().HasData(
                new Interested { InterestedID = 1, ProductID = 1, AppUserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749") },
                new Interested { InterestedID = 2, ProductID = 2, AppUserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749") },
                new Interested { InterestedID = 3, ProductID = 3, AppUserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749") }
            );
            #endregion
            #region Invoice
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice { InvoiceId = 1, CreateDate = DateTime.Now, Status = InvoiceStatus.Success, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), VoucherUserId=1 },
                new Invoice { InvoiceId = 2, CreateDate = DateTime.Now, Status = InvoiceStatus.Success, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749") },
                new Invoice { InvoiceId = 3, CreateDate = DateTime.Now, Status = InvoiceStatus.Success, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749") }
            );
            #endregion
            #region PaymentInfo
            modelBuilder.Entity<PaymentInfo>().HasData(
                new PaymentInfo { PaymentInfoId = 1, InvoiceId = 1, PaymentMethod = PaymentMethod.VNPay, PaymentStatus = PaymentStatus.Success },
                new PaymentInfo { PaymentInfoId = 2, InvoiceId = 2, PaymentMethod = PaymentMethod.MomoQR, PaymentStatus = PaymentStatus.Success },
                new PaymentInfo { PaymentInfoId = 3, InvoiceId = 3, PaymentMethod = PaymentMethod.Cash, PaymentStatus = PaymentStatus.Success }
            );
            #endregion
            #region Role
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                //new IdentityRole<Guid> { Id = Guid.Parse("30A990C6-33C7-4884-9DCB-718CE356EB0D"), Name = "SupperAdmin", NormalizedName = "SupperAdmin" },
                new IdentityRole<Guid> { Id = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA"), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<Guid> { Id = Guid.Parse("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E"), Name = "User", NormalizedName = "USER" }
            );
            #endregion
            #region UserRole
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                //Seed admin
                new IdentityUserRole<Guid> { UserId = Guid.Parse("2753c921-2304-4f8d-b8d5-75229d3b20d6"), RoleId = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA") },
                //Seend user
                new IdentityUserRole<Guid> { UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), RoleId = Guid.Parse("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E") }
            );
            #endregion
            #region TimeRange
            modelBuilder.Entity<TimeRange>().HasData(
                new TimeRange { Id = 1, Name = "0 - 1 triệu", MinPrice = 0, MaxPrice = 1000000 },
                new TimeRange { Id = 2, Name = "1 triệu - 2 triệu", MinPrice = 1000000, MaxPrice = 2000000 },
                new TimeRange { Id = 3, Name = "2 triệu - 3 triệu", MinPrice = 2000000, MaxPrice = 3000000 },
                new TimeRange { Id = 4, Name = "3 triệu - 4 triệu", MinPrice = 3000000, MaxPrice = 4000000 },
                new TimeRange { Id = 5, Name = "4 triệu - 5 triệu", MinPrice = 4000000, MaxPrice = 5000000 },
                new TimeRange { Id = 6, Name = "5 triệu - 6 triệu", MinPrice = 5000000, MaxPrice = 6000000 },
                new TimeRange { Id = 7, Name = "6 triệu - 7 triệu", MinPrice = 6000000, MaxPrice = 7000000 },
                new TimeRange { Id = 8, Name = "7 triệu - 8 triệu", MinPrice = 7000000, MaxPrice = 8000000 },
                new TimeRange { Id = 9, Name = "8 triệu - 9 triệu", MinPrice = 8000000, MaxPrice = 9000000 },
                new TimeRange { Id = 10, Name = "9 triệu - 10 triệu", MinPrice = 9000000, MaxPrice = 10000000 },
                new TimeRange { Id = 11, Name = "Hơn 10 triệu", MinPrice = 10000000, MaxPrice = 999999999 }
            );
            #endregion
            #region CategoryTimeRange
            modelBuilder.Entity<CategoryTimeRange>().HasData(
                new CategoryTimeRange { Id = 1, CategoryId = 15, TimeRangeId = 1 },
                new CategoryTimeRange { Id = 2, CategoryId = 15, TimeRangeId = 2 },
                new CategoryTimeRange { Id = 4, CategoryId = 15, TimeRangeId = 4 },
                new CategoryTimeRange { Id = 5, CategoryId = 15, TimeRangeId = 5 },
                new CategoryTimeRange { Id = 6, CategoryId = 15, TimeRangeId = 6 },
                new CategoryTimeRange { Id = 7, CategoryId = 15, TimeRangeId = 7 },
                new CategoryTimeRange { Id = 8, CategoryId = 15, TimeRangeId = 8 },
                new CategoryTimeRange { Id = 9, CategoryId = 15, TimeRangeId = 9 },
                new CategoryTimeRange { Id = 11, CategoryId = 15, TimeRangeId = 11 }
            );
            #endregion
            #region Voucher
            modelBuilder.Entity<Voucher>().HasData(
                new Voucher { Id = 1, Name = "VOUCHER10", DiscountByPercent = 10, DiscountByPrice = 0, CreateAt = DateTime.Now },
                new Voucher { Id = 2, Name = "VOUCHER20", DiscountByPercent = 20, DiscountByPrice = 0, CreateAt = DateTime.Now },
                new Voucher { Id = 3, Name = "VOUCHER300000", DiscountByPercent = 0, DiscountByPrice = 300000, CreateAt = DateTime.Now }
            );
            #endregion
            #region VoucherUser
            modelBuilder.Entity<VoucherUser>().HasData(
                new VoucherUser { Id = 1, VoucherId = 1, AppUserId = Guid.Parse("00BB44D1-F674-49F6-BDAE-AFB143AB9749"), From = DateTime.Now, To = DateTime.MaxValue, IsDeleted=true },
                new VoucherUser { Id = 2, VoucherId = 2, AppUserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), From = DateTime.Now, To = DateTime.MaxValue },
                new VoucherUser { Id = 3, VoucherId = 3, AppUserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), From = DateTime.Now, To = DateTime.MaxValue }
                );
            #endregion ShppingOrder
            #region ShippingOrder
            modelBuilder.Entity<ShippingOrder>().HasData(
                new ShippingOrder { Id = 1, OrderCode = "L6AHHM", UserId = Guid.Parse("AA7C5218-4F1E-4AC6-A3B4-08DCB162E29E"), Price = 200000, CreateAt = DateTime.Now, InvoiceId = 3 }
            );
            #endregion

        }
        public static bool IsSeeded(DATNDbContext context)
        {
            return context.Promotions.Any() || context.Notifications.Any();
        }

    }


}

