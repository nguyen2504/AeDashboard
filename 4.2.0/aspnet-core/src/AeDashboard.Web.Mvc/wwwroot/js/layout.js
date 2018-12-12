$(document).ready(function () {
    $(function() {
        $('[data-toggle="tooltip"]').tooltip();
    });
    var color = $('.navbar').css('background-color');
    if (sessionStorage.index!=null) {
           
        $('.list>li').eq(sessionStorage.index).find('a').css('color', color);
        $('.list>li').eq(sessionStorage.index).find('span').css('color', color);

    } else {
        $('.list>li').eq(1).find('a').css('color', color);
        $('.list>li').eq(1).find('span').css('color', color);
    }
    $('.demo-choose-skin>li').on('click',
        function() {
            getColor();
        });
      
    function getColor() {
        var color = $('.navbar').css('background-color');
        $('.btn-add-circle').css('background-color', color);
        $('.itemae').css('color', '');
        $('.itemae.active').css('color', color);
        //   $('.material-icons').css('color', color);
        //  $('.material-icons.menuas').css('color', 'white');
        if ($(window).width < 576) {
            $('.ae-work-time').css('background-color', color);
        } else {
            $('.ae-work-time').css('color', color);
        }
        $('.table-cell-or-item.colspan').css('background-color', color);
        $('.itemae.active').css('color', color);
        if ($(window).width() < 575) {
            $('.box-flex_item').css('background-color', color);
        }
    }

    $('.list>li').click(function() {
          
        sessionStorage.index = $(this).index();
         
    });
    //$(this).click(function() {

    //    $('.dropdown-menu').removeClass('show');
    //    $('.dropdown').removeClass('show');
    //});
    $('.ae-tb-body').click(function() {
        console.log('ae-tb-body');
    });
    $('.page-item-loader-wrapper').css('display', 'none');

    $('.box-search-ae-boottom>.itemae').on('click',
        function() {
            $('.box-search-ae-boottom>.itemae').removeClass('active');
            $(this).addClass('active');
            $('.itemae').css('color', '');
             
            var index = $(this).index() ;
            if ($(this).index() > 0) {
                   
                $('.box-flex_item').removeClass('show');
                $('.box-search-ae').removeClass('show');
                $('.box-flex_item').eq(index).addClass('show');
                $('.box-search-ae').addClass('show');
                var color = $('.navbar').css('background-color');
                $('.itemae.active').css('color', color);
                  
            } else {
                $('.box-search-ae').removeClass('show');
                $('#modalCreate').modal('show');
            }
               
              
        });
    window.onload = getColor;
    setTimeout(function () { getColor(); }, 300);
    $('.close').click(function() {
        $('.box-search-ae').removeClass('show');
        //alert('ok')
    });
    function setColspan() {
        if ($(window).width() > 768) {
            var rowWidth = $('.table-row:first').width();
            var colWidth = $('.table-cell-or-item:first').width();
            //console.log('rowWidth ' + rowWidth + ' colWidth ' + colWidth)
            var marginRight = colWidth - rowWidth;
            $('.table-cell-or-item.colspan').css('margin-right', marginRight + 'px').show();
        }

    };

    setColspan();
    $(window).resize(function() {
        setColspan();
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() == ($(document).height() - $(window).height())) {
            setColspan();
            getColor(); 
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

    $(window).on('load', function() {
        setColspan();
        getColor();
    });
    window.onload = function() {
        setColspan();
    };
    
});
$('.close').click(function() {
    $('.box-search-ae').removeClass('show');
});
