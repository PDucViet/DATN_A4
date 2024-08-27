using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.PromotionVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DATN.Client.Controllers.Components
{
    public class ProductsViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductsViewComponent> _logger;
        private readonly ClientService _clientService;
        public ProductsViewComponent(HttpClient httpClient, ILogger<ProductsViewComponent> logger, ClientService clientService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _clientService = clientService;
        }

        private async Task<List<ProductVM>> GetProductsByCategory(string? categoryId)
        {
            var products = await _clientService.GetList<ProductVM>($"{ApiPaths.Product}/GetProductByCategory?categoryId={categoryId}");
            //var response = await _httpClient.GetAsync($"https://localhost:7095/api/Product/GetProductByCategory?categoryId={categoryId}");
            //response.EnsureSuccessStatusCode();
            //var responseContent = await response.Content.ReadAsStringAsync();
            //var products = JsonConvert.DeserializeObject<List<ProductVM>>(responseContent);

            return products ?? new List<ProductVM>();
        }
        private async Task<List<ProductVM>> GetProductsByPromotion(int promotionId)
        {
            var products = await _clientService.GetList<ProductVM>($"{ApiPaths.Product}/GetProductByPromotion?promotionId={promotionId}");
            return products ?? new List<ProductVM>();
        }
        public async Task<IViewComponentResult> InvokeAsync(string? categoryId, int? promotionId)
        {
            var products = new List<ProductVM>();

            if (categoryId != null)
            {
                products = await GetProductsByCategory(categoryId);
            }

            if (promotionId.HasValue)
            {
                var promotionProducts = await GetProductsByPromotion(promotionId.Value);
                // Combine the two lists while avoiding duplicates
                products.AddRange(promotionProducts);
                products = products.Distinct().ToList();
            }

            foreach (var product in products)
            {
                var productRating = await _clientService.Get<double>($"{ApiPaths.Product}/GetProductRating?productId={product.Id}");
                var productRateCount = await _clientService.Get<int>($"{ApiPaths.Product}/GetProductRateCount?productId={product.Id}");
                product.Rating = productRating;
                product.RateCount = productRateCount;
            }

            return View(products ?? new List<ProductVM>());
        }
    }
}
