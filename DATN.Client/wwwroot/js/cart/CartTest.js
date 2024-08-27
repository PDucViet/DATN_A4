var giohang = [];

//function loadCart() {
//    if (window.localStorage) {
//        giohang = JSON.parse(window.localStorage.getItem("giohang")) || [];
//    }
//    updateCartDisplay();
//}

//function updateCartDisplay() {
//    var sosp = 0;
//    var tongtien = 0;

//    for (var i = 0; i < giohang.length; i++) {
//        sosp += giohang[i].soluong;
//        tongtien += parseFloat(giohang[i].dongia) * parseFloat(giohang[i].soluong);
//    }

//    document.getElementById("giohang").innerText = "Số lượng sản phẩm: " + sosp + " | Tổng tiền: " + tongtien.toFixed(2);
//}

async function addToCart(selectedTTD) {
    if (!document.getElementsByClassName("nav-profile")[0]) {
        location.href = "/Identity/Account/Login"
    } else {
        const response = await fetch(`https://localhost:7095/api/ProductAtribute/GetDataCustomById?productAttributeId=${selectedTTD}`);
        if (!response.ok) {
            throw new Error('Failed to fetch');
        }

        const data = await response.json();
        console.log(data)

        var exist = false;

        for (var i = 0; i < giohang.length; i++) {
            if (giohang[i].maTTD === data.id) {
                giohang[i].soluong += 1;
                exist = true;
                break;
            }
        }

        if (!exist) {
            var sanpham = {
                ma: data.product.id,
                ten: data.product.name,
                dongia: parseFloat(data.salePrice),
                giamoi: parseFloat(data.afterDiscountPrice),
                soluong: 1,
                img: data.product.imagePath,
                soluongconlai: data.quantity,
                maTTD: data.id,
                tenTTD: data.attributeValue.value,
                loaiTTD: data.attributeValue.attributes.name,
                gianhap: data.puscharPrice
            };
            giohang.push(sanpham);
        }

        localStorage.setItem(`giohang-${document.getElementsByClassName("user-email")[0].innerHTML}`, JSON.stringify(giohang));
        var dataLocal = JSON.parse(localStorage.getItem(`giohang-${document.getElementsByClassName("user-email")[0].innerHTML}`))
        document.querySelector(".count-cart").innerHTML = dataLocal.length
        alert("Thêm sản phẩm vào giỏ hàng thành công");

    }
}

function showCart() {
    var txt = '';
    for (var i = 0; i < giohang.length; i++) {
        txt += giohang[i].ma + ': ' + giohang[i].ten + ' - ' + giohang[i].dongia + ' x ' + giohang[i].soluong + ' = ' + (parseFloat(giohang[i].dongia) * parseFloat(giohang[i].soluong)).toFixed(2) + '\n';
    }
    alert(txt);
}

//document.addEventListener("DOMContentLoaded", function () {
//    loadCart();
//})
function updateCart(id, quantity) {
    for (var i = 0; i < giohang.length; i++) {
        if (giohang[i].ma === id) {
            giohang[i].soluong = quantity;
            if (giohang[i].soluong <= 0) {
                giohang.splice(i, 1);
            }
            break;
        }
    }
    window.localStorage.setItem("giohang", JSON.stringify(giohang)); 
    alert("Cập nhật giỏ hàng thành công");
    updateCartDisplay();
}
function deleteFromCart(id) {
    for (var i = 0; i < giohang.length; i++) {
        if (giohang[i].ma == id) {
            giohang.splice(i, 1); 
            break;
        }
    }
    window.localStorage.setItem("giohang", JSON.stringify(giohang)); 
    alert("Xóa sản phẩm khỏi giỏ hàng thành công");
    updateCartDisplay();
}   

