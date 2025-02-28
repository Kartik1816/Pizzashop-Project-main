

var controllers = ['Dashboard', 'UserList','RolesAndPermission']


var url = window.location.pathname;
var controllerName = controllers.find(controller => url.includes(controller));
 console.log(controllerName);

$(document).ready(function () {
    $(".sidebar-element").removeClass("sidebar-active");
    $(".common-span").removeClass("sidebar-active");
    $("#" + controllerName).addClass("sidebar-active");
    $("#" + controllerName + "-title").addClass("sidebar-font");
    console.log($("#" + controllerName));

    
    

    $(".sidebar-svg").each(function () {
        var src = $(this).attr("src");
        src = src.replace("Active.svg", ".svg");
    });
    var src = $("#" + controllerName + "-svg").attr("src");
    src = src.replace(".svg", "Active.svg"); 
    $("#" + controllerName + "-svg").attr("src", src);
    


    $('#logout').click(function(){
        document.cookie="token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        document.cookie="email=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        window.location.href="/Auth";
    })
});
