(function () {
    'use strict';

    app.directive('returnFiles', directive);

    directive.$inject = ['$window'];

    function directive($window) {
        // Usage:
        //     <directive></directive>
        // Creates:
        // 
        var directive = {
            require: "ngModel",
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, elem, attrs, ngModel) {
            elem.on("change",
                function(e) {
                    var files = elem[0].files;
                    ngModel.$setViewValue(files);
                    console.log(JSON.stringify(scope.fileList));
                });
        }
    }

})();