(function () {
    'use strict';
 app.controller('ctrl', calendarViewCtrl);

    calendarViewCtrl.$inject = ['$location', '$http', '$scope', '$window','$filter', 'factory'];

    function calendarViewCtrl($location, $http, $scope, $window, $filter, factory) {
        /* jshint validthis:true */
        var vm = this;
        $scope.getAll = [];
        vm.title = 'calendar_view_ctrl';
        $scope.count = 15;
        activate();

        function activate() {
            $scope.load = 0;
            localStorage.removeItem('count');
            moment().locale("vi");
            //getAll(0,32);
            loadScroll();
            var w = new Date().getFullYear() + "-W" + factory.getWeek(new Date());
            $scope.week = moment(w).toDate();
           
        }
      
        //============================
        $scope.$watch('week',
            function() {
                console.log('week ' + $scope.week + '  ' + $scope.load);
                if ($scope.load == 2) {

                    var week = $filter('date')($scope.week, "yyyy-ww");
                    getWeek(0, 0, week);
                    factory.getColor();
                }
                if ($scope.load != 2) {
                    $scope.load = 2;
                }
            });
        $scope.$watch('searchDate',
            function () {
             if ($scope.load == 1) {
                    var date = $scope.searchDate;
                 getDate(0, 0, date);
                 factory.getColor();
             }
                if ($scope.load !=1) {
                    $scope.load = 1;
                    //alert('toi day' + $scope.load);
                }
              
            });
        $scope.$watch('search',
            function (e) {
                $scope.count = 15;
                var name = $scope.search;
                $scope.getAll = [];
                getSearchName($scope.count, name);
                factory.getColor();
            });
        $scope.hoverIn = function () {
            this.hoverEdit = 'show';
        };
        $scope.formatDate = function(a) {
            //console.log('f ' + factory.formatDate(a, 1));
            return factory.formatDate(a, 1);
        };
        $scope.onChangeWeek = function() {

        };
        $scope.hoverOut = function () {
            this.hoverEdit = 'hide';
        };
        $scope.showEdit = function () {
            //console.log('showEdit');
            ////$scope.show = 'show';
            //angular.element(this).find('.ae-update').addClass('show');
            //$('.ae-update').removeClass('show');
            //$(this).find('.ae-update').addClass('show');
        };
        $scope.edit_caladarView = function (id) {
            
            var url = "/Calendar/EditCalendarViewModal?id=" + id;
            $http.get(url).then(function(e) {
                //alert(e.data);
                $('#editCalendarView div.modal-body').html(e.data);
                //getRole();
            });
        };
        $scope.delete_item = function (id) {

           var url = "/Calendar/Delete?id=" + id;
            $http.get(url).then(function (e) {
                reloadHome();
            });
        };
        $scope.showBtns = function ($event) {
         
            //console.log(angular.element($event.currentTarget).prop('offsetWidth'));
            //var top = angular.element($event.currentTarget).prop('offsetTop')+1 ;        
            //$('.ae-btns').addClass('active').css({ "top": top });
            //var top = angular.element($event.target).prop('offsetTop');
            //$('.ae-btns').css({ "top": top });
        };

        function getRole() {
            var url = "/Calendar/GetRole";
            $http.get(url).then(function (e) {
                //alert(e.data.result);
                if (e.data.result == 0) {
                    $(".delete_itemCalendar").hide();
                }
                $scope.showdelete = e.data.result;
            });
        }

//-------------------------------------------//
        function reloadHome() {
            $window.location.reload();
        }

       
        function loadScroll() {
           
            $(window).scroll(function () {
                var h = (($(document).height() - $(window).height())) - $(window).scrollTop();
                //console.log('kk ' + h);
                var check = false;
                if (h >= 0 && h <= 9) check = true;
                if (h >= 400 && h <= 410) check = true;
                if (h >= 300 && h <= 310) check = true;
                if (h >= 50 && h <= 60) check = true;
                if (h >= 100 && h <= 110) check = true;
                if (h >= 150 && h <= 160) check = true;
                if (h >= 200 && h <= 210) check = true;
                if (check) {
                    $scope.count = $scope.count + 1;
                   getSearchName($scope.count , $scope.search);
                    factory.getColor();
                   //console.log("$scope.count " + $scope.count);
               }
            });
        }

        function getWeek(skip, take,week) {
            var url = "/Calendar/SearchWeek";
            var data = {
                'Skip': skip,
                'Take': take,
                "Week": week
            };
            factory.loading(true);
            $http.get(url, { params: data }).then(function(e) {
                $scope.getAll = e.data.result;

                setTimeout(function () {
                    factory.setColspan();
                }, 100);
                factory.loading(false);
            });
        }

        function getSearchName(count,name) {
            var url = "/Calendar/SearchName";
            var data = {
                "Count":count,
                "Search": name
            };
            factory.loading(true);
            $http.get(url, { params: data }).then(function (e) {
                for (var j = 0; j < e.data.result.length; j++) {
                    $scope.getAll.push(e.data.result[j]);
                }
                factory.loading(false);
                setTimeout(function () {
                    factory.setColspan();
                    factory.getColor();
                }, 100);
            }); 
        }
        function getDate(skip, take, date) {
            factory.loading(true);
            var url = "/Calendar/SearchDate";
            var data = {
                'Skip': skip,
                'Take': take,
                "Date": date
            };
            $http.get(url, { params: data }).then(function (e) {
                $scope.getAll = e.data.result;
                setTimeout(function () {
                    factory.setColspan();
                }, 100);
                factory.loading(false);
            });
        }
        function getAll(skip, take) {
           
            var url = "/Calendar/GetLoads";
            var data = {
                'Skip': skip,
                'Take': take
            };

            $http.get(url, { params: data}).then(function (e) {
                //console.log(JSON.stringify(e.data.result.length));
                if (take == 0) {
                    $scope.getAll = e.data.result;
                    localStorage.setItem("count", $scope.getAll.length);
                } else {
                    for (var j = 0; j < e.data.result.length; j++) {
                        $scope.getAll.push(e.data.result[j]);
                    }
                    localStorage.setItem("count", $scope.getAll.length);
                    
                }
              
                setTimeout(function() {
                    factory.setColspan();
                    factory.getColor();
                }, 100);

            });

        }
    };
})();
