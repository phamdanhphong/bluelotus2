$(function () {
    var nut = document.getElementById('chensubmenu');
    nut.addEventListener('click', function () {
        var thanh = document.getElementById("mainmenu").style.visibility;
        if (thanh == "hidden") {
            document.getElementById('mainmenu').style.visibility = "visible";
        }
        else {
            document.getElementById('mainmenu').style.visibility = "hidden"
        }
    })
})

//function activesubmenu(elm) {
//    $(elm).parent().toggleClass("acset");
//}

$(document).ready(function () {
    $('.menucon').click(function () {
        $(this).toggleClass("active");
    });
    $('.SubdmsanphamHome .tieude').click(function () {
        $('.SubdmsanphamHome').toggleClass("active");
    });
    $('.boxsearch a.timkiem').click(function () {
        $('.boxsearch').toggleClass('active');
    });
    if ($(window).width() < 767) {
        $(".section .listCate .listmenucon").prepend("<a class='submenuds' href='javascript://'>sub</a>");

    } else {

    }
    $('.submenuds').click(function () {
        $('.section .listCate .listmenucon .tabpick').toggleClass("active");
    });
    $('.nutclickmenucon').click(function () {
        $(this).parent().toggleClass("acset");
    });
});