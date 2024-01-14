using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LaBenVi.Products.Migrations
{
    /// <inheritdoc />
    public partial class addProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Women Fashion", "Beautiful golden ladies heels for grand occassions.", "https://www.brasslook.com/wp-content/uploads/2018/06/Gold-High-Heels-Shoes-Ideas-18.jpg", "High heels", 15.0 },
                    { 2, "Cars", "The 2023 Bentley Mulliner Batur / Bacalar is a super-powered two-seater grand tourer that only Mulliner can create. The 2023 Bentley Mulliner batur is powered by a 6.0 liter, twin-turbocharged W12 engine that spins the rear wheels via an eight-speed automatic transmission.", "https://cdn.carbuzz.com/gallery-images/2023-bentley-batur-frontal-aspect-carbuzz-1030017.jpg", "2023 Bentley Continental GT Mulliner", 13.99 },
                    { 3, "Women Fashion", "Classy designers white hand bag.", "https://ae01.alicdn.com/kf/HTB1nvnVnOCYBuNkHFCcq6AHtVXaV/white-handbag-2019-Elegant-Shoulder-Bag-Women-designer-handbags-high-quality-pu-leather-ladies-hand-bags.jpg", "Hand bag", 10.99 },
                    { 4, "Men Fashion", "Designers suit for men.", "https://www.bing.com/images/search?view=detailV2&ccid=ITKK39of&id=4C3CA5085BD9EBDE7828194F715EA487657B0168&thid=OIP.ITKK39ofynUuKucW5DBIlgAAAA&mediaurl=https%3A%2F%2Fi0.wp.com%2Fwww.theunstitchd.com%2Fwp-content%2Fuploads%2F2016%2F05%2Fmen-black-suit.jpg%3Fw%3D1000&exph=713&expw=333&q=classy+male+suit&simid=607999857431368482&form=IRPRST&ck=61CABCB39CDED823D2BF81B9AE1F7829&selectedindex=0&itb=0&ajaxhist=0&ajaxserp=0&vt=0&sim=11&cdnurl=https%3A%2F%2Fth.bing.com%2Fth%2Fid%2FR.21328adfda1fca752e2ae716e4304896%3Frik%3DaAF7ZYekXnFPGQ%26pid%3DImgRaw%26r%3D0", "Pav Bhaji", 15.0 },
                    { 5, "Dessert", "CHicken and vegie pizza just for your taste buds.", "https://picfiles.alphacoders.com/303/thumb-1920-303287.jpg", "Pizza", 15.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
