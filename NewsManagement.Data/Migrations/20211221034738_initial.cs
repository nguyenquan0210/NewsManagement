using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsManagement.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Leader = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    License = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Hotline = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Contact_Advertise = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Click = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Hot = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Hot = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventss_CategoryNews_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Account_Id",
                        column: x => x.Id,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Account_Id",
                        column: x => x.Id,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Img = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Account_Id",
                        column: x => x.Id,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "varchar(max)", unicode: false, maxLength: 2147483647, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Viewss = table.Column<int>(type: "int", nullable: false),
                    News_Hot = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Eventss_EventId",
                        column: x => x.EventId,
                        principalTable: "Eventss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Servicess_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Servicess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeActive = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    Checkrating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Advertise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Url = table.Column<string>(type: "varchar(max)", unicode: false, maxLength: 2147483647, nullable: false),
                    UrlImg = table.Column<string>(type: "varchar(max)", unicode: false, maxLength: 2147483647, nullable: false),
                    Published_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expire_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertise_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Nhân viên" },
                    { 3, "Khách hàng" },
                    { 4, "Người dùng" }
                });

            migrationBuilder.InsertData(
                table: "CategoryNews",
                columns: new[] { "Id", "Name", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 1, "Xã Hội", 1, 1 },
                    { 2, "Xã Hội", 2, 1 },
                    { 3, "Xã Hội", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Name", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 1, "Đà nẵng", 1, 1 },
                    { 2, "Hà Nội", 1, 1 },
                    { 3, "TP HCM", 1, 1 },
                    { 4, "Hải Phòng", 1, 1 },
                    { 5, "Cần Thơ", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Servicess",
                columns: new[] { "Id", "Date", "Description", "Period", "Price", "Status", "Title" },
                values: new object[] { 1, new DateTime(2021, 12, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(3609), "Quảng cáo tất cả các loại sản phẩm, đảm bảo uy tín chất lượng liên tục, dễ dàng nâp cấp lên gói khác,...", 1, 200000m, 1, "Dịch vụ quảng cáo 1 tháng" });

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "Id", "Name", "SortOrder", "Status" },
                values: new object[] { 1, "Món ngon mỗi ngày", 1, 1 });

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "Id", "Hot", "Name", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 2, true, " Thời tiết hôm nay", 1, 1 },
                    { 3, true, "Tai nạn giao thông", 1, 1 },
                    { 4, true, " Tin tức thời sự", 1, 1 },
                    { 5, true, "Quân sự nước ngoài", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "AccountTypeId", "Date", "Password", "Status", "UserName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 21, 10, 47, 37, 723, DateTimeKind.Local).AddTicks(3653), "AQAAAAEAACcQAAAAEDZeMOKRMEkW5DHBu4vOq6Jz1ospU+UCA90UTv3/LQKZMdVLNBBY3M1jEIo5n9zu9g==", 1, "Admin" },
                    { 2, 2, new DateTime(2021, 12, 21, 10, 47, 37, 730, DateTimeKind.Local).AddTicks(8477), "AQAAAAEAACcQAAAAEDAtrsJP5LtrYGDfaMMm2Ui1i+x47z7wZMsS+Nnqv7yJ43YO7UTdLQMPEdy8//mfdg==", 1, "nhanvien1" },
                    { 3, 4, new DateTime(2021, 12, 21, 10, 47, 37, 738, DateTimeKind.Local).AddTicks(2530), "AQAAAAEAACcQAAAAEOaRc4KkwtOdlzvbBShZUKHxculqMT1h2EfaBDEvKRiQlkoYOSzmBOOls53eJeWZeQ==", 1, "quan" },
                    { 4, 3, new DateTime(2021, 12, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(2799), "AQAAAAEAACcQAAAAEP5S2NSnjcjCu8hwXLY0jcyLGGFd3ilhkVQP2xrmB3iy/djfw/KSgZsCx4RxI8uxjw==", 1, "Client1" }
                });

            migrationBuilder.InsertData(
                table: "Eventss",
                columns: new[] { "Id", "CategoryId", "Hot", "Name", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 1, 1, true, "Vòng loại World Cup 2022", 1, 1 },
                    { 2, 1, true, "Tiêm vaccine Covid-19 cho trẻ em", 1, 1 },
                    { 3, 1, true, "Loạt ca nhiễm nCoV mới ở Việt Nam", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Address", "Company", "Email", "Img", "PhoneNumber" },
                values: new object[] { 4, "Quảng Nam", "Trung tâm phát triển phần mềm - ĐH Đà Nẵng", "nguyenquan52000@gmail.com", "user1.jpg", "0373951042" });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AccountId", "CityId", "Content", "Date", "Description", "EventId", "Img", "Keyword", "News_Hot", "Status", "Title", "TopicId", "Url", "Video", "Viewss" },
                values: new object[] { 1, 1, 2, "<p>Sở Y tế H&agrave; Nội tối 29/11 cho biết tr&ecirc;n địa b&agrave;n th&agrave;nh phố ghi nhận 390 ca dương t&iacute;nh, trong đ&oacute; c&oacute; 220 ca cộng đồng, 109 ca tại khu c&aacute;ch ly v&agrave; 61 ca tại khu phong toả. Đ&acirc;y l&agrave; ng&agrave;y ghi nhận số ca mắc trong 24 giờ v&agrave; ca cộng đồng cao nhất từ trước tới nay.</p> ", new DateTime(2021, 12, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(3555), "Với 220 ca cộng đồng trong tổng 390 ca nhiễm ghi nhận, ngày 29/11 đánh dấu mốc kỷ lục về dịch COVID-19 ở Hà Nội.", 3, "29112021_081723_PM_news1.jpg", "covid-19,Hà nội", 1, 1, "Hà Nội lập kỷ lục 'kép': 390 ca nhiễm mới, 220 ca cộng đồng", 4, null, null, 0 });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "Birthday", "Email", "Img", "Name", "PhoneNumber" },
                values: new object[] { 2, "Quảng Nam", new DateTime(2000, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "nguyenquan52000@gmail.com", "user1.jpg", "Nguyễn Đình Quân", "0373951042" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "Birthday", "Img", "Name", "PhoneNumber" },
                values: new object[] { 3, "Quảng Nam", new DateTime(2000, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1.jpg", "Nguyễn Đình Quân", "0373951042" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Answer", "Date", "NewsId", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2021, 12, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(3575), 1, "Covid-19", true, 3 },
                    { 2, 0, new DateTime(2021, 12, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(3577), 1, "13", false, 3 }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "ClientId", "Date", "ServiceId", "Status", "Title" },
                values: new object[] { 1, 4, new DateTime(2021, 12, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(3625), 1, 0, "đơn hàng demo" });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "Id", "Checkrating", "NewsId", "UserId", "Value" },
                values: new object[] { 1, "13", 1, 3, 5 });

            migrationBuilder.InsertData(
                table: "Advertise",
                columns: new[] { "Id", "Description", "Expire_Date", "OrderId", "Published_Date", "Status", "Title", "Url", "UrlImg" },
                values: new object[] { 1, "<H3>Mã sản phẩm 212364001</H3> <br> <p>Lòng nồi làm từ chất liệu hợp kim nhôm dạng niêu bền bỉ, nấu ngon</p> <br> <p>Dung tích 1.8 lít dùng phù hợp cho gia đình 4 - 6 người</p> <br> <p>Nồi dạng cơ sử dụng đơn giản Công suất 700W nấu cơm nhanh và ngon</p>", new DateTime(2022, 1, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(3642), 1, new DateTime(2021, 12, 21, 10, 47, 37, 745, DateTimeKind.Local).AddTicks(3641), 1, "Nồi Cơm Niêu Điện 1.8L Mishio MK248 700W", "https://gsshop.vn/noi-com-nieu-dien-1-8l-mishio-mk248-700w-212364001.html?utm_source=google-gdn&device=c&agid=125553696715&cid=13888844554&creative=533462530738&keyword==&gclid=CjwKCAiA7dKMBhBCEiwAO_crFKJ6eS3GoAuXugINVAKwsZ9MCnVk00vTjvdm_twOSriDHsQj6vCEFhoCxSgQAvD_BwE", "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcSpq3Kunb8u-YtUU15Sf-V6IfvRAez268ugpA7VT2JKiQWHrvnxSxGAS5Ycvg&usqp=CAI" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountTypeId",
                table: "Account",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveUser_UserId",
                table: "ActiveUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertise_OrderId",
                table: "Advertise",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                table: "Comment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventss_CategoryId",
                table: "Eventss",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_News_AccountId",
                table: "News",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CityId",
                table: "News",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_News_EventId",
                table: "News",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_News_TopicId",
                table: "News",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ServiceId",
                table: "Order",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_NewsId",
                table: "Rating",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId",
                table: "Rating",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveUser");

            migrationBuilder.DropTable(
                name: "Advertise");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Servicess");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Eventss");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "CategoryNews");

            migrationBuilder.DropTable(
                name: "AccountType");
        }
    }
}
