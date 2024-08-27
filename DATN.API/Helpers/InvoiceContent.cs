using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Models;
using DATN.Core.ViewModels.SendMailVM;
using System.Globalization;

namespace DATN.API.Helpers
{
    public class InvoiceContent
    {

        public static SendMailVM GenerateContentMail(AppUser user, Invoice invoice)
        {
            SendMailVM sendMail = new SendMailVM();
            sendMail.Email = user.Email;
            sendMail.Subject = "DATN - New Product";

            // Tạo chuỗi HTML cho danh sách chi tiết hóa đơn
            string ListInvoiceDetail = "";
            foreach (var item in invoice.InvoiceDetails)
            {
                ListInvoiceDetail += "<tr><td>" + item.ProductAttribute.Product.Name + "</td><td>" + item.Quantity + "</td><td>" + item.NewPrice.ToString("N0") + " đ</td></tr>";
            }

            // Tính toán tổng số tiền
            var total = invoice.InvoiceDetails.Sum(p => p.Quantity * p.NewPrice);
            double discount = 0.0;

            // Kiểm tra và tính toán giảm giá nếu có voucher
            if (invoice.VoucherUserId != null)
            {
                var voucher = invoice.VoucherUser.Voucher;
                double? v = total * (voucher.DiscountByPercent / 100.0);
                discount = (double)(v + (double)voucher.DiscountByPrice);
            }

            // Tính số tiền sau khi giảm giá
            var result = total - discount;

            // Tạo nội dung email
            string content = @"
                    <!DOCTYPE html>
                    <html lang='en'>
                      <head>
                        <meta charset='UTF-8' />
                        <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                        <title>Document</title>
                        <script
                          src='https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js'
                          integrity='sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM'
                          crossorigin='anonymous'
                        ></script>
                        <link
                          href='https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css'
                          rel='stylesheet'
                          integrity='sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC'
                          crossorigin='anonymous'
                        />
                      </head>
                      <body>
                        <div class='container'>
                          <h3 class='text-center' style='color: #D82D8B;'>DATN - New Product</h3>
                          <b class=''>Xin chào " + user.FullName + @"</b>
                          <br>
                            Rất cảm ơn bạn đã mua hàng tại shop.
                          </br>
                          Chúng tôi đến từ DATN - Shop, chúng tôi xin gửi đến bạn hóa đơn của bạn.
                          <div class='my-3'>Mã hóa đơn: " + invoice.InvoiceId + @"</div>
                            <table class='table'>
                                <thead>
                                    <tr>
                                        <th scope='col'>Tên sản phẩm</th>
                                        <th scope='col'>Số lượng</th>
                                        <th scope='col'>Giá</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    " + ListInvoiceDetail + @"
                                </tbody>    
                            </table>
                          <div class='my-3'>
                            <svg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='currentColor' class='bi bi-alarm' viewBox='0 0 16 16'>
                              <path d='M8.5 5.5a.5.5 0 0 0-1 0v3.362l-1.429 2.38a.5.5 0 1 0 .858.515l1.5-2.5A.5.5 0 0 0 8.5 9z'/>
                              <path d='M6.5 0a.5.5 0 0 0 0 1H7v1.07a7.001 7.001 0 0 0-3.273 12.474l-.602.602a.5.5 0 0 0 .707.708l.746-.746A6.97 6.97 0 0 0 8 16a6.97 6.97 0 0 0 3.422-.892l.746.746a.5.5 0 0 0 .707-.708l-.601-.602A7.001 7.001 0 0 0 9 2.07V1h.5a.5.5 0 0 0 0-1zm1.038 3.018a6 6 0 0 1 .924 0 6 6 0 1 1-.924 0M0 3.5c0 .753.333 1.429.86 1.887A8.04 8.04 0 0 1 4.387 1.86 2.5 2.5 0 0 0 0 3.5M13.5 1c-.753 0-1.429.333-1.887.86a8.04 8.04 0 0 1 3.527 3.527A2.5 2.5 0 0 0 13.5 1'/>
                            </svg> Ngày Mua bán : " + invoice.CreateDate.ToString("dd/MM/yyyy") + @"
                          </div>
                          <div>Thành tiền (Chưa bao gồm phí vận chuyển)<b>&nbsp;" + result.ToString("N0") + @" đ</b></div>
                          <div class='my-3'>Vui lòng kiểm tra trạng thái đơn hàng thường xuyên và đừng quên để lại đánh giá cho shop nhé!</div>
                          <div class='my-3'>DATN - Shop rất cảm ơn sự đồng hành của bạn!</div>
                        </div>
                      </body>
                    </html>
                    ";
            sendMail.Content = content;
            return sendMail;
        }


    }
}
