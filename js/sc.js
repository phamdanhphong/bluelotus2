//Face Book
$(document).ready(function ($) {
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.9";
        fjs.parentNode.insertBefore(js, fjs);
    }
	(document, 'script', 'facebook-jssdk'));
});

//Button Top
$(document).ready(function () {
    if ($(".btn_top").length > 0) {
        $(window).scroll(function () {
            var e = $(window).scrollTop();
            if (e > 300) {
                $(".btn_top").show()
            } else {
                $(".btn_top").hide()
            }
        });
        $(".btn_top").click(function () {
            $('html,body').animate({
                scrollTop: 0
            }, 700);
        })
    }
});

$(document).ready(function () {
    i = 1;
    $('.cut_text').each(function () {
        $(this).addClass('p' + i);
        i = i + 1;
    });
    for (j = 1; j <= i; j++) {
        $('.cut_text.p' + j).dotdotdot();
    }
});

$(document).ready(function () {
    $('.five_item .tabs li a').click(function () {
        var tab_id = $(this).attr('id');

        $('.five_item .tabs li a').removeClass('active');
        $('.five_item .group_item').removeClass('active');

        $(this).addClass('active');
        $('.five_item .group_item.' + tab_id).addClass('active');
    });
});

//Menu Responsive
$(document).ready(function () {
    $('.open_menu_res').click(function () {
        $(this).toggleClass('active');
        $(".menu_box li a.open_sub").removeClass("active");
        $(".menu_box li a.open_sub").parent("li").removeClass("active");
        if ($('.menu_box').hasClass('active')) {
            $('.menu_box').removeClass('active');
        }
        else {
            $('.menu_box').addClass('active');
        }
    });
});

$(document).ready(function () {
    $(".menu_box li a.open_sub").click(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
            $(this).parent("li").removeClass("active");
        }
        else {
            $(this).addClass("active");
            $(this).parent("li").addClass("active");
        }
    });
});

//Co chu
var size = parseInt(jQuery(".TextSize").css("font-size"));
var lineheight = parseInt(jQuery(".TextSize").css("line-height"));
if (!size)
    size = 14;
if (!lineheight)
    lineheight = 22;
function IncreaseTextSize() {
    size++;
    lineheight += 2;

    jQuery(".TextSize")
		.css('cssText',
			'font-size:' +
			size +
			'px !important; line-height:' +
			lineheight +
			'px !important');
    jQuery(".TextSize")
		.find("*")
		.css('cssText',
			'font-size:' +
			size +
			'px !important; line-height:' +
			lineheight +
			'px !important');
}

function DecreaseTextSize() {
    size--;
    lineheight -= 2;

    jQuery(".TextSize")
		.css('cssText',
			'font-size:' +
			size +
			'px !important; line-height:' +
			lineheight +
			'px !important');
    jQuery(".TextSize")
		.find("*")
		.css('cssText',
			'font-size:' +
			size +
			'px !important; line-height:' +
			lineheight +
			'px !important');
}

function ResetTextSize() {
    size = 14;
    lineheight = 22;

    jQuery(".TextSize")
		.css('cssText',
			'font-size:' +
			size +
			'px !important; line-height:' +
			lineheight +
			'px !important');
    jQuery(".TextSize")
		.find("*")
		.css('cssText',
			'font-size:' +
			size +
			'px !important; line-height:' +
			lineheight +
			'px !important');
}

$(document).ready(function () {
    $(".noidung.TextSize iframe[src*='youtube']").each(function () {
        var iframeCopy = $(this).clone();
        $(this).replaceWith($("<div class='youtube-iframe-wrap'></div>").append(iframeCopy));
    });
});

//Slick
$(document).ready(function () {
    $('.slick_1').slick({
        rows: 1,
        slidesPerRow: 1,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        infinite: true,
        dots: true,
        arrows: true,
        speed: 1000,
        autoplaySpeed: 5000,
    });
});



