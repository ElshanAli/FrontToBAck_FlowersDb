//$(document).on('click', '#delete-category', function () {

//    let id = $(this).val();
//    $.ajax({
//        method: 'POST',
//        url: "/adminpanel/category/delete?id=" + id,
//        success: function () {
//            console.log('success');
//            location.reload();

//        },

//    });

//});

let deleteCategory = document.getElementById('delete-category');

deleteCategory.addEventListener('click', function () {
    let id = this.value;
    fetch("/adminpanel/category/delete?id=" + id, {
        method: 'POST',

    })
        location.reload();
        
    

});