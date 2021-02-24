$(function () {
    $('.wrapper1').on('scroll', function (e) {
        $('.wrapper2').scrollLeft($('.wrapper1').scrollLeft());
    });
    $('.wrapper2').on('scroll', function (e) {
        $('.wrapper1').scrollLeft($('.wrapper2').scrollLeft());
    });
});

$(window).on('load', function (e) {
    $('.div1').width($('table').width());
    $('.div2').width($('table').width());
});