using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN.Client.Controllers.Components
{
    public class GridProductsViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductsViewComponent> _logger;
        private readonly ClientService _clientService;

        public GridProductsViewComponent(HttpClient httpClient, ILogger<ProductsViewComponent> logger, ClientService clientService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _clientService = clientService;
        }

        private async Task<List<ProductVM>> GetProductsByCategory(int categoryId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7095/api/Product/GetProductByCategory?categoryId={categoryId}");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductVM>>(responseContent) ?? new List<ProductVM>();
            foreach (var item in products)
            {
                var productRating = await _clientService.Get<double>($"{ApiPaths.Product}/GetProductRating?productId={item.Id}");
                var productRateCount = await _clientService.Get<int>($"{ApiPaths.Product}/GetProductRateCount?productId={item.Id}");
                item.Rating = productRating;
                item.RateCount = productRateCount;
            }
            return products;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var products = new List<ProductVM>();
            if (categoryId != null)
            {
                products = await GetProductsByCategory(categoryId);
            }
            return View(products);


        }
    }
}
