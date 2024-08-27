using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.ViewModel.ListProductCompVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.PromotionVM;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers.Components
{
    public class SearchProductViewComponent : ViewComponent
    {
        private readonly ClientService _clientService;
        private readonly ILogger<SearchProductViewComponent> _logger;
        public SearchProductViewComponent(ClientService clientService, ILogger<SearchProductViewComponent> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? search, int page = 1, int pageSize = 10)
        {
            // Gọi API để lấy dữ liệu phân trang
            var productsPaging = await _clientService.Get<ProductPaging>($"{ApiPaths.Product}/GetProductBySearch?search={search}&page={page}&pageSize={pageSize}");

            // Tạo danh sách sản phẩm và số lượng sản phẩm
            var listProductVM = new List<ProductVM>();
            int totalItems = 0;

            if (productsPaging != null)
            {
                totalItems = productsPaging.TotalRecord; // Lấy tổng số bản ghi từ sản phẩm phân trang

                if (productsPaging.Items != null)
                {
                    foreach (var item in productsPaging.Items)
                    {
                        // Lấy đánh giá và số lượng đánh giá cho từng sản phẩm
                        var productRating = await _clientService.Get<double>($"{ApiPaths.Product}/GetProductRating?productId={item.Id}");
                        var productRateCount = await _clientService.Get<int>($"{ApiPaths.Product}/GetProductRateCount?productId={item.Id}");

                        item.Rating = productRating;
                        item.RateCount = productRateCount;
                    }

                    // Cập nhật danh sách sản phẩm
                    listProductVM = productsPaging.Items;
                }
            }

            // Tạo ViewModel cho thông tin phân trang và danh sách sản phẩm
            var viewModel = new ProductPaging()
            {
                Items = listProductVM,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                TotalRecord = totalItems
            };

            // Cung cấp thông tin phân trang cho View
            return View(viewModel);
        }




    }
}
