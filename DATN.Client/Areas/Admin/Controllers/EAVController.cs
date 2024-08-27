using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    [Route("Admin/[controller]/[action]")]
    public class EAVController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EAVController(ClientService clientService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _clientService = clientService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var requestUrl = $"{ApiPaths.EAV}/GetAllAsync";
            var listAtributes = await _clientService.Get<List<AttributesVM>>(requestUrl);
            return View(requestUrl);
        }


    }
}
