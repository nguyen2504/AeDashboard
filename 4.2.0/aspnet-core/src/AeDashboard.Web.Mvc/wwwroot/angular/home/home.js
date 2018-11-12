(function () {
    'use strict';

  app.controller('ctrl', controller);

    controller.$inject = ['$scope'];

    function controller($scope) {
        $scope.title = 'controller';

        activate();

        function activate() { }
    }
})();
