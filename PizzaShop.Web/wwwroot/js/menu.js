
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
                setTimeout(function () {
                    window.location.reload();
                }, 500);
            }
            else
            {
                toastr.error(data.message);
            }
        }
    });
});

function editCategory(id, Name, Description){

    $('#edit-category-name').val(Name);
    $('#edit-category-description').val(Description);
    $('#edit-category-id').val(id);

}

$('#editCategoryForm').submit(function(e){
    e.preventDefault();
    let id=$('#edit-category-id').val();

    console.log(id);
    let category = $('#edit-category-name').val();
   let description = $('#edit-category-description').val();
    $.ajax({
        url: '/Menu/EditCategory/'+id,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({CategoryName: category, Description: description}),

        //settimeout of 300ms and reload window after that
        

        success: function(data){
            $('#editCategory').modal('hide');
            console.log(data);
            if(data.success)
            {
                toastr.success(data.message);
                setTimeout(function () {
                    window.location.reload();
                }, 500);
            }
            else
            {
                toastr.error(data.message);
            }
        }
    });
});


function openModel(id) {
    console.log(id);
    $("#deleteButton").click(function () {
        $.ajax({
            type: "DELETE",
            url: "/Menu/DeleteCategory/" + id,
            data: { id: id },
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                } else {
                    toastr.error(data.message);
                }
            },
            error: function () {
                toastr.error("Error");
            }
        });
    });
  }