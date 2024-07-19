/// <reference path="jquery-3.4.1.min.js" />

$(function () {
    $(".menu-item.parent-menu").on('click', function () {
        $('.sub-menu-container .sub-menu-item').hide();
        $('.sub-menu-container .div-button-close').show();
        var id = $(this).attr('data-id');
        $('.sub-menu-container .sub-menu-item[data-parentid="' + id + '"]').show();
        $('.div-menu-title').text($(this).text());
    })
    $('.sub-menu-container .div-button-close .button-close').on('click', function () {
        $('.sub-menu-container .sub-menu-item').hide();
        $('.sub-menu-container .div-button-close').hide();
    });
});