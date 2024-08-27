function setCurrentPage(pageIndex) {
    var currentPage = document.getElementById('current-page');
    currentPage.value = pageIndex;

    var formSearch = document.getElementById('form-search');
    formSearch.submit();
}
function showToast(type, message) {
    toastr[type](message);
}