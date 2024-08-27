var selectedBrands = [];
var selectedCates = [];
var selectedPriceRanges = "";
document.addEventListener("DOMContentLoaded", async function () {
    // Lấy các phần tử DOM cần thiết
    var buttonFilter = document.getElementById('buttonFilter'); // Nút để mở menu bộ lọc
    var dropdownMenu = document.getElementById('dropdownMenu'); // Menu dropdown
    var btnCloseDropdown = document.getElementById('btnCloseDropdown'); // Nút để đóng menu dropdown
    var selectedFilters = document.querySelector('.selected-filters'); // Phần đã chọn các bộ lọc

    var currentSelectedCate = localStorage.getItem("currentSelectedCate")
    if (currentSelectedCate) {
        var cateLevel1Response = await fetch(`https://localhost:7095/api/Category/GetById/get-by-id?id=${currentSelectedCate}`)
        var cateLevel1 = await cateLevel1Response.json()
        addFilterButton("cate", (Number)(currentSelectedCate), cateLevel1.name)
    }
    localStorage.removeItem("currentSelectedCate")
    console.log(selectedCates)

    // Xử lý sự kiện khi click vào nút bộ lọc để hiển thị/ẩn menu dropdown
    buttonFilter.addEventListener('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        dropdownMenu.classList.toggle('show');
    });

    // Xử lý sự kiện click bên ngoài menu dropdown để ẩn nó đi
    document.addEventListener('click', function (event) {
        if (!buttonFilter.contains(event.target) && !dropdownMenu.contains(event.target)) {
            dropdownMenu.classList.remove('show');
        }
    });

    // Xử lý sự kiện click vào nút đóng của menu dropdown để ẩn menu đi
    btnCloseDropdown.addEventListener('click', function () {
        dropdownMenu.classList.remove('show');
    });

    // Ngăn chặn sự kiện lan truyền khi click vào menu dropdown
    dropdownMenu.addEventListener('click', function (event) {
        event.stopPropagation();
    });

    // Hàm định dạng số tiền thành đơn vị tiền tệ Việt Nam
    function formatCurrency(value) {
        return value.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }).replace(/₫/g, 'VND');
    }

    // Hàm chuyển đổi chuỗi tiền thành số nguyên
    function parseCurrency(value) {
        return parseInt(value.replace(/[^0-9]/g, ''), 10);
    }

    // Hàm thêm nút bộ lọc
    function addFilterButton(type, value, label) {
        // Kiểm tra nếu bộ lọc đã tồn tại thì không thêm nữa
        var existingFilter = Array.from(selectedFilters.children).find(function (child) {
            return child.dataset.type === type && child.dataset.value === value; // Kiểm tra cả value và id
        });

        if (existingFilter) {
            return; // Nếu bộ lọc đã tồn tại thì không làm gì cả
        }
        if (type == "brand") {
            selectedBrands.push((Number)(value))
        } else if (type == "price") {
            selectedPriceRanges = value
        } else if (type == "cate") {
            selectedCates.push((Number)(value))
        }
        

        var filterBtn = document.createElement('button');
        filterBtn.classList.add('btn', 'btn-selected', 'm-2');
        filterBtn.dataset.type = type;
        filterBtn.dataset.value = value;
        filterBtn.dataset.label = label;
        filterBtn.innerHTML = `${label} <i class="bi bi-x ms-1"></i>`;

        // Thêm sự kiện click vào biểu tượng x để xóa bộ lọc
        filterBtn.addEventListener('click', function (event) {
            event.stopPropagation();
            selectedFilters.removeChild(filterBtn);
            console.log(type)
            // Cập nhật lại các mảng sau khi xóa bộ lọc
            if (type === 'brand') {
                selectedBrands = selectedBrands.filter(b => b !== parseInt(value));
            } else if (type === 'price') {
                selectedPriceRanges = ""
            } else if (type === 'cate') {
                selectedCates = selectedCates.filter(b => b !== parseInt(value));
            }
        });

        // Thêm nút bộ lọc vào phần đã chọn
        selectedFilters.appendChild(filterBtn);
    }

    // Hàm xóa tất cả bộ lọc khoảng giá từ button
    function clearPriceFilters() {
        Array.from(selectedFilters.children).forEach(function (child) {
            if (child.dataset.type === 'price' || child.dataset.type === 'range') {
                selectedFilters.removeChild(child);
            }
        });
        selectedPriceRanges = []; // Xóa hết các giá trị trong mảng selectedPriceRanges
    }

    // Hàm khởi tạo trang danh mục
    function initializeCategoryPage() {
        // Bạn có thể thêm logic khởi tạo bổ sung ở đây nếu cần
    }

    // Gọi hàm khởi tạo
    initializeCategoryPage();

    // Đảm bảo rằng hàm toggleSelectedFilter có thể truy cập toàn cục nếu cần
    window.toggleSelectedFilter = function (type, value, label) {
        if (type === 'price') {
            clearPriceFilters();
        }
        // Thêm hoặc loại bỏ giá trị khỏi các mảng tùy thuộc vào trạng thái
        document.querySelectorAll('.filter-btn').forEach(function (btn) {
            if (btn.dataset.type === 'brand') {
                let brandId = parseInt(btn.dataset.value);
                if (!selectedBrands.includes(brandId)) {
                    selectedBrands.push(brandId);
                }
            } else if (btn.dataset.type === 'price') {
                let priceRange = btn.dataset.value;
                if (!selectedPriceRanges== priceRange) {
                    selectedPriceRanges=priceRange;
                }
            } else if (btn.dataset.type === 'cate') {
                let cateId = btn.dataset.value;
                if (!selectedCates.includes(cateId)) {
                    selectedBrands.push(cateId);
                }
            }
        });
        // Thêm bộ lọc nếu chưa tồn tại
        addFilterButton(type, value, label);
    };



    
    function applyFilter() {
        // Lấy URL hiện tại
        var url = new URL(window.location.href);

        // Sử dụng URLSearchParams để lấy các tham số query string
        var params = new URLSearchParams(url.search);

        // Lấy giá trị của tham số 'page' và 'category'
        var cate = params.get('cateId');
        

        let minPrice = (Number)(selectedPriceRanges.split("-")[0])
        let maxPrice = (Number)(selectedPriceRanges.split("-")[1])
        console.log(selectedBrands)
        // Tạo đối tượng JSON để gửi
        let productFilter = {
            BrandId: selectedBrands,
            CateId: selectedCates,
            MinPrice: minPrice,
            MaxPrice: maxPrice,
            Cate: (Number)(cate)
        };
        console.log(JSON.stringify(productFilter))
        const requestOptions = {
            method: 'POST', // phương thức gửi là 
            headers: {
                'Content-Type': 'application/json' // loại nội dung gửi đi là JSON
            },
            body: JSON.stringify(productFilter) // chuyển đổi dữ liệu sang chuỗi JSON

        };

        fetch(`https://localhost:7095/api/Product/GetProductByFilter`, requestOptions)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(async(data) => {
                var html = ''
                for (var item of data) {
                    console.log(item)
                    var formattedPrice = new Intl.NumberFormat('vi-VN', {
                        style: 'decimal',
                        minimumFractionDigits: 0,
                        maximumFractionDigits: 0
                    }).format(item.productAttributes.find(p => p.isDefault == true).salePrice);
                    var formattedNewPrice = new Intl.NumberFormat('vi-VN', {
                        style: 'decimal',
                        minimumFractionDigits: 0,
                        maximumFractionDigits: 0
                    }).format(item.defaultPrice);
                    var productRatingResponse = await fetch(`https://localhost:7095/api/Product/GetProductRating?productId=${item.id}`)
                    var productRateCountResponse = await fetch(`https://localhost:7095/api/Product/GetProductRateCount?productId=${item.id}`)
                    var productRating = await productRatingResponse.json()
                    var productRateCount = await productRateCountResponse.json()
                    html += `
                    <div class="col-md-2 product-item align-items-center" style="width: 20%; padding-left: 0; padding-right: 0;">
                    <div class="card" style="border-radius: 0!important; height: 412px;">
                                            <img src="${item.imagePath}" class="card-img-top" alt="${item.name} Image" style="height: 220px;">
                                    <div class="card-body">
                                            <div class="card-title text-ellipsis" style="line-height: 21px; height: 63px; font-size:14px;">${item.name}</div>
                                        <p class="card-text">
                                                    <span style="position: relative; font-size:14px;color:#666;text-decoration:line-through">
                                                    ${formattedPrice}đ
                                            </span>
                                                <strong style="font-size: 14px; color: #EB5757; background-color: #FFF0E9; margin-left: 8px;">-${item.discount}%</strong>
                                            <br />
                                                    <span style="font-size:18px;" class="text-danger fw-bolder">${formattedNewPrice}đ</span>
                                        </p>
                                        <div class="rating d-flex align-items-center">
                                                <span class="text-dark" style="padding-right: 8px; font-size:14px;">${productRating}</span>
                                                <i class="fa fa-star" style="padding-right: 8px; font-size:14px;"></i> <!-- Thẻ i tạo ra ngôi sao -->
                                            <p style="margin:0;font-size:14px;color:#999999">(${productRateCount})</p>
                                        </div>
                                    </div>
                                         <div product-id="${item.id}" class="product-detail">Xem chi tiết</div>
                                </div>
                            </div>`
                }
                document.getElementById("productContainer").innerHTML = html
                if (document.getElementById('loadMoreBtn')) {
                    document.getElementById('loadMoreBtn').style.display="none"
                }
                var products = document.getElementsByClassName("product-detail")
                for (product of products) {
                    product.addEventListener("click", (e) => {
                        var id = e.target.getAttribute("product-id")
                        location.href = `/product/edit?id=${id}`
                    })
                }
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }
    window.applyFilter = applyFilter


})
