(function () {
    'use strict';
 app.controller('ctrl', calendarViewCtrl);

    calendarViewCtrl.$inject = ['$location','$http','$scope'];

    function calendarViewCtrl($location,$http,$scope) {
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
        $scope.edit_caladarView = function (id) {
            var url = "/Calendar/EditCalendarViewModal?id=" + id;
            $http.get(url).then(function(e) {
                //alert(e.data);
                $('#editCalendarView div.modal-body').html(e.data);
            });
        }
//-------------------------------------------//

        //function edit_calendarView(parameters) {
        //    var l = $('.edit-calendarview').
        //}
        function getAll(parameters) {
            var url = "/Calendar/GetAll";
            $http.get(url).then(function(e) {
                $scope.getAll = e.data.result;
                for (var i = 0; i < $scope.getAll.length; i++) {
                    var day = $scope.getAll[i].day;
                    $scope.getAll[i].day =' '+ moment().locale("vi").subtract(day, 'days').calendar().split('lúc')[0].split('at')[0];
                }
                console.log('now ' + moment().locale("vi").subtract(-1, 'days').calendar());
            });

        }
    }
})();
