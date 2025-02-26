

var controllers = ['Dashboard', 'UserList']


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
    
});
