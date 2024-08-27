using DATN.Core.Model.Product;
using DATN.Core.ViewModel.BrandVM;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DATN.Client.Controllers.Components
{
    public class FilterViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FilterViewComponent> _logger;
        public FilterViewComponent(HttpClient httpClient, ILogger<FilterViewComponent> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IViewComponentResult> InvokeAsync(int cateId, int parrentCateId)
        {
            ViewBag.cateId = cateId;
            ViewBag.parrentCateId = parrentCateId;
            return View();
        }
    }
}
