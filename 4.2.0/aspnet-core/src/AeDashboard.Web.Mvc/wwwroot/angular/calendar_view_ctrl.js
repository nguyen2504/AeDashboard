(function () {
    'use strict';
 app.controller('ctrl', calendarViewCtrl);

    calendarViewCtrl.$inject = ['$location', '$http', '$scope','$window'];

    function calendarViewCtrl($location, $http, $scope, $window) {
        /* jshint validthis:true */
        var vm = this;
        $scope.getAll = [];
        vm.title = 'calendar_view_ctrl';

        activate();

        function activate() {
            localStorage.removeItem('count');
            moment().locale("vi");
            getAll(0,22);
            loadScroll();
        }
        //============================
        $scope.hoverIn = function () {
            this.hoverEdit = 'show';
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
        $scope.edit_caladarView = function(id) {
            var url = "/Calendar/EditCalendarViewModal?id=" + id;
            $http.get(url).then(function(e) {
                //alert(e.data);
                $('#editCalendarView div.modal-body').html(e.data);
            });
        };
        $scope.delete_item = function (id) {
           var url = "/Calendar/Delete?id=" + id;
            $http.get(url).then(function (e) {
                reloadHome();
            });
        };

        function reloadHome() {
            $window.location.reload();
        }
//-------------------------------------------//
        function loadScroll() {
          
            $(window).scroll(function () {
                if ($(window).scrollTop() ==( $(document).height() - $(window).height())) {
                    var take = 10;
                    var skip = 0;
                    var count = localStorage.getItem("count");
                    console.log('count ' + localStorage.getItem("count"));
                    try {
                        skip = localStorage.getItem("count");
                    } catch (e) {
                        skip = 0;
                    } 
                    getAll(skip, take);
                }
            });
        }
        //function edit_calendarView(parameters) {
        //    var l = $('.edit-calendarview').
        //}
        function getAll(skip, take) {
           
            var url = "/Calendar/GetLoads";
            var data = {
                'Skip': skip,
                'Take': take
            };

            $http.get(url, { params: data}).then(function (e) {
                console.log(JSON.stringify(e.data.result.length));
                if (take == 0) {
                    $scope.getAll = e.data.result;
                    localStorage.setItem("count", $scope.getAll.length);
                } else {
                    for (var j = 0; j < e.data.result.length; j++) {
                        $scope.getAll.push(e.data.result[j]);
                    }
                    localStorage.setItem("count", $scope.getAll.length);
                    
                }
                if ($scope.getAll.length > 0) {
                    for (var i = 0; i < $scope.getAll.length; i++) {
                        var day = $scope.getAll[i].day;
                        console.log('day ' + day);
                        $scope.getAll[i].day = ' ' + moment().locale("vi").subtract(day, 'days').calendar().split('lúc')[0].split('at')[0];
                      
                    }
                   
                } else {
                    localStorage.setItem('count',0);
                    $('#myModal').modal();
                }
              
                //console.log('now ' + moment().locale("vi").subtract(-1, 'days').calendar());
            });

        }
    }
})();
