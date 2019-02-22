(function () {
    'use strict';
 app.controller('ctrl', calendarViewCtrl);

    calendarViewCtrl.$inject = ['$location', '$http', '$scope', '$window', '$filter', 'factory', '$timeout', '$sce','$compile'];

    function calendarViewCtrl($location, $http, $scope, $window, $filter, factory, $timeout, $sce, $compile) {
        /* jshint validthis:true */
        var vm = this;
        $scope.getAll = [];
        $scope.users = [];
        vm.title = 'calendar_view_ctrl';
        $scope.count = 15;
        var connection = new signalR.HubConnectionBuilder().withUrl("/mesHub").build();

        activate();

        function activate() {

	        initSignal();
            $scope.load = 0;
            localStorage.removeItem('count');
       
            //getAll(0,32);
            loadScroll();
            var w = new Date().getFullYear() + "-W" + factory.getWeek(new Date());
            $scope.week = moment(w).toDate();
            getIsAdmin();
            getUsers('1');
            //console.log('data '+$scope.users);
          
        }

        function initSignal(parameters) {

	        

	        //Disable send button until connection is established
	        $('[value=Create]').disabled = true;
	        $('[value=Save]').disabled = true;

	        connection.on("ReceiveMessage", function (a, b, c) {
		      
		        console.log('ok '+a+"  "+b+" "+c);
	        });

	        connection.start().then(function () {
		        $('[value=Create]').disabled = false;
		        $('[value=Save]').disabled = false;
	        }).catch(function (err) {
		        return console.error(err.toString());
	        });

	        //document.getElementById("sendButton").addEventListener("click", function (event) {
	        //	var id = document.getElementById("userInput").value;
	        //    var table = document.getElementById("messageInput").value;
	        //    var action = document.getElementById("messageInpu1").value;
	        //	connection.invoke("SendMessage", id, table,action).catch(function (err) {
	        //		return console.error(err.toString());
	        //	});
	        //	event.preventDefault();
	        //});
            $('[value=Create]').on('click', function (event) {
		        //var id = $('#id').val('add');
		        //var table = $('#table').val('ok');
		        //var action = $('#action').val('op');
                connection.invoke("SendMessage",'1','table','create').catch(function (err) {

			        return console.error(err.toString());
		        });
		     
            });
         
        }
        $scope.edit_cc = function(id) {
	        connection.invoke("SendMessage", '1', 'table', 'edit '+id).catch(function (err) {

		        return console.error(err.toString());
	        });
        }
        //============================
        function getIsAdmin() {
            var url = "/Document/GetIsAdmin";
            $http.get(url).then(function (e) {
                $scope.getIsAdmin = e.data.result;
            });
        }
        var dem = 0;
        $scope.$watch('week',
            function () {
                dem++;
                if (dem >= 2) {
                    var week = $filter('date')($scope.week, "yyyy-ww");
                
                    getWeek(0, 0, week);
                    $timeout(function () {
                        //alert('ok');
                        factory.getColor();
                    }, 1000);
                  
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
        $scope.createCalendar = function () {
	  

	        var t = "";
	        $('.name-admin-create').magicsearch({
		        dataSource: $scope.users,
		        fields: ['name', 'emailAddress'],
		        id: 'id',
		        format: '%name% - %emailAddress%',
		        multiple: true,
		        multiField: 'name',
		        multiStyle: {
			        space: 5,
			        width: 80
		        },
		        success: function($input, data) {
			        var id = $input.attr('data-id');
                    $('#IdAdmins').val(id);
                    if (id.split(',').length == 1) {
	                    t += data.name;
                    } else {
	                    t +=','+ data.name ;
                    }
			      
			        $('#Admin').val(t);
			    },
		        afterDelete: function($input, data) {
			        var id = $input.attr('data-id');
                    $('#IdAdmins').val(id);
                    //alert(JSON.stringify(data));
                    t = t.replace(data.name + ',', '');
                    if (id.split(',').length == 1) {
	                    t = t.replace(data.name , '');
                    }
			        $('#Admin').val(t);
		        }
            });
            //--------------------------------------
            var user = "";
            $('.name-admin-user').magicsearch({
	            dataSource: $scope.users,
	            fields: ['name', 'emailAddress'],
	            id: 'id',
	            format: '%name% - %emailAddress%',
	            multiple: true,
	            multiField: 'name',
	            multiStyle: {
		            space: 5,
		            width: 80
	            },
	            success: function ($input, data) {
                    var id = $input.attr('data-id');
                 
                    $('#IdUsers').val(id);
		            if (id.split(',').length == 1) {
			            user += data.name;
		            } else {
			            user += ',' + data.name;
		            }

                    $('#Users').val(user);
	            },
	            afterDelete: function ($input, data) {
		            var id = $input.attr('data-id');
                    $('#IdUsers').val(id);
		            //alert(JSON.stringify(data));
                    user = user.replace(data.name + ',', '');
                    if (id.split(',').length == 1) {
	                    user = user.replace(data.name , '');
                    }
                    $('#Users').val(user);
	            }
            });
            //--------

        };
        $scope.edit_caladarView = function (id) {
            var url = "/Calendar/EditCalendarViewModal?id=" + id;
            $http.get(url).then(function(e) {
            
                var $el = $('#editCalendarView div.modal-body').html(e.data);
                $compile($el)($scope);
              //console.log(e.data);  
                $("textarea").froalaEditor({
	                heightMin: 250

                });
                var t1 = "";
                var dataSource = [];
                dataSource = $scope.users;
                $('.name-admin1').magicsearch({
	                dataSource: dataSource,
                    fields: ['name', 'emailAddress'],
	                id: 'id',
                    format: '%name% - %emailAddress%',
	                multiple: true,
                    multiField: 'name',
	                multiStyle: {
		                space: 5,
		                width: 80
                    },
                    success: function ($input, data) {
	                  var id = $input.attr('data-id');
                        $('.id-admin1').val(id);
		                if (id.split(',').length == 1) {
			                t1 += data.name;
		                } else {
			                t1 += ',' + data.name;
		                }
                        $('.admin1').val(t1);
	                },
	                afterDelete: function ($input, data) {
		                var id = $input.attr('data-id');
                        $('.id-admin1').val(id);
		                //alert(JSON.stringify(data));
		                t1 = t1.replace(data.name + ',', '');
                        $('.admin1').val(t1);
	                }
                });

                var t2 = "";
                $('.name-admin2').magicsearch({
	                dataSource: dataSource,
	                fields: ['name', 'emailAddress'],
	                id: 'id',
	                format: '%name% - %emailAddress%',
	                multiple: true,
	                multiField: 'name',
	                multiStyle: {
		                space: 5,
		                width: 80
	                },
	                success: function ($input, data) {
		                var id = $input.attr('data-id');
                       
		                if (id.split(',').length == 1) {
			                t2 += data.name;
		                } else {
			                t2 += ',' + data.name;
		                }
                        $('.edit-user1').val(t2);
                        $('.id-edit-user1').val(id);
	                },
	                afterDelete: function ($input, data) {
		                var id = $input.attr('data-id');
						t2 = t2.replace(data.name + ',', '');
		                $('.edit-user1').val(t2);
		                $('.id-edit-user1').val(id);
	                }
                });
                //var t = html.html(e.data).find('#Users').val();
                //console.log(t);
              
                //$('.name-admin').eq(1).trigger('set', { id: '3,4,5,7,8' });
                //$('#set-btn').click(function () {
                // $('#basic').trigger('set', { id: '3,4' });
                //});
                showAdminUser(id);
            });
        };

        function showAdminUser(id) {
            var url = "/Calendar/EditCalendarViewModal1?id=" + id;
            $http.get(url).then(function(e) {
                $('.name-admin1').trigger('set', { id: e.data.result.idAdmins });
                $('.name-admin2').trigger('set', { id: e.data.result.idUsers });
                console.log(e.data.result.idUsers);
                console.log(e.data.result.idAdmins);
            });
        }
        $scope.uCanTrust = function(string) {
	        return $sce.trustAsHtml(string);
        };
        function   getUsers(ids) {
            var url = '/Calendar/GetUsers?ids=' + ids;
            $http.get(url).then(function(e) {
                $scope.users = e.data.result;
          
            });
        };
        $scope.delete_item = function (id,$index) {

           var url = "/Calendar/Delete?id=" + id;
            $http.get(url).then(function (e) {
                //reloadHome();getAll
                //var index = $scope.getAll.indexOf($index);
                //console.log('aa ' + index);
                $scope.getAll.splice($index, 1);
            });
        };
        $scope.showBtns = function ($event) {
         
            //console.log(angular.element($event.currentTarget).prop('offsetWidth'));
            //var top = angular.element($event.currentTarget).prop('offsetTop')+1 ;        
            //$('.ae-btns').addClass('active').css({ "top": top });
            //var top = angular.element($event.target).prop('offsetTop');
            //$('.ae-btns').css({ "top": top });
        };

        $scope.createcalendar = function() {
	        alert('ok');
        }

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
