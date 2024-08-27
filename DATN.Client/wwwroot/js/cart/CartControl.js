function ShowInputPmtCode() {
    var form = document.getElementById("promotioncodebox_input");
    var arrow = document.getElementById("arrow");
    form.classList.toggle("show");

    // Thay đổi mũi tên khi mở/đóng form
    if (form.classList.contains("show")) {
        arrow.innerHTML = "&#9650;"; // Mũi tên lên
    } else {
        arrow.innerHTML = "&#9660;"; // Mũi tên xuống
    }
}
function checkPromoCode() {
    var promoInput = document.getElementById("promo_code_input");
    var applyButton = document.getElementById("apply_button");
    if (promoInput.value.trim() !== "") {
        applyButton.disabled = false;
        applyButton.style.backgroundColor = "#007bff"; // Màu xanh khi có mã giảm giá
    } else {
        applyButton.disabled = true;
        applyButton.style.backgroundColor = "#ccc"; // Màu xám đậm khi không có mã giảm giá
    }
}
document.addEventListener("DOMContentLoaded", function () {
    const provinceSelect = document.getElementById("provinceSelect");
    const districtSelect = document.getElementById("districtSelect");
    const wardSelect = document.getElementById("wardSelect");
    const token = "af9239df-2402-11ef-8e53-0a00184fe694";
    fetch("https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/province", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Token": token
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.code === 200) {
                const provinces = data.data;
                provinces.forEach(province => {
                    const option = document.createElement("option");
                    option.value = province.ProvinceID;
                    option.textContent = province.ProvinceName;
                    provinceSelect.appendChild(option);
                });
            } else {
                console.error("Failed to fetch provinces:", data.message);
            }
        })
        .catch(error => {
            console.error("Error fetching provinces:", error);
        });

    provinceSelect.addEventListener("change", function () {
        const selectedProvinceId = parseInt(this.value);
        districtSelect.innerHTML = '<option selected disabled hidden>Quận/Huyện</option>';
        wardSelect.innerHTML = '<option selected disabled hidden>Xã/Phường</option>';
        fetch(`https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id=${selectedProvinceId}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Token": token
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.code === 200) {
                    const districts = data.data;
                    districts.forEach(district => {
                        const option = document.createElement("option");
                        option.value = district.DistrictID;
                        option.textContent = district.DistrictName;
                        districtSelect.appendChild(option);
                    });
                } else {
                    console.error("Failed to fetch districts:", data.message);
                }
            })
            .catch(error => {
                console.error("Error fetching districts:", error);
            });
    });


    districtSelect.addEventListener("change", function () {
        const selectedDistrictId = parseInt(this.value);
        wardSelect.innerHTML = '<option selected disabled hidden>Xã/Phường</option>';
        fetch(`https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id=${selectedDistrictId}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Token": token
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.code === 200) {
                    const wards = data.data;
                    wards.forEach(ward => {
                        const option = document.createElement("option");
                        option.value = ward.WardCode;
                        option.textContent = ward.WardName;
                        wardSelect.appendChild(option);
                    });
                } else {
                    console.error("Failed to fetch wards:", data.message);
                }
            })
            .catch(error => {
                console.error("Error fetching wards:", error);
            });
    });
    function GetItemList() {
        const items = JSON.parse(localStorage.getItem("giohang"));
        const itemList = [];
        items.forEach(item => {
            const name = item.ten;
            const quantity = item.soluong;
            const height = 50;
            const width = 50;
            const length = 50;
            const weight = 5000;
            itemList.push({ name, quantity, height, width, length, weight });
        });
        return itemList;
    }
    function GetOrderData() { 
        const to_name = document.getElementById("customerFirstName").value + ' ' + document.getElementById('customerLastName');
        const to_phone = document.getElementById("customerPhoneNumber").value;
        const to_address = document.getElementById("address").value;
        const required_note = 'CHOXEMHANGKHONGTHU'
        const to_ward_code = wardSelect.value;      
        const service_type_id = 2;
        const to_district_id = parseInt(districtSelect.value);
        const items = GetItemList();
        const weight = items.reduce((total, item) => total + item.weight * item.quantity, 0);
        const height = 50;
        const width = 50;
        const length = 50; 
        return { weight, to_ward_code, service_type_id, to_district_id, items, to_name, to_phone, to_address, required_note, height, width, length };
    }
    function GetCodOrder() {
        const payment_type_id = 2;
        const to_name = document.getElementById("customerFirstName").value + ' ' + document.getElementById('customerLastName');
        const to_phone = document.getElementById("customerPhoneNumber").value;
        const to_address = document.getElementById("address").value;
        const required_note = 'CHOXEMHANGKHONGTHU'
        const to_ward_code = wardSelect.value;
        const service_type_id = 2;
        const to_district_id = parseInt(districtSelect.value);
        const items = GetItemList();
        const weight = items.reduce((total, item) => total + item.weight * item.quantity, 0);
        const height = 50;
        const width = 50;
        const length = 50;
        const items = GetItemList();
        const cod_amount = parseInt(document.querySelector('.pricetotal').textContent.replace(/[^\d.-]/g, ''));
        return { payment_type_id, weight, to_ward_code, service_type_id, to_district_id, items, to_name, to_phone, to_address, required_note, height, width, length, cod_amount };
    }
    async function CodOrder() {
        const dataOrder = GetCodOrder();
        console.log(JSON.stringify(dataOrder));
        try {
            const response = await fetch("https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Token": token,
                    "shop_Id": 192491
                },
                body: JSON.stringify(dataOrder)
            });
            if (!response.ok) {
                throw new Error('Failed to create invoice');
            }
            const dataresponse = await response.json();
            console.log(dataresponse)
            return dataresponse;

        } catch (error) {
            console.error('Error creating CodOrder:', error);
            return null;
        }
    }
    //async function ShippingOrder() {
    //    const dataShipping = GetShippingFee();
    //    console.log(JSON.stringify(dataShipping));
    //    try {
    //        const response = await fetch("https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee", {
    //            method: "POST",
    //            headers: {
    //                "Content-Type": "application/json",
    //                "Token": token,
    //                "shop_Id": 192491
    //            },
    //            body: JSON.stringify(dataShipping)
    //        });
    //        if (!response.ok) {
    //            throw new Error('Failed to create invoice');
    //        }
    //        const dataresponse = await response.json();
    //        console.log(dataresponse)
    //        return dataresponse;

    //    } catch (error) {
    //        console.error('Error creating GetShippingFee:', error);
    //        return null;
    //    }
    //}
    //document.querySelector('.addAddress').addEventListener('click', async function () {
        
    //    const Submit = await ShippingOrder();
    //});
    
});