$(document).ready(function () {
    $('.slick_3').slick({
        dots: true,
        arrows: true,
        infinite: true,
        rows: 1,
        slidesPerRow: 3,
        slidesToShow: 3,
        slidesToScroll: 1,
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    rows: 1,
                    slidesPerRow: 3,
                    slidesToShow: 3,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    rows: 1,
                    slidesPerRow: 2,
                    slidesToShow: 2,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            }
        ]
    });
});

//Slick
$(document).ready(function () {
    $('.slick_4').slick({
        dots: true,
        arrows: true,
        infinite: true,
        rows: 1,
        slidesPerRow: 4,
        slidesToShow: 4,
        slidesToScroll: 1,
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    rows: 1,
                    slidesPerRow: 3,
                    slidesToShow: 3,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    rows: 1,
                    slidesPerRow: 3,
                    slidesToShow: 3,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('.slick_4a').slick({
        dots: true,
        arrows: true,
        infinite: true,
        rows: 1,
        slidesPerRow: 4,
        slidesToShow: 4,
        slidesToScroll: 1,
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    rows: 1,
                    slidesPerRow: 2,
                    slidesToShow: 2,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    rows: 1,
                    slidesPerRow: 2,
                    slidesToShow: 2,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('.slick_5').slick({
        dots: true,
        arrows: true,
        infinite: true,
        rows: 1,
        slidesPerRow: 5,
        slidesToShow: 5,
        slidesToScroll: 1,
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesPerRow: 3,
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesPerRow: 3,
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesPerRow: 2,
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('.slick_7').slick({
        dots: true,
        arrows: true,
        infinite: true,
        rows: 1,
        slidesPerRow: 7,
        slidesToShow: 7,
        slidesToScroll: 1,
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesPerRow: 3,
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesPerRow: 3,
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesPerRow: 2,
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('.slick_22').slick({
        dots: true,
        arrows: true,
        infinite: true,
        rows: 2,
        slidesPerRow: 2,
        slidesToShow: 1,
        slidesToScroll: 1,
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    rows: 2,
                    slidesPerRow: 2,
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    rows: 2,
                    slidesPerRow: 2,
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesPerRow: 2,
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesPerRow: 1,
                    slidesToShow: 1,
                }
            }
        ]
    });
});

//Slick with thumbs
$(document).ready(function () {
    var i = 1;
    $('.slider_image').each(function () {
        $(this).addClass('slide_' + i);
        i = i + 1;
    });
    for (j = 1; j <= i; j++) {
        $('.slide_' + j + ' .slider-for').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            fade: true,
            asNavFor: '.slide_' + j + ' .slider-nav',
            autoplay: true
        });
        $('.slide_' + j + ' .slider-nav').slick({
            slidesToShow: 5,
            slidesToScroll: 1,
            asNavFor: '.slide_' + j + ' .slider-for',
            dots: false,
            centerMode: false,
            focusOnSelect: true,
            arrows: true,
            responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                }
            }
        ]
        });
    }
});

//Khung anh
$(window).load(function () {
    $(".khungAnhCrop img").each(function () {
        $(this).removeClass("wide tall").addClass((this.width / this.height > $(this).parent().width() / $(this).parent().height()) ? "wide" : "tall");
    });
});

$(window).resize(function () {
    $(".khungAnhCrop img").each(function () {
        $(this).removeClass("wide tall").addClass((this.width / this.height > $(this).parent().width() / $(this).parent().height()) ? "wide" : "tall");
    });
});

//Popup
function popup() {
    $('#popup').css('opacity', '1');
    $('#popup').css('pointer-events', 'auto');
}

$(document).ready(function () {
    var popup = document.getElementById('popup');

    $('#close_popup').click(function () {
        $('#popup').css('opacity', '0');
        $('#popup').css('pointer-events', 'none');
    });

    window.onclick = function (e) {
        if (e.target == popup) {
            $('#popup').css('opacity', '0');
            $('#popup').css('pointer-events', 'none');
        }
    }

    document.body.onkeyup = function (e1) {
        if (e1.keyCode == 27) {
            $('#popup').css('opacity', '0');
            $('#popup').css('pointer-events', 'none');
        }
    }
})