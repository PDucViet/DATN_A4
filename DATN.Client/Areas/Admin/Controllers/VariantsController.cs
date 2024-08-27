using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.AttributeVM.AttributeDynamic;
using DATN.Core.ViewModel.AttributeVM.AtttributeVariation;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class VariantsController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VariantsController(ClientService clientService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _clientService = clientService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            //https://localhost:7095/api/Variant/GetAllAttributeVariation


            var requestUrl = $"{ApiPaths.Variant}/GetAllAttributeVariation";
            var listAtributes = await _clientService.Get<List<AttributesVM>>(requestUrl);
            return View(listAtributes);
        }

        public async Task<IActionResult> Create()
        {
            //https://localhost:7095/api/AttributeValues
            var valuesRespon = await _clientService.Get<List<AttributeValueVM>>($"{ApiPaths.AttributeValues}/GetAllValueVariant");

            ViewBag.listAttributes = new SelectList((System.Collections.IEnumerable)valuesRespon, "AtributeValueId", "Value");

            return View();
        }
        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVariantVM attributeVM)
        {
            try
            {
                // https://localhost:7095/api/Variant/CreateAttributeVariation
                var createAttributeResponse = await _clientService.Post<CreateVariantVM>($"{ApiPaths.Variant}/CreateAttributeVariation", attributeVM);

                if (createAttributeResponse !=null )
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Fetch attribute values from API
                    var valuesResponse = await _clientService.Get<List<AttributeValueVM>>($"{ApiPaths.AttributeValues}/GetAllValueVariant");

                    // Check if the response is null or empty
                    if (valuesResponse != null && valuesResponse.Any())
                    {
                        ViewBag.listAttributes = new SelectList(valuesResponse, "AtributeValueId", "Value");
                    }
                    else
                    {
                        // Handle the case where the API response is null or empty
                        ModelState.AddModelError("", "Không có giá trị thuộc tính nào.");
                        ViewBag.listAttributes = new SelectList(Enumerable.Empty<AttributeValueVM>(), "AtributeValueId", "Value");
                    }

                    return View(attributeVM);
                }
            }
            catch (Exception)
            {
                // Fetch attribute values from API
                var valuesResponse = await _clientService.Get<List<AttributeValueVM>>($"{ApiPaths.AttributeValues}/GetAllValueVariant");

                // Check if the response is null or empty
                if (valuesResponse != null && valuesResponse.Any())
                {
                    ViewBag.listAttributes = new SelectList(valuesResponse, "AtributeValueId", "Value");
                }
                else
                {
                    // Handle the case where the API response is null or empty
                    ModelState.AddModelError("", "Không có giá trị thuộc tính nào.");
                    ViewBag.listAttributes = new SelectList(Enumerable.Empty<AttributeValueVM>(), "AtributeValueId", "Value");
                }

                return View(attributeVM);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id < -1)
            {
                return NotFound();
            }
            //https://localhost:7095/api/Attributes/GetById/1
            var attributeRespon = await _clientService.Get<AttributesVM>($"{ApiPaths.Attributes}/GetById/{id}");

            //getListValue

            return View(attributeRespon);
        }
        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateVariantVM attributeVM)
        {
            if (attributeVM == null)
            { 

                ////https://localhost:7095/api/AttributeValues
                //var valuesRespon = await _clientService.Get<List<AttributeValueVM>>($"{ApiPaths.AttributeValues}/GetAll");
                //ViewBag.listAttributes = new SelectList((System.Collections.IEnumerable)valuesRespon, "AtributeValueId", "Value");

                return RedirectToAction(nameof(Edit));
            }

            //https://localhost:7095/api/Attributes/UpdateAttributeDynamic
            var updateAttributeRespon = await _clientService.Put<UpdateVariantVM>($"{ApiPaths.Variant}/UpdateAttributeVariant", attributeVM);

            if (updateAttributeRespon != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật thất bại");
                ViewBag.TypeAttributes = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList(), "Value", "Text");

                return View(attributeVM);
            }
        }



    }
}
