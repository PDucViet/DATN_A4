using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModels.SendMailVM;
using System.Globalization;

namespace DATN.Client.Helper
{
    public class CancelnvoiceContent
    {
        public static SendMailVM GenerateContentMail(AppUser user, Invoice invoice)
        {
            SendMailVM sendMail = new SendMailVM
            {
                Email = user.Email,
                Subject = "DATN - Đơn Hàng Đã Bị Hủy",
                Content = $@"
        <!DOCTYPE html>
        <html lang='vi'>
          <head>
            <meta charset='UTF-8' />
            <meta name='viewport' content='width=device-width, initial-scale=1.0' />
            <title>Thông Báo Đơn Hàng Hủy</title>
            <link
              href='https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css'
              rel='stylesheet'
              integrity='sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC'
              crossorigin='anonymous'
            />
            <style>
              body {{
                font-family: Arial, sans-serif;
                background-color: #f8f9fa;
                color: #343a40;
              }}
              .container {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
              }}
              h3 {{
                color: #D82D8B;
              }}
              .footer {{
                margin-top: 20px;
                padding-top: 10px;
                border-top: 1px solid #e9ecef;
              }}
              .alert {{
                margin-bottom: 20px;
                padding: 10px;
                background-color: #f8d7da;
                border-color: #f5c6cb;
                color: #721c24;
                border-radius: 4px;
              }}
              table {{
                width: 100%;
                border-collapse: collapse;
                margin-bottom: 20px;
              }}
              table, th, td {{
                border: 1px solid #dee2e6;
              }}
              th, td {{
                padding: 8px;
                text-align: left;
              }}
              th {{
                background-color: #f1f1f1;
              }}
            </style>
          </head>
          <body>
            <div class='container'>
              <h3 class='text-center'>DATN - Đơn Hàng Đã Bị Hủy</h3>
              <p>Xin chào {user.FullName},</p>
              <p>Chúng tôi rất tiếc phải thông báo rằng đơn hàng của bạn đã bị hủy. Dưới đây là thông tin chi tiết về đơn hàng:</p>
              <p><b>Mã hóa đơn:</b> {invoice.InvoiceId}</p>
              <p><b>Ngày mua:</b> {invoice.CreateDate.ToString("dd/MM/yyyy")}</p>
              <table>
                <thead>
                  <tr>
                    <th>Tên sản phẩm</th>
                    <th>Số lượng</th>
                    <th>Giá</th>
                  </tr>
                </thead>
                <tbody>
                  {string.Join("", invoice.InvoiceDetails.Select(item => $@"
                  <tr>
                    <td>{item.ProductAttribute.Product.Name}</td>
                    <td>{item.Quantity}</td>
                    <td>{item.NewPrice.ToString("C", new CultureInfo("vi-VN"))}</td>
                  </tr>"))}
                </tbody>
              </table>
              <div class='alert'>
                Đơn hàng của bạn đã bị hủy. Nếu bạn có bất kỳ câu hỏi nào hoặc cần hỗ trợ thêm, xin vui lòng liên hệ với chúng tôi.
              </div>
              <div class='footer'>
                <p>Chúng tôi rất tiếc về sự bất tiện này và mong bạn thông cảm.</p>
                <p>Trân trọng,</p>
                <p>Đội ngũ DATN - Shop</p>
              </div>
            </div>
          </body>
        </html>"
            };

            return sendMail;
        }


    }
}
