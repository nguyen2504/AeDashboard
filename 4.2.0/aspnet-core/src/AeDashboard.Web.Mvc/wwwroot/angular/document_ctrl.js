(function () {
    'use strict';

   app.controller('ctrl', documentCtrl);

    documentCtrl.$inject = ['$location','$scope','$http'];

    function documentCtrl($location, $scope,$http) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ctrl';

        activate();

        function activate() {
            getAll();
        }
        // ---------------- get All
        function getAll() {
            var url = "/Document/GetAll";
            $http.get(url).then(function(e) {
                $scope.alls = e.data;
            });
        }
    }
})();
