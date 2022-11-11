//const shoppingCartMenu = document.querySelector(".shop-carts");
function addToCart(ev) {
    console.log("is working");
    const productId = ev.target.getAttribute("data-id");
    fetch(`/basket/AddToBasket?productId=${productId}`)
        .then(response => response.text())
        .then(() => {
            updateBasketCount();
        });
}

function updateBasketCount() {
    fetch(`/basket/GetBasketCount`)
        .then(response => response.text())
        .then(data => {
            const basketCount = document.querySelector(".shop-cart > .rounded-circle")
            basketCount.innerText = data;
        });
}

updateBasketCount();

const addToCartBtns = document.querySelectorAll('.add-to-cart');
addToCartBtns.forEach(addToCartBtn => addToCartBtn.addEventListener('click', addToCart));