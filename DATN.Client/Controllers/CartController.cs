using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.Model;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.PromotionVM;
using DATN.Core.ViewModels.VNPayVM;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers
{
    public class CartController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;


        public CartController(ClientService clientService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _clientService = clientService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
            if (user==null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            var product = await _clientService.GetList<ProductVM>("https://localhost:7095/api/Product/GetAll");
            var voucher =await _clientService.GetList<VoucherUser>($"https://localhost:7095/api/VoucherUser/GetVoucherByUser?Id={user.UserId}");
            try
            {
                ViewData["voucher"] = voucher;
                ViewData["user"] = user;
                ViewData["product"] = product;               
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View();
        }
        public async Task<IActionResult> ListProduct()
        {
            var lst= await _clientService.GetList<ProductVM>("https://localhost:7095/api/Product/GetAll");
            return View(lst);
        }
        public IActionResult Pay(int typePayment, long money, int invoiceId)
        {
            try
            {
                //Get Config Info
                string vnp_Returnurl = _configuration["AppSettings:vnp_Returnurl"]; //URL nhan ket qua tra ve 
                string vnp_Url = _configuration["AppSettings:vnp_Url"]; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = _configuration["AppSettings:vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
                string vnp_HashSecret = _configuration["AppSettings:vnp_HashSecret"]; //Secret Key

                //Get payment input
                OrderInfo order = new OrderInfo();
                order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
                order.Amount = money; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
                order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
                order.CreatedDate = DateTime.Now;
                //Save order to db

                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

                if (typePayment == 1)
                {
                    vnpay.AddRequestData("vnp_BankCode", "VNBANK");
                }
                else if (typePayment == 2)
                {
                    vnpay.AddRequestData("vnp_BankCode", "INTCARD");
                }
                var ultils = new Utils(_httpContextAccessor);
                vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", ultils.GetIpAddress());

                vnpay.AddRequestData("vnp_Locale", "vn");

                vnpay.AddRequestData("vnp_OrderInfo", "Hóa đơn quyên góp" + order.OrderId);
                vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                //Add Params of 2.1.0 Version
                //Billing
                HttpContext.Session.SetInt32("invoiceId", invoiceId);
                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {

                ToastHelper.ShowError(TempData, ex.Message);

                return RedirectToAction("Error");
            }

        }
        public async Task<IActionResult> vnpay_return()
        {

            if (HttpContext.Request.Query.Count > 0)
            {
                int? invoiceId = HttpContext.Session.GetInt32("invoiceId");
                string vnp_HashSecret = _configuration["AppSettings:vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = HttpContext.Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (var s in vnpayData.Keys)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = HttpContext.Request.Query["vnp_SecureHash"];
                string TerminalID = HttpContext.Request.Query["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(HttpContext.Request.Query["vnp_Amount"]) / 100;
                string bankCode = HttpContext.Request.Query["vnp_BankCode"];



                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        ViewBag.displayMsg = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        TempData["paymentStatus"] = "Success";
                        TempData["invoiceId"] = invoiceId;

                        //Xu ly them moi donate

                        var currentUser = SessionHelper.GetObjectFromJson<UserInfo>(HttpContext.Session, "user");
                        await _clientService.Get($"{ApiPaths.Invoice}/ChangeStatus?invoiceId={invoiceId}&status={4}");

                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.displayMsg = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        TempData["paymentStatus"] = "Fail";
                        TempData["invoiceId"] = invoiceId;
                        await _clientService.Get($"{ApiPaths.Invoice}/ChangeStatus?invoiceId={invoiceId}&status={5}");
                    }
                    ViewBag.displayTmnCode = "Mã Website (Terminal ID):" + TerminalID;
                    ViewBag.displayTxnRef = "Mã giao dịch thanh toán:" + orderId.ToString();
                    ViewBag.displayVnpayTranNo = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.displayAmount = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    ViewBag.displayBankCode = "Ngân hàng thanh toán:" + bankCode;
                }
                else
                {
                    ViewBag.displayMsg = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return Redirect("/Cart/Index");
        }
    }
}
