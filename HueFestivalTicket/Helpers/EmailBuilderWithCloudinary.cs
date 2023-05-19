using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HueFestivalTicket.Models;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace HueFestivalTicket.Helpers
{
    public class EmailBuilderWithCloudinary
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public EmailBuilderWithCloudinary(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string BuildEmailContent(List<Ticket> tickets)
        {
            StringBuilder sb = new StringBuilder();

            // Begin HTML
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>Tickets</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            // Insert Title
            sb.AppendLine("<h1>Hue Festival Ticket Online</h1>");
            sb.AppendLine($"<p>Xin chào {tickets[0].Invoice!.Customer!.FirstName}</p>");
            sb.AppendLine("<p>Cảm ơn bạn đã đặt vé từ hệ thống của chúng tôi</p>");
            sb.AppendLine("<p>Đây là các thông tin về vé bạn đã đặt</p>");
            sb.AppendLine($"<p>Mã hoá đơn: {tickets[0].IdInvoice} </p>");
            sb.AppendLine($"<p>Địa điểm: {tickets[0].EventLocation!.Location!.Title} </p>");
            sb.AppendLine($"<p>Địa chỉ: {tickets[0].EventLocation!.Location!.Address} </p>");
            sb.AppendLine($"<p>Thời gian tổ chức: {tickets[0].EventLocation!.DateStart.ToString("dd/MM/yyyy")} vào lúc {tickets[0].EventLocation!.Time.ToString("HH:mm")} </p>");
            sb.AppendLine($"<p>Số lượng vé: {tickets.Count} </p>");
            sb.AppendLine($"<p>Giá vé: {tickets[0].Price} </p>");
            sb.AppendLine($"<p>Loại vé: {tickets[0].TypeTicket!.Name} </p>");
            sb.AppendLine($"<p>Tổng hoá đơn: {tickets[0].Invoice!.Total} </p>");

            sb.AppendLine("<h1>--- Mã Vé ---</h1>");
            sb.AppendLine("<p>Dưới đây là mã vé của bạn, vui lòng đưa trước nhân viên soát vé để được kiểm tra</p>");

            /*
            // Insert Ticket List
            sb.AppendLine("<ul>");
            foreach (var ticket in tickets)
            {
                sb.AppendLine("<li>");
                sb.AppendLine($"<h2>{ticket.TicketNumber}</h2>");
                sb.AppendLine("<div>");

                string qrCodeUrl = GenerateAndUploadQRCode(ticket.QRCode!);

                // Insert QRCode Image
                sb.AppendLine($"<img src='{qrCodeUrl}' alt='{ticket.TicketNumber}' />");

                sb.AppendLine("</div>");
                sb.AppendLine("</li>");
            }
            sb.AppendLine("</ul>");*/

            // End HTML
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        public string GenerateAndUploadQRCode(string text)
        {
            string? cloudName = _configuration["Cloudinary:CloudName"];
            string? apiKey = _configuration["Cloudinary:APIKey"];
            string? apiSecret = _configuration["Cloudinary:APISecret"];

            // Tạo QR code từ chuỗi text
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);

            // Khởi tạo Cloudinary
            CloudinaryDotNet.Account cloudinaryAccount = new CloudinaryDotNet.Account(cloudName, apiKey, apiSecret);
            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);

            // Tạo một tên duy nhất cho ảnh
            string uniqueFilename = Guid.NewGuid().ToString();

            // Chuyển đổi Bitmap thành Stream
            MemoryStream stream = new MemoryStream();
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;

            // Tạo các thông tin để tải ảnh lên Cloudinary
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(uniqueFilename, stream),
                PublicId = uniqueFilename,
                Folder = "qr_codes" // Tên thư mục trên Cloudinary
            };

            // Tải ảnh lên Cloudinary
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            // Trả về đường dẫn ảnh từ Cloudinary
            return uploadResult.SecureUrl.ToString();
        }

        public string GenerateQRCodeImage(string content, string name)
        {
            var newImagePath = _environment.WebRootPath + "\\images\\";
            // Tạo đường dẫn và tên file mới
            //string fileName = Guid.NewGuid().ToString("N") + ".png";
            string fileName = name + ".png";
            string filePath = System.IO.Path.Combine(newImagePath, fileName);

            // Tạo mã QR từ chuỗi văn bản
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                // Chuyển đổi mã QR thành hình ảnh
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                // Lưu hình ảnh mã QR vào file
                qrCodeImage.Save(filePath, ImageFormat.Png);
            }

            // Trả về đường dẫn của file mã QR
            return filePath;
        }

        public void DeleteFile(string? imagePath)
        {
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
