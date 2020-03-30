//menu header

$(function(){
    $('.mobile-icon').on('click', function(){
        $(this).toggleClass("mobile-close");
        $(".nav-menu").toggleClass("active");
        $("body").toggleClass("fix");
    });

    $(".nav-menu ul li").each(function () {
        $(this).find('ul').parent().addClass('has-sub');
    });

    $(".nav-menu ul").each(function () {
        if (!($(this).find("span.submenu-button").length)) {
            $(this).find(".has-sub").prepend('<span class="button"><i class="fa fa-chevron-down" aria-hidden="true"></i></span>');
        }
    });
    $(".nav-menu  ul>li .button").click(function () {
        $(this).toggleClass('on');
        $(this).parent().find(">ul").slideToggle("");
    });
});

//resize slider load page
// var window_type;
// var $window = $(window);
// if ($window.width() <= 1024) {
//     window_type = 'sp';
// } else {
//     window_type = 'pc';
// }
// $(window).resize(function() {
//     if($window.width() <= 1024){
//         if( (window_type != 'sp') ){
//             location.reload();
//         }
//     }else{
//         if(window_type != 'pc'){
//             location.reload();
//         }
//     }
// });




// $(window).on("load resize",function () {
//     $("main").css("padding-top",$("#header").outerHeight());
// });



//siider-home
$('.sec-mv--home ul').slick({
    dots: true,
    focusOnSelect: true,
    pauseOnHover:false,
    infinite: true,
    speed: 800,
    fade: true,
    autoplay: true,
    cssEase: 'linear',
    arrow:true
});
$('.sec-mv--child ul').slick({
    dots: false,
    focusOnSelect: true,
    pauseOnHover:false,
    infinite: true,
    speed: 800,
    fade: true,
    autoplay: true,
    cssEase: 'linear',
    arrow:true
});
$('.list-news').slick({
    focusOnSelect: true,
    pauseOnHover:true,
    infinite: true,
    speed: 800,
    slidesToShow:3,
    autoplay: false,
    cssEase: 'linear',
    arrow:true,
    responsive: [
        {
            breakpoint: 640,
            settings: {
                slidesToShow: 2,
            }
        },
    ]
});

$( function() {
    $( "#datepicker" ).datepicker();
} );
// $(function () {
//     objectFitImages('img');
// });


//matchHeight
jQuery(function ($) {
    $('.list-news__ttl').matchHeight();
    $(window).on('load, resize', function(){
        $('.list-news__ttl').matchHeight();
    });
});



//fade
$(window).on('scroll load assessFeatureHeaders', function(){
    var scrollTop = $(window).scrollTop();
    var appearenceBuffer = 60;
    var windowBottom = scrollTop + $(window).height() - appearenceBuffer;
    $('body').toggleClass('scrolled-down', scrollTop > 0);
    $('.fade-up:not(.active)').filter(function(){
        var offset = $(this).offset().top;
        var height = $(this).outerHeight();
        return offset + height >= scrollTop && offset <= windowBottom;
    }).addClass('active');
});


//backtop
// jQuery(document).ready(function ($) {
//     $(".backtop").hide();
//     $(window).on("scroll", function () {
//         if ($(this).scrollTop() > 100) {
//             $(".backtop").fadeIn("fast");
//         } else {
//             $(".backtop").fadeOut("fast");
//         }
//     });
//     $('.backtop').click(function () {
//         $('body,html').animate({
//             scrollTop: 0
//         }, 500);
//         return false;
//     });
// });

//TAB
$(document).ready(function(){

    $('.nav-tabs li').click(function(){
        var tab_id = $(this).attr('data-tab');

        $('ul.nav-tabs li').removeClass('on');
        $('.tab-item').removeClass('on');

        $(this).addClass('on');
        $("#"+tab_id).addClass('on');
    })
    $('.list-album').masonry({
        itemSelector: '.list-album__item',
        percentPosition: true
    });
})


