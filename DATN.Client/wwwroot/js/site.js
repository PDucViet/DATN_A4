// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showToast(type, message) {
    toastr[type](message);
}



var navChildTitle = document.getElementsByClassName("nav-menu-child-1-title")
var navMenu = document.getElementsByClassName("nav-menu")[0]
navMenu.addEventListener("mouseover", () => {
    document.getElementsByClassName("nav-menu-child")[0].style.display = "flex"

})
navMenu.addEventListener("mouseleave", () => {
    document.getElementsByClassName("nav-menu-child")[0].style.display = "none"

})
for (var a of navChildTitle) {
    a.addEventListener("mouseover",async (e) => {
        document.getElementsByClassName("nav-menu-child")[0].style.width = "100%"
        document.getElementsByClassName("nav-menu-child-2")[0].style.display = "flex"
        document.getElementsByClassName("nav-menu-child-2")[0].style.flex = "1"
        //document.getElementsByClassName("nav-menu-child")[0].style.width = "100%!important"
        var cate0Id = e.target.getAttribute("content")
        await initialize(cate0Id);

    })
    a.addEventListener("mouseleave", (e) => {
        document.getElementsByClassName("nav-menu-child-2")[0].style.display = "flex"
        //document.getElementsByClassName("nav-menu-child")[0].style.width = "250px"

    })
}
document.getElementsByClassName("nav-menu-child")[0].addEventListener("mouseover", () => {

    document.getElementsByClassName("nav-menu-child-2")[0].style.display = "flex"
    document.getElementsByClassName("nav-menu-child-2")[0].style.flex = "1"
})
document.querySelector(".fa-magnifying-glass").addEventListener("click", function () {
    var searchValue = document.querySelector("input[type='text']").value.toLowerCase(); // Chuyển thành chữ thường
    if (searchValue) {
        window.location.href = `/Product/Search?query=${encodeURIComponent(searchValue)}`;
    }
});

