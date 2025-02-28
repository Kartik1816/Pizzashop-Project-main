var changesInPermission =[];
function changePermission(roleId,permissionId,permissionType)
{
    var Id=permissionType+roleId+permissionId;

    var value=document.getElementById(Id).checked;

    let obj ={
        RoleId:roleId,
        PermissionId:permissionId,
        PermissionType:permissionType,
        PermissionValue:value
    }
    if(changesInPermission.findIndex(c=>c.roleId == roleId && c.permissionId==permissionId) === -1)
    {
        changesInPermission.push(obj);
    }
    else{
        changesInPermission[changesInPermission.findIndex(c=>c.roleId == roleId && c.permissionId==permissionId)]=obj;
    }
    
}

function editPermission()
{
    console.log("clicked");
    console.log(changesInPermission);

    $.ajax({
        type : 'POST',
        url : '/RolesAndPermission/EditPermission',
        contentType : 'application/json',
        data : JSON.stringify(changesInPermission),

        success : function (response) {
            console.log(response);
            if(response.success)
            {
                toastr.success(response.message)
                setTimeout(function(){
                    window.location.reload();
                },500);
            }
            else{
                toastr.error(response.message)
            }
        }
    });
    
}