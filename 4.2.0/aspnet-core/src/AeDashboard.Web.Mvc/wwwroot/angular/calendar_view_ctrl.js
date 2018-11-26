(function () {
    'use strict';
 app.controller('ctrl', calendarViewCtrl);

    calendarViewCtrl.$inject = ['$location', '$http', '$scope','$window'];

    function calendarViewCtrl($location, $http, $scope, $window) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'calendar_view_ctrl';

        activate();

        function activate() {
            moment().locale("vi");
            getAll();
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
                $window.location.reload();
            });
        };

        function reloadHome() {
            $window.location.reload();
        }
//-------------------------------------------//

        //function edit_calendarView(parameters) {
        //    var l = $('.edit-calendarview').
        //}
        function getAll(parameters) {
            var url = "/Calendar/GetLoads";
            var loads = {
                skip:0,
                load :0
            }
            $http.get(url, loads).then(function(e) {
                $scope.getAll = e.data.result;
                if ($scope.getAll.length > 0) {
                    for (var i = 0; i < $scope.getAll.length; i++) {
                        var day = $scope.getAll[i].day;
                        $scope.getAll[i].day = ' ' + moment().locale("vi").subtract(day, 'hours').calendar().split('lúc')[0].split('at')[0];
                    }
                } else {
                    $('#myModal').modal();
                }
                //console.log('now ' + moment().locale("vi").subtract(-1, 'days').calendar());
            });

        }
    }
})();