document.querySelector("input[type='text']").addEventListener("keypress", function (e) {
    if (e.key === "Enter") {
        var searchValue = e.target.value.toLowerCase(); // Chuyển thành chữ thường
        if (searchValue) {
            window.location.href = `/Product/Search?query=${encodeURIComponent(searchValue)}`;
        }
    }
});
async function fetchCategories(cate0Id) {
    try {
        const response = await fetch(`https://localhost:7095/api/Category/GetAllCategoryByLevel0?categoryId=${cate0Id}`); // Thay đổi URL API của bạn tại đây
        if (!response.ok) {
            throw new Error('Failed to fetch categories');
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching categories:', error.message);
        return [];
    }
}
function setCurrentPage(pageIndex) {
    var currentPage = document.getElementById('current-page');
    currentPage.value = pageIndex;

    var formSearch = document.getElementById('form-search');
    formSearch.submit();
}
function confirmDelete(url) {
    if (confirm("Bạn chắc chắn muốn xóa?")) {
        window.location.href = url;
    }
}
function renderCategories(columns) {
    const columnsContainer = document.getElementsByClassName('nav-menu-child-2')[0];
    columnsContainer.innerHTML = ""
    columns.forEach((column, index) => {
        const colDiv = document.createElement('div');
        colDiv.className = 'col-3';

        column.forEach(category => {
            const navMenuChildBox = document.createElement('div');
            navMenuChildBox.className = 'nav-menu-child-2-box';

            // Header section
            const headerDiv = document.createElement('div');
            headerDiv.className = 'd-flex align-items-center';
            headerDiv.style.borderBottom = '1px solid rgba(0, 0, 0, 0.1)';
            headerDiv.style.margin = '8px 0';
            headerDiv.style.paddingBottom = '8px';

            const categoryName = document.createElement('h6');
            categoryName.style.margin = '0 8px 0 0';
            categoryName.style.fontSize = '12px';
            categoryName.style.textTransform = 'uppercase';
            categoryName.textContent = category.name;
            headerDiv.appendChild(categoryName);

            const btnAllSpan = document.createElement('span');
            btnAllSpan.className = 'btn-all';
            btnAllSpan.textContent = 'Xem tất cả';
            btnAllSpan.style.color = "#2f80ed";
            btnAllSpan.style.fontSize = "12px";
            headerDiv.appendChild(btnAllSpan);

            const caretIcon = document.createElement('i');
            caretIcon.className = 'fa-solid fa-caret-right';
            caretIcon.style.color = '#2f80ed';
            caretIcon.style.marginLeft = '4px';
            headerDiv.appendChild(caretIcon);

            navMenuChildBox.appendChild(headerDiv);
            // Subcategories section
            if (category.subCategories && category.subCategories.length > 0) {
                category.subCategories.forEach(subcategory => {
                    var subcategoryP = document.createElement('p');
                    subcategoryP.classList.add("subcategory")
                    subcategoryP.setAttribute("subCateId",subcategory.id)
                    subcategoryP.setAttribute("cateId",category.id)
                    subcategoryP.textContent = subcategory.name;
                    navMenuChildBox.appendChild(subcategoryP);
                });
            }

            colDiv.appendChild(navMenuChildBox);
        });

        columnsContainer.appendChild(colDiv);
        
    });
}

document.addEventListener("DOMContentLoaded", function () {
    

})


async function initialize(cate0Id) {
    const categories = await fetchCategories(cate0Id);
    const totalColumns = 4;
    const columns = Array.from({ length: totalColumns }, () => []);

    let currentColumn = 0;
    let itemCount = 0;
    const maxItemsPerColumn = 12;
    categories.forEach(category => {
        if (category.subCategories && category.subCategories.length > 0) {
            if (itemCount + category.subCategories.length + 1 > maxItemsPerColumn) {
                currentColumn++;
                itemCount = 0;
            }

            if (currentColumn >= totalColumns) {
                currentColumn = 0;
            }

            columns[currentColumn].push(category);
            itemCount += category.subCategories.length + 1;
        }
    });
    renderCategories(columns);
    var subcategories = document.getElementsByClassName("subcategory")
    for (var item of subcategories) {
        item.addEventListener("click", (e) => { 
            var cateLevel1 = fetch(`https://localhost:7095/api/Category/GetById/get-by-id?id=${e.target.getAttribute("subCateId") }`)
            localStorage.setItem("currentSelectedCate", e.target.getAttribute("subCateId"))
            window.location.href = `/categories/index?cateId=${e.target.getAttribute("cateId")}&subCateId=${e.target.getAttribute("subCateId")}`
        })

    }
}




// load san pham theo danh muc
document.querySelectorAll('.cate-item').forEach(item => {
    item.addEventListener('click', () => {
        var cateId = item.getAttribute('cateId');
        window.location.href = `/categories/index?cateId=${cateId}`;
    });
});



function setCurrentPage(pageIndex) {
    var currentPage = document.getElementById('current-page');
    currentPage.value = pageIndex;

    var formSearch = document.getElementById('form-search');
    formSearch.submit();
}

function confirmDelete(url) {
    if (confirm("Bạn chắc chắn muốn xóa?")) {
        window.location.href = url;
    }
}
document.getElementById('order-btn').addEventListener('click', function () {
    location.href = '/shippingorder/index';
});
document.getElementById("cart-btn").addEventListener("click", () => {
   
    location.href = "/cart/index"
      
})


// Hàm để xử lý sự kiện cuộn
function onScroll() {
    // Kiểm tra vị trí cuộn hiện tại
    var windowHeight = window.innerHeight;
    var scrollY = window.scrollY || window.pageYOffset;
    var bodyHeight = document.body.offsetHeight;
    if (window.scrollY >= 300) {
        var stickyBanners = document.querySelectorAll(".sticky-banner")
        for (var item of stickyBanners) {
            item.classList.remove("sticky-hide")
        }
    } else {
        var stickyBanners = document.querySelectorAll(".sticky-banner")
        for (var item of stickyBanners) {
            item.classList.add("sticky-hide")
        }
    }
    if (scrollY + windowHeight >= bodyHeight - 80) { // 100px trước khi đến cuối trang
        var stickyBanners = document.querySelectorAll(".sticky-banner")
        for (var item of stickyBanners) {
            item.classList.add("sticky-hide")
        }
    }
}

// Gắn sự kiện cuộn vào window
window.addEventListener('scroll', onScroll);