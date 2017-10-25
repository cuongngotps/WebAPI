(function () {
	var app = angular.module('myApp', ['ngRoute']);

	app.config(function ($routeProvider) {
		$routeProvider
			.when('/login', {
				templateUrl: "/src/login.html",
				controller: 'ctlLogin'
			})
			//.when('/', {
			//	templateUrl: "/src/item-user.html",
			//	controller: 'myCtrl'
			//})
			;
	});

    app.controller('myCtrl', ['$http', '$scope', '$window', '$location', 'addNewUser', 'userService',
        function ($http, $scope, $window, $location, addNewUser, userService) {
		
		if (typeof (window.token) == 'undefined') {
			app.vm = this;
			app.vm.hideAddUser = true;
			app.vm.hideUsers = true;
			$location.path('login');
		} else {
			//$http({
			//	method: 'GET',
			//	url: 'api/me/users/all',
			//	headers: {
			//		"Authorization": "bearer " + window.token
			//	}
			//}).then(function (response) {
			//	app.vm.userdata = {};
			//	app.vm.userdata.users = response.data;

			//	app.vm.hideUsers = false;

			//}, function (response) {
			//	$location.path('login');
            //});
            userService.getUsers();
		}

		

		app.vm.deleteUser = function (id) {

			$http({
				method: 'GET',
				url: 'api/me/users/delete/' + id,
				headers: {
					"Authorization": "bearer " + window.token
				}
			}).then(function (response) {
				$window.alert(response.data);
			}, function (response) {
				window.alert(response.data.message);
			});
		};

		app.vm.addUser = function () {
			addNewUser.add(app.vm);
		}

		app.vm.showAddUser = function () {
			app.vm.hideAddUser = false;
		}

	}]);

    app.controller('ctlLogin', ['$http', '$scope', '$location', 'userService', 
        function ($http, $scope, $location, userService) {
        $scope.login = function () {
			var data = "grant_type=password&username=" + $scope.username + "&password=" + $scope.password;
			$http({
				method: 'POST',
				url: '/token',
				data: data,
				headers: {
					"Content-Type": "application/x-www-form-urlencoded"
				}
			}).then(function (response) {
				window.token = response.data.access_token;
				$location.path('/');
                //app.vm.hideUsers = true;

                userService.getUsers();
                app.vm.hideAddUser = true;

            }), function (response) {

            };
		}
	}]);

    app.service('userService', ['$http', function ($http) {

        this.getUsers = function () {
            $http({
                method: 'GET',
                url: 'api/me/users/all',
                headers: {
                    "Authorization": "bearer " + window.token
                }
            }).then(function (response) {
                app.vm.userdata = {};
                app.vm.userdata.users = response.data;

                app.vm.hideUsers = false;

            }, function (response) {
                $location.path('login');
            });
        }

        this.addNewUser = function () {
            $http({
                method: 'POST',
                url: 'api/me/users/add',
                headers: {
                    "Authorization": "bearer " + window.token
                },
                data: {
                    userName: $scope.userName,
                    email: $scope.email,
                    phoneNumber: $scope.phoneNumber
                }
            }, $scope.username, {

            }).then(function (response) {
                console.log(response);
            }, function (response) {
                console.log(response);
            });
            app.vm.hideAddUser = true;
        }

        this.deleteUser = function () {
            $http({
                method: 'GET',
                url: 'api/me/users/delete/' + id,
                headers: {
                    "Authorization": "bearer " + window.token
                }
            }).then(function (response) {
                $window.alert(response.data);
            }, function (response) {
                window.alert(response.data.message);
            });
        }
    }]);

	app.factory('addNewUser', ['$http', function ($http) {

		return {
			add:add
		}

		function add($scope) {
			$http({
				method: 'POST',
				url: 'api/me/users/add',
				headers: {
					"Authorization": "bearer " + window.token
				},
				data: {
					userName: $scope.userName,
					email: $scope.email,
					phoneNumber: $scope.phoneNumber
				}
			}).then(function (response) {
				console.log(response);
				$scope.userdata.users.push(response.data);
			}, function (response) {
				console.log(response);
			});
			app.vm.hideAddUser = true;
		}
	}]);

	app.directive('itemUser', function () {
		return {
			templateUrl: '/src/item-user.html'
		}
	});

	app.directive('drAddUser', function () {
		return {
			templateUrl: '/src/add-user.html'
		}
	});

    app.directive('verifyExist', ['$http', function ($http) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                elem.on('blur', function (evt) {
                    scope.$apply(function () {
                        $http({
                            method: 'POST',
                            url: 'api/me/users/checkexist',
                            data: {
                                username: elem.val()
                            },
                            headers: {
                                "Authorization": "bearer " + window.token
                            }
                        }).then(function (data, status, headers, config) {
                            ctrl.$setValidity('unique', !data.data);
                        });
                    });
                });
            }
        }
    }]);

})();