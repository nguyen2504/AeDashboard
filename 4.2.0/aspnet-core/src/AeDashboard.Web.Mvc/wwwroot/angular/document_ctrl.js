(function () {
    'use strict';

   app.controller('ctrl', documentCtrl);

    documentCtrl.$inject = ['$location', '$scope', '$http','factory'];

    function documentCtrl($location, $scope, $http, factory) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ctrl';

        activate();
        $scope.$watch('search',
            function() {
                $scope.data = [];
                getSearch(0, 32, $scope.search);
            });
        $scope.addOrUpdate = function (id) {
            $('#modalCreateDocument').modal();
            var url = "/Document/AddOrUpdate?id=" + id;
            $http.get(url).then(function(e) {
                var result = e.data;
            });
        };
        $scope.onUpload = function() {
            $('.upload-document').removeClass('hide').addClass('show');
            if ($('.upload-document').hasClass('show')) {
                //alert($('.upload-document').hasClass('show'));
            }
        };
        //---------------------------------//
        function activate() {
            $scope.data = [];
            
            loadScroll();
        }
        // ---------------- get All-------//
        //function getAll() {
        //    var url = "/Document/GetAll";
        //    $http.get(url).then(function(e) {
        //        $scope.data = e.data.result;
        //        console.log(JSON.stringify(e));
        //    });
        //}

        function getSearch(skip, take, name) {
            factory.loading(true);
            var url = "/Document/Search";
            var data = {
                'Skip': skip,
                'Take': take,
                "Search": name
            };
            $http.get(url, { params: data }).then(function (e) {
                var data = e.data.result;
                for (var i = 0; i < data.length; i++) {
                    $scope.data.push(data[i]);
                }
                factory.loading(false);
            });
        }
        function loadScroll() {

            $(window).scroll(function () {
                var h = (($(document).height() - $(window).height())) - $(window).scrollTop();
                //console.log('kk ' + h);
                var check = false;
                if (h >= 0 && h <= 1) check = true;
                if (h >= 400 && h <= 410) check = true;
                if (h >= 300 && h <= 310) check = true;
                if (h >= 50 && h <= 60) check = true;
                if (h >= 100 && h <= 110) check = true;
                if (h >= 150 && h <= 160) check = true;
                if (h >= 200 && h <= 210) check = true;
               
                if (check) {
                    var skip = $('.table-row.doc').length + 1;
                    var take = 5;
                    getSearch(skip, take, $scope.search);

                    //console.log("skip " + skip);
                }
            
            });
        }
    }
})();
