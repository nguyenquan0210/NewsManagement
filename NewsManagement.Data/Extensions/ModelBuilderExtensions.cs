using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Data.Entities;
using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var roleId1 = new Guid("2DD4EC71-5669-42D7-9CF9-BB17220C64C7");
            var roleId2 = new Guid("50FE257E-6475-41F0-93F7-F530D622362B");
            var roleId3 = new Guid("BD6B262E-D24B-477B-9094-B67F447CDC42");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            }, new AppRole
            {
                Id = roleId1,
                Name = "staff",
                NormalizedName = "staff",
                Description = "staff role"
            }, new AppRole
            {
                Id = roleId2,
                Name = "client",
                NormalizedName = "client",
                Description = "client role"
            }, new AppRole
            {
                Id = roleId3,
                Name = "user",
                NormalizedName = "user",
                Description = "user role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "nguyenquan52000@gmail.com",
                NormalizedEmail = "nguyenquan52000@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "quax2h1408$"),
                SecurityStamp = string.Empty,
                FirstName = "Quan",
                LastName = "Nguyen",
                Dob = new DateTime(2000, 10, 02),
                Date = new DateTime(2021, 12, 17),
                Address = "Quảng Nam City",
                Img = "userAdmin.png"
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
            modelBuilder.Entity<Category>().HasData(
               new Category() { Id = 1, Name = "Xã Hội" , Status = Status.Active, SortOrder = 1},
               new Category() { Id = 2, Name = "Pháp Luật" , Status = Status.Active, SortOrder = 2},
               new Category() { Id = 3, Name = "Giao thông", Status = Status.Active, SortOrder = 3}
               );
            modelBuilder.Entity<Eventss>().HasData(
               new Eventss() { Id = 1, Name = "Vòng loại World Cup 2022", Status = Status.Active, SortOrder = 1 , CategoryId = 1, Hot = true},
               new Eventss() { Id = 2, Name = "Tiêm vaccine Covid-19 cho trẻ em", Status = Status.Active, SortOrder = 1 , CategoryId = 1, Hot = true},
               new Eventss() { Id = 3, Name = "Loạt ca nhiễm nCoV mới ở Việt Nam", Status = Status.Active, SortOrder = 1 , CategoryId = 1, Hot = true}
               );
            modelBuilder.Entity<City>().HasData(
               new City() { Id = 1, Name = "Đà nẵng", Status = Status.Active, SortOrder = 1 },
               new City() { Id = 2, Name = "Hà Nội", Status = Status.Active, SortOrder = 1 },
               new City() { Id = 3, Name = "TP HCM", Status = Status.Active, SortOrder = 1 },
               new City() { Id = 4, Name = "Hải Phòng", Status = Status.Active, SortOrder = 1 },
               new City() { Id = 5, Name = "Cần Thơ", Status = Status.Active, SortOrder = 1 }
               );
            modelBuilder.Entity<Topic>().HasData(
               new Topic() { Id = 1, Name = "Món ngon mỗi ngày", Status = Status.Active, SortOrder = 1, Hot = false },
               new Topic() { Id = 2, Name = " Thời tiết hôm nay", Status = Status.Active, SortOrder = 1, Hot = true },
               new Topic() { Id = 3, Name = "Tai nạn giao thông", Status = Status.Active, SortOrder = 1, Hot = true },
               new Topic() { Id = 4, Name = " Tin tức thời sự", Status = Status.Active, SortOrder = 1, Hot = true },
               new Topic() { Id = 5, Name = "Quân sự nước ngoài", Status = Status.Active, SortOrder = 1, Hot = true }
               );
            modelBuilder.Entity<News>().HasData(
               new News() { Id = 1,
                   Title = "Hà Nội lập kỷ lục 'kép': 390 ca nhiễm mới, 220 ca cộng đồng", 
                   Description = "Với 220 ca cộng đồng trong tổng 390 ca nhiễm ghi nhận, ngày 29/11 đánh dấu mốc kỷ lục về dịch COVID-19 ở Hà Nội.",
                   Content = "<p>Sở Y tế H&agrave; Nội tối 29/11 cho biết tr&ecirc;n địa b&agrave;n th&agrave;nh phố ghi nhận 390 ca dương t&iacute;nh, trong đ&oacute; c&oacute; 220 ca cộng đồng, 109 ca tại khu c&aacute;ch ly v&agrave; 61 ca tại khu phong toả. Đ&acirc;y l&agrave; ng&agrave;y ghi nhận số ca mắc trong 24 giờ v&agrave; ca cộng đồng cao nhất từ trước tới nay.</p> ",
                   Img = "29112021_081723_PM_news1.jpg",
                   Viewss = 0,
                   Status = Status.Active, 
                   Keyword = "covid-19,Hà nội",
                   Date = DateTime.Now,
                   News_Hot = Status.Active,
                   UserId = adminId,
                   CityId = 2,
                   EventId = 3,
                   TopicId = 4
               }
               
               );
            modelBuilder.Entity<Comment>().HasData(
              new Comment() { Id = 1, Title = "Covid-19", Date = DateTime.Now, NewsId = 1,UserId = adminId, Type = true },
              new Comment() { Id = 2, Title = "13", Date = DateTime.Now, NewsId = 1,UserId = adminId, Type = false }
              );
            modelBuilder.Entity<Rating>().HasData(
              new Rating() { Id = 1, Checkrating = "13", Value = 5, NewsId = 1, UserId = adminId }
              );
            modelBuilder.Entity<Servicess>().HasData(
              new Servicess() { Id = 1, Title = "Dịch vụ quảng cáo 1 tháng", Description = "Quảng cáo tất cả các loại sản phẩm, đảm bảo uy tín chất lượng liên tục, dễ dàng nâp cấp lên gói khác,...", Period = 1, Price = 200000, Date = DateTime.Now, Status = Status.Active }
              );
            modelBuilder.Entity<Order>().HasData(
             new Order() { Id = 1, Title = "đơn hàng demo",  UserId = adminId, ServiceId = 1, Date = DateTime.Now, Status = OrderStatus.InProgress }
             );
            modelBuilder.Entity<Advertise>().HasData(
            new Advertise() { Id = 1, 
                Title = "Nồi Cơm Niêu Điện 1.8L Mishio MK248 700W", 
                Description = "<H3>Mã sản phẩm 212364001</H3> <br> <p>Lòng nồi làm từ chất liệu hợp kim nhôm dạng niêu bền bỉ, nấu ngon</p> <br> <p>Dung tích 1.8 lít dùng phù hợp cho gia đình 4 - 6 người</p> <br> <p>Nồi dạng cơ sử dụng đơn giản Công suất 700W nấu cơm nhanh và ngon</p>", 
                OrderId = 1, 
                Url = "https://gsshop.vn/noi-com-nieu-dien-1-8l-mishio-mk248-700w-212364001.html?utm_source=google-gdn&device=c&agid=125553696715&cid=13888844554&creative=533462530738&keyword==&gclid=CjwKCAiA7dKMBhBCEiwAO_crFKJ6eS3GoAuXugINVAKwsZ9MCnVk00vTjvdm_twOSriDHsQj6vCEFhoCxSgQAvD_BwE", 
                UrlImg = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcSpq3Kunb8u-YtUU15Sf-V6IfvRAez268ugpA7VT2JKiQWHrvnxSxGAS5Ycvg&usqp=CAI",
                Published_Date = DateTime.Now, 
                Expire_Date = DateTime.Now.AddMonths(1),
                Status = Status.Active }
            );
            modelBuilder.Entity<Contact>().HasData(
                new Contact()
                {
                    Id = 1,
                    Email = "nguyenquan52000@gmail.com",
                    Address = "Thôn An Lương Xã Tam Anh Bắc Núi Thành Quảng Nam",
                    Company = "Trung tâm phát triển phần mềm - ĐH Đà Nẵng",
                    Click = 0 ,
                    Contact_Advertise = "ads@tintuc.vn - Điện thoại: 0964.705.888",
                    Hotline = "(+84) 236.2240.741",
                    Leader = "Nguyễn Đình Quân",
                    License = "<p>Giấy ph&eacute;p hoạt động số 3049/GP-TTĐT do Sở TTTT H&agrave; Nội cấp ng&agrave;y 07/11/2014.</p>< p > Giấy x & aacute; c nhận số: 4164 / GXN - TTĐT, do sở TTTT H & agrave; Nội cấp ng & agrave; y 19 / 10 / 2018.</ p >< p > Giấy ph & eacute; p sửa đổi bổ sung số: 4294 / GP - TTĐT, do sở TTTT H & agrave; Nội cấp ng & agrave; y 30 / 10 / 2018.</ p >",
                    Position = "Phó Giám Đốc"
                });
        }
    }
}
