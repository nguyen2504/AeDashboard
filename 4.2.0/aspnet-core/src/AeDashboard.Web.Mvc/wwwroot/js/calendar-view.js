$(document).ready(function() {
    function setColspan() {
        if ($(window).width() > 768) {
            var rowWidth = $('.table-row:first').width();
            var colWidth = $('.table-cell-or-item:first').width();
            //console.log('rowWidth ' + rowWidth + ' colWidth ' + colWidth)
            var marginRight = colWidth - rowWidth;
            $('.table-cell-or-item.colspan').css('margin-right', marginRight + 'px').show()
        }

    };

    setColspan();
    $(window).resize(function() {
        setColspan();
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() == ($(document).height() - $(window).height())) {
            setColspan();
        }
    });
    $('.table-cell-or-item').on('click',
        function(e) {
            e.preventDefault;
            var x = $(this).position();
            //console.log("Top: " + x.top + " Left: " + x.left);
            if ($('.ae-list-btn').hasClass('active') == false) {

            }
            var bt = $(this).height() + x.top - $('.ae-list-btn').height() + 10;
            if ($(window).width() < 768) {
                bt = x.top;
            }
            console.log('top' + bt);
            $('.ae-list-btn').removeClass('active').delay(300);
            $('.ae-list-btn').addClass('active');
            $('.ae-list-btn').css({ "top": bt });
            $('.as-btns').css({ "top": bt });

        });
    //$('.table-body').on('click',
    //    function() {
    //        var x = $(this).position();
    //        console.log("Top: " + x.top + " Left: " + x.left);
    //    });
    //$('body').click(function() {
    //    var x = $(this).position();
    //    console.log("Top position: " + x.top + " Left position: " + x.left);
    //       if($('.ae-list-btn').hasClass('active')){
    //          $('.ae-list-btn').removeClass('active'); 
    //     }
    //});
    $(window).on('load', function() {
        setColspan();
    });
    window.onload = function() {
        setColspan();
    }
  
});
