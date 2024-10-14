using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class SeedDât : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "IdCategory", "CreatedBy", "CreatedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "HẢI SẢN", null, null },
                    { 2, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ĐỒ NƯỚNG", null, null },
                    { 3, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "CÁ CÁC MÓN", null, null },
                    { 4, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "MÓN ĂN CHƠI", null, null },
                    { 5, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "MÓN NHẬU", null, null }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "CategoryID", "CreatedBy", "CreatedDate", "Description", "Image", "Name", "Price", "Type", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tên món: Tôm chiên hoàng kim\r\nĐịnh lượng: 1 suất 2-4 người ăn\r\nMô tả: Tôm chiên hoàng kim là một trong những món nhậu cực kỳ tốn bia tại Quán Nhậu Tự Do.\r\nTôm tươi, căng mọng đem chiên vàng giòn rồi lại đảo qua gia vị đậm đà tạo nên món ăn hấp dẫn, ăn một miếng là không ngừng lại được.", "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/20240424094950598.webp", "Tôm chiên hoàng kim", 225000.0, "Suất", null, null },
                    { 2, 1, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tên món: Tôm sú sốt ớt pattaya\r\nĐịnh lượng: 1 suất cho 3-4 người ăn\r\nMô tả: Tôm sú sốt ớt pattaya là một trong những món ăn mới lạ không thể bỏ qua khi ghé Quán Nhậu Tự Do.\r\nThịt tôm sú dai giòn ăn cùng rau củ, thấm đẫm nước sốt ớt pattaya “thần thánh\" xen lẫn vị chua chua, ngọt ngọt, cay nồng chắc chắn sẽ làm hài lòng mọi thực khách.", "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/06/202406051717238266.webp", "Tôm sú sốt ớt pattaya", 225000.0, "Suất", null, null },
                    { 3, 2, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tên món: Lợn mán nướng mắc khén\r\nĐịnh lượng: 1 suất cho 3-4 người ăn\r\nMô tả: Lợn mán nướng mắc khén là một trong những món nhậu kích thích vị giác của thực khách.\r\nThịt lợn mềm ngọt, có chút dai dai, không quá mỡ được nướng cùng hạt mắc khén thơm lừng. Sự hòa trộn giữa vị đậm đà của thịt lợn mán cùng các loại rau củ ăn kèm mang đến cho món ăn hương vị tuyệt hảo không thể nào quên.\r\n", "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404231915283755.webp", "Lợn mán nướng mắc khén", 185000.0, "Suất", null, null },
                    { 4, 2, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tên món: Gà đen nướng mắc khén\r\nĐịnh lượng: Gà nửa con, hạt mắc khén rang thơm, quả ớt cay, lá chanh, muối,củ sả.\r\nMô tả: Chuẩn gia vị chẩm chéo để chấm gà, thơm phức mùi mắc kén và tê tê đầu lưỡi.", "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/05/20240530101506515.webp", "Gà đen nướng mắc khén", 300000.0, "Suất", null, null },
                    { 5, 5, "Admin", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tên món: Gà H’Mong rang muối\r\nĐịnh lượng: 800g\r\nMô tả: Gà H’Mong rang muối là món ăn được nhiều anh em sành ăn yêu thích tại Quán Nhậu Tự Do.\r\nGà H’Mong được tuyển chọn từ giống gà đen nuôi tự nhiên nên thịt rất săn chắc và thơm ngon. Đem gà chiên giòn và xóc đều với bột muối, sả, hành và lá chanh là đủ để có một món nhậu hấp dẫn khó quên.", "https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404240940237304.webp", "Gà H’Mong rang muối", 300000.0, "Suất", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "DishId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "DishId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "DishId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "DishId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "DishId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 5);
        }
    }
}
