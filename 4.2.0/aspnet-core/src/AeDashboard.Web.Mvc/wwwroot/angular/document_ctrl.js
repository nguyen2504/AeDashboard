(function () {
    'use strict';

   app.controller('ctrl', documentCtrl);

    documentCtrl.$inject = ['$location', '$scope', '$http', 'factory', '$timeout','$compile'];

    function documentCtrl($location, $scope, $http, factory, $timeout, $compile) {
        $('#fileSelected').on('change', function (evt) {
            var files = $(evt.currentTarget).get(0).files;
           
            if (files.length > 0) {

                $('#filePath').text($('#fileSelected').val());
            }
        });
        //$('.form-line').on('click',
        //    function() {
        //        $(this).removeClass('focused');
        //    });
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
            console.log('id ' + id);
            //$('#showinfor').html(id);
            $('input[name="bookId"]').val(id);
            $('#modalUpload').modal();
        };
        $scope.onChangeImportant = function ($index) {
            $scope.oldValue = $scope.data[$index].id;

     
            var url = "/Document/EditIportant?id=" + $scope.data[$index].id;
            $http.post(url).then(function(e) {
                //console.log(JSON.stringify(e.data));
            });
            if ($scope.data[$index].important) {
                $scope.data[$index].important = false;
                $scope.data[$index].colorstar = '';
            } else {
                $scope.data[$index].important = true;
                $scope.data[$index].colorstar = 'red';
            }

        };
        $scope.editUrl = function(id) {
            console.log('id ' + id);
        };
        $scope.delete = function(id) {
            var url = "/Document/Delete?id=" + id;
            $http.get(url).then(function(e) {
                $scope.data = jQuery.grep($scope.data, function (value) {
                    return value.id != id;
                });
            });
        };
        $scope.edit = function(id) {
            var url = "/Document/_Edit?id=" + id;
            //$scope.addOrUpdate(id);
            var l = $('input[name="bookId"]').val();
            //alert(l);
            $http.get(url).then(function (e) {
                var dt = e.data.result;
                var $el =   $('#editDocument div.modal-body').html(e.data);
                $compile($el)($scope);
                $scope.loadCatalogue();
                $('#editDocument').modal();
            });
        };
        $scope.onUpload = function() {
            $('.upload-document').removeClass('hide').addClass('show');
            if ($('.upload-document').hasClass('show')) {
                //alert($('.upload-document').hasClass('show'));
            }
        };
        $scope.editDocument = function(id) {
            $scope.addOrUpdate(id);
        };
        $scope.hideModal = function() {


            $timeout(function() {

                    $('#editDocument').modal('hide');
                },
                3000);


        };
        $scope.submitForm = function () {
            //console.log('data ' + JSON.stringify($scope.document));
            var payload = new FormData();
            var url = "/Document/Edit";
            $http.post(url, { params: JSON.stringify($scope.document) }).then(function(e) {

            });
        };

      
        $scope.loadCatalogue = function() {
            var url = "/Document/LoadCatalogue";
            $http.get(url).then(function (e) {
                //console.log(JSON.stringify(e.data));
                $scope.catalog = e.data.result;
            });
        };
        $scope.loadCatalogue();
        //---------------------------------//
        function activate() {
            $scope.data = [];
           
            loadScroll();
        }
     
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

            var i = 0;
            $(window).scroll(function () {
                var h = (($(document).height() - $(window).height())) - $(window).scrollTop();
                i++;
                var check = false;
                if (h >= 0 && h <= 9) check = true;
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
                    i = 0;
                }
            
            });
        }
    }
})();
function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    var url = "/Document/UploadFile";
   
    $.ajax(
        {
            url:url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (e) {
                var t = e.result;
                $("#Url").val(t.url);
                if (t.url != "") {
                    var file = t.url.split('_')[t.url.split('_').length - 1];
                    var id = $('input[name="bookId"]').val();
                    if (id > 0) {
                        $('input[name="bookId1"]').val(id);
                        localStorage.setItem('item', file);
                        $('#modalUpload').modal('hide');
                    } else {
                        $('#modalCreateDocument').modal();
                        $('#modalUpload').modal('hide');
                        $('.progress-bar').addClass('show');
                        $('.complete').text("upload 100% " + file);
                    }
                } else {
                    $('.showinfor').addClass('show');
                    $('#modalUpload').modal('show');
                }
                

            }
        }
    );
}