using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.GHNVM;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ShippingOrderVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using CreateGHNOrderAdmin = DATN.Core.ViewModel.GHNVM.CreateGHNOrderAdmin;
using OrderItemsGHNAdmin = DATN.Core.ViewModel.GHNVM.OrderItemsGHNAdmin;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class InvoiceController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceController(ClientService clientService, IUnitOfWork unitOfWork)
        {
            _clientService = clientService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(InvoicePaging request)
        {
            InvoicePaging invoicePaging = new InvoicePaging();


            invoicePaging = await _clientService.Post<InvoicePaging>("https://localhost:7095/api/Invoice/GetInvoicePaging", request);

            if (invoicePaging == null)
            {
                return NotFound();
            }

            return View(invoicePaging);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _clientService.Get<InvoiceVM>($"https://localhost:7095/api/Invoice/GetById/get-by-id?id={id}");
            return View(result);

        }
        public async Task<IActionResult> CreateShippingOrder(int invoiceId)
        {
            var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(invoiceId);
            var to_name = invoice.Note.Split("-")[0];
            var to_phone = invoice.Note.Split("-")[1];
            var to_address = invoice.Note.Split("-")[2];
            var to_ward_code = invoice.Note.Split("-")[3];
            var to_district_id = invoice.Note.Split("-")[4];
            var cod_amount = invoice.Note.Split("-")[5];
            var listItem = new List<OrderItemsGHNAdmin>();
            foreach (var item in invoice.InvoiceDetails)
            {
                listItem.Add(new OrderItemsGHNAdmin()
                {
                    name = item.ProductAttribute.Product.Name,
                    quantity = item.Quantity
                });
            }
            var body = new CreateGHNOrderAdmin()
            {
                to_name = to_name,
                to_phone = to_phone,
                to_address = to_address,
                to_ward_code = to_ward_code,
                to_district_id = to_district_id,
                cod_amount = int.Parse(cod_amount),
                Items = listItem
            };
            using (HttpClient client = new HttpClient())
            {
                // URL của API
                string url = "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create";

                // Chuyển đổi object thành JSON string
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(body);

                // Tạo nội dung của body
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                // Thêm các header
                client.DefaultRequestHeaders.Add("Token", "af9239df-2402-11ef-8e53-0a00184fe694");
                client.DefaultRequestHeaders.Add("shop_Id", "192491");

                try
                {
                    // Gửi yêu cầu POST
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Kiểm tra nếu yêu cầu thành công
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc nội dung phản hồi từ API
                        string responseData = await response.Content.ReadAsStringAsync();
                        var lastData = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(responseData);
                        var a = await _clientService.Post("https://localhost:7095/api/ShippingOrder/Create", new CreateShippingOrderRepuest()
                        {
                            ShippingOrderCode = lastData.data.order_code,
                            ShippingFee = lastData.data.total_fee,
                            CustomerId = invoice.UserId,
                            InvoiceId = invoice.InvoiceId
                        });
                        invoice.Status = Core.Enum.InvoiceStatus.Delivery;
                        _unitOfWork.InvoiceRepository.Update(invoice);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {

                        Console.WriteLine("API call failed with status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> CancelInvoice(int invoiceId)
        {
            var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(invoiceId);
            invoice.Status = Core.Enum.InvoiceStatus.Cancel;
            if (invoice.VoucherUser != null)
            {
                invoice.VoucherUser.IsDeleted = false;
            }
            var decent = await _clientService.Get($"https://localhost:7095/api/Invoice/ChangeStatus2?invoiceId={invoiceId}" );
            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.SaveChanges();
            var content = CancelnvoiceContent.GenerateContentMail(invoice.User, invoice);
            var sendemail = await _clientService.Post("https://localhost:7095/api/SendMail/SendMail", content);
            return RedirectToAction("Index");
        }
    }
}
