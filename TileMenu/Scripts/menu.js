/// <reference path="jquery-3.4.1.min.js" />

$(function () {
    $(".menu-item.parent-menu").on('mouseover', function () {
        $('.sub-menu-container .sub-menu-item').hide();
        var id = $(this).attr('data-id');
        $('.sub-menu-container .sub-menu-item[data-parentid="' + id + '"]').show();

    })
    var mouseIsInsideContainer = false;
    var mouseIsInsideItem = false;

    $('.sub-menu-container').on('mouseenter', function () {
        mouseIsInsideContainer = true;
    });

    $('.sub-menu-container').on('mouseleave', function () {
        mouseIsInsideContainer = false;
        hideElementsIfNeeded();
    });

    $('.sub-menu-item').on('mouseenter', function () {
        mouseIsInsideItem = true;
    });

    $('.sub-menu-item').on('mouseleave', function () {
        mouseIsInsideItem = false;
        hideElementsIfNeeded();
    });

    $(".menu-item.parent-menu").on('dblclick', function () {
        $('.sub-menu-container .sub-menu-item').hide();
        var id = $(this).attr('data-id');
        $('.sub-menu-container .sub-menu-item[data-parentid="' + id + '"]').show();
    })
    function hideElementsIfNeeded() {
        if (!mouseIsInsideContainer && !mouseIsInsideItem) {
            $('.sub-menu-container .sub-menu-item').hide();
        }
    }
});
