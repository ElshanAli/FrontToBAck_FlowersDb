let loadBtn = document.getElementById('loadMoreProduct');
let productList = document.getElementById('productsList');
let toPass = 4;
loadBtn.addEventListener('click', function () {
 
    fetch('/product/PartialProduct?toPass=' + toPass)
        .then((reponse) => reponse.text())
        .then((data) => {
            productList.innerHTML += data;           
            toPass += 4;
            let productCount = document.getElementById('product-Count').value;          
            if (toPass >= productCount) {
                loadBtn.style.display = 'none';
            }

        });

});