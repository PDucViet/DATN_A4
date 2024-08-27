using Azure;
using DATN.Client.Services;
using DATN.Core.Model;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AddressController : Controller
    {
        private readonly ClientService _clientService;
        public AddressController(ClientService _client)
        {
            _clientService = _client;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _clientService.GetList<AddressVM>("https://localhost:7095/api/Address/GetAll");
        //    return View(result);

        //}

        public async Task<IActionResult> Index(AddressPaging request)
        {
            AddressPaging partnerPaging = new AddressPaging();


            partnerPaging = await _clientService.Post<AddressPaging>("https://localhost:7095/api/Address/GetAddressPaging", request);

            if (partnerPaging == null)
            {
                return NotFound();
            }

            return View(partnerPaging);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _clientService.Get<AddressVM>($"https://localhost:7095/api/Address/GetById/get-by-id?id={id}");
            return View(result);

        }
        public IActionResult Creat() // Tạo view rỗng
        {
            return View();
        }
        public async Task<IActionResult> CreatAddress(Address obj)
        {
            string requestURL = $"https://localhost:7095/api/Address/Create";
            var HttpClient = new HttpClient();



            var response = await HttpClient.PostAsJsonAsync(requestURL, obj);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
            //var originlst = await _clientService.Post<AddressVM>("https://localhost:7095/api/Address/Create", obj);
            //return View(originlst);

        }
        public async Task<IActionResult> Edit(string id) // Tlấy view rỗng từ Sv vừa chọn
        {
            //// vìa là sửa nên chúng ta cần truyền được dữ liệu của sv sang
            string requestURL = $"https://localhost:7095/api/Address/GetById/get-by-id?id={id}";
            var HttpClient = new HttpClient();

            var response = await HttpClient.GetAsync(requestURL); // Truyền cả obj theo requets url
            string apiData = await response.Content.ReadAsStringAsync();
            var sp = JsonConvert.DeserializeObject<AddressVM>(apiData);
            return View(sp); // truyền list vừa nhận được sang view
        }

        public async Task<IActionResult> EditAddress(AddressVM address)// Thực hiện lấy data từ view và sửa trong csdl
        {
          
            var result = await _clientService.Put<AddressVM>($"https://localhost:7095/api/Address/Update/edit-address?id={address.AddressID}", address);
            if (result != null)
            {
                 return RedirectToAction("Index");
            }
               
            else { return BadRequest(); }

        }

        public async Task<IActionResult> DeleteAddress(int id)// Thực hiện lấy data từ view và sửa trong csdl
        {

            var result = await _clientService.Delete<AddressVM>($"https://localhost:7095/api/Address/Delete?id={id}");
            if (result != null)
            {
                return RedirectToAction("Index");
            }

            else { return BadRequest(); }

        }


    }
}
