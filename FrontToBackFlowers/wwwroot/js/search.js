var searchInput = document.getElementById('input-search');

searchInput.addEventListener('keyup', function () {
    var searchValue = searchInput.value;
    var productList = document.querySelector('#listofproduct');
    if (searchValue.length == 0) {
        productList.innerHTML = '';
    } else {
        fetch('/home/Search?searchProduct=' + searchValue)
            .then((response) => response.text())
            .then((data) => {
                productList.innerHTML = data
            });
    }


});