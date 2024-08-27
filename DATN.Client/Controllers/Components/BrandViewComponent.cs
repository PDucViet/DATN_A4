using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN.Client.Controllers.Components
{
	public class BrandViewComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<ProductsViewComponent> _logger;

		public BrandViewComponent(HttpClient httpClient, ILogger<ProductsViewComponent> logger)
		{
			_httpClient = httpClient;
			_logger = logger;
		}
		private async Task<List<ProductVM>> GetProductsByBrand(string? branId)
		{
			var response = await _httpClient.GetAsync($"https://localhost:7095/api/Product/GetProductByBrand?brandId={branId}");
			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync();
			var products = JsonConvert.DeserializeObject<List<ProductVM>>(responseContent);

			return products ?? new List<ProductVM>();
		}
		public async Task<IViewComponentResult> InvokeAsync(string? brandId)
		{
			var products = new List<ProductVM>();
			if (brandId != null)
			{
				products = await GetProductsByBrand(brandId);
			}
			return View(products);



		}

	}
}
