

function changeColor(item){
    let categories = ['sandwich','pizza','burger','pasta','salads','dessert','sides','dips'];
    categories.forEach(function(category){
        document.getElementById(`blue-${category}`).classList.add('d-none');
        document.getElementById(`gray-${category}`).classList.remove('d-none');
        document.getElementById(`span-${category}`).classList.remove('active-category');
    });
    document.getElementById(`blue-${item}`).classList.remove('d-none');
    document.getElementById(`gray-${item}`).classList.add('d-none');
    document.getElementById(`span-${item}`).classList.add('active-category');
}


addEventListener("mouseover", function(event) {
    let categories = ['sandwich','pizza','burger','pasta','salads','dessert','sides','dips'];
    this.onmouseover = function(event) {
        let target = event.target;
        if (target.tagName != 'SPAN') return;
        categories.forEach(function(category){
            document.getElementById(`blue-${category}`).classList.add('d-none');
            document.getElementById(`gray-${category}`).classList.remove('d-none');
            document.getElementById(`span-${category}`).classList.remove('active-category');
        });
        document.getElementById(`blue-${target.id}`).classList.remove('d-none');
        document.getElementById(`gray-${target.id}`).classList.add('d-none');
        document.getElementById(`span-${target.id}`).classList.add('active-category');
    }
});

$('#addCategoryForm').submit(function(e){
    e.preventDefault();
    let category = $('#category-name').val();
   let description = $('#category-description').val();
    $.ajax({
        url: '/Menu/AddCategory',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({CategoryName: category, Description: description}),
        success: function(data){
            $('#addCategory').modal('hide');
            console.log(data);
            if(data.success)
            {
                toastr.success(data.message);
            }
            else
            {
                toastr.error(data.message);
            }
        }
    });
});
