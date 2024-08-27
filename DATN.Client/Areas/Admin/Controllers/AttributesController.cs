using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.AttributeVM.AttributeDynamic;
using DATN.Core.ViewModel.AttributeVM.AtttributeVariation;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AttributesController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AttributesController(ClientService clientService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _clientService = clientService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            //https://localhost:7095/api/Attributes/GetAllAttributeDynamic


            var requestUrl = $"{ApiPaths.Attributes}/GetAllAttributeDynamic";
            var listAtributes = await _clientService.Get<List<AttributesVM>>(requestUrl);
            return View(listAtributes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.TypeAttibutes = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            //https://localhost:7095/api/AttributeValues
            var valuesRespon = await _clientService.Get<List<AttributeValueVM>>($"{ApiPaths.AttributeValues}/GetAllValueDynamic");

            ViewBag.listAttributes = new SelectList((System.Collections.IEnumerable)valuesRespon, "AtributeValueId", "Value");

            return View();
        }
        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttributeValueVM attributeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TypeAttibutes = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList(), "Value", "Text");

                //https://localhost:7095/api/AttributeValues
                var valuesRespon = await _clientService.Get<List<AttributeValueVM>>($"{ApiPaths.AttributeValues}/GetAll");
                ViewBag.listAttributes = new SelectList((System.Collections.IEnumerable)valuesRespon, "AtributeValueId", "Value");

                return RedirectToAction(nameof(Create));
            }

            //https://localhost:7095/api/Attributes/CreateAttributeDynamic
            var createAttributeRespon = await _clientService.Post<CreateAttributeVM>($"{ApiPaths.Attributes}/CreateAttributeDynamic", attributeVM);

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id < -1)
            {
                return NotFound();
            }
            ViewBag.TypeAttibutes = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            //https://localhost:7095/api/Attributes/GetById/1
            var attributeRespon = await _clientService.Get<AttributesVM>($"{ApiPaths.Attributes}/GetById/{id}");

            return View(attributeRespon);
        }
        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateAttributeVM attributeVM)
        {
            if (attributeVM == null)
            {
                ViewBag.TypeAttibutes = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList(), "Value", "Text");

                ////https://localhost:7095/api/AttributeValues
                //var valuesRespon = await _clientService.Get<List<AttributeValueVM>>($"{ApiPaths.AttributeValues}/GetAll");
                //ViewBag.listAttributes = new SelectList((System.Collections.IEnumerable)valuesRespon, "AtributeValueId", "Value");

                return RedirectToAction(nameof(Edit));
            }
            
            //https://localhost:7095/api/Attributes/UpdateAttributeDynamic
            var updateAttributeRespon = await _clientService.Put<UpdateAttributeVM>($"{ApiPaths.Attributes}/UpdateAttributeDynamic", attributeVM);

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
