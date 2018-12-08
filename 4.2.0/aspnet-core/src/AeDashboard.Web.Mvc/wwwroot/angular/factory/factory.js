(function () {
    'use strict';

    app.factory('factory', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getData: getData,
            formatDate: formatDate,
            setColspan: setColspan,
            getWeek: getWeek,
            loading: loading,
            getColor: getColor
        };

        return service;

        function getData() { }

        function formatDate(time, a) {
         
            var t = time.split('T');
         
if (a == 0) {
    return t[0];
} else {
    return hhmm(t[1]);
}
        }
    }

    function hhmm(a) {
         var t = a.split(':');
            return t[0] + ":" + t[1];
    }
    function setColspan() {
       if ($(window).width() > 768) {
           var rowWidth = $('.table-row:first').width();
           //console.log('width ' + rowWidth);
            var colWidth = $('.table-cell-or-item:first').width();
            var marginRight = colWidth - rowWidth;
            $('.table-cell-or-item.colspan').css('margin-right', marginRight + 'px').show();
        }

    };
    function getColor() {
        var color = $('.navbar').css('background-color');
        
        if ($(window).width < 576) {
            $('.ae-work-time').css('background-color', color);
        } else {
            $('.ae-work-time').css('color', color);
        }
        $('.table-cell-or-item.colspan').css('background-color', color);
        $('.box-search-ae-boottom > .itemae.active, .box-search-ae-boottom > .itemae:hover').css('color', color);
        //$('.material-icons').css('color', color);
        //$('.material-icons.menuas').css('color', 'white');
    }
    function getWeek(a) {
        var day = new Date(a);
        var onejan = new Date(day.getFullYear(), 0, 1);
        //console.log('da ' + onejan);
        return Math.ceil((((day - onejan) / 86400000) + onejan.getDay() + 1) / 7);
    }

    function loading(a) {
        if (a == true) {
            $('.page-item-loader-wrapper').css('display', 'block');
        } else {
            $('.page-item-loader-wrapper').css('display', 'none');
        }
    }
})();