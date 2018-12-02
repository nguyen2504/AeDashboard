(function ($) {
    $('.ae-tb-body').hover(function () {
        console.log('toiday');
        $('.ae-update').removeClass('show');
        $(this).find('.ae-update').addClass('show');
    });
})(jQuery);
