(function () {
	var app = angular.module('myApp', ['ngRoute']);

	app.config(function ($routeProvider) {
		$routeProvider
			.when('/login', {
				templateUrl: "/src/login.html",
				controller: 'ctlLogin'
				//redirectTo: '/src/login.html'
			}).when('/', {
				templateUrl: "/",
				controller: 'myCtrl'
			})
	});

	app.controller('myCtrl', ['$http', '$scope', '$window', '$location', 'addNewUser', function ($http, $scope, $window, $location, addNewUser) {
		
		if (typeof (window.token) == 'undefined') {
			$location.path('login');
			app.vm = this;
		} else {
			$http({
				method: 'GET',
				url: 'api/me/users/all',
				headers: {
					"Authorization": "bearer " + window.token
				}
			}).then(function (response) {
				app.vm.userdata = {};
				app.vm.userdata.users = response.data;
				console.log(app.vm.userdata.users);

			}, function (response) {
				$location.path('login');
			});
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

		$scope.addUser = function () {
			addNewUser.add($scope);
		}

	}]);

	app.controller('ctlLogin', ['$http', '$scope', '$location', function ($http, $scope, $location) {
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
            }), function (response) {

            };
		}
	}]);

	app.factory('addNewUser', ['$http', function ($http) {

		return {
			add:add
		}

		function add($scope) {
			$http.post('api/me/users/add', $scope.email)
				.then(function (response) {

				}, function (response) {

				});
			$scope.email = "";
		}
	}]);

	app.directive('itemUser', function () {
		return {
			templateUrl: '/src/item-user.html'
		}
	});

})();