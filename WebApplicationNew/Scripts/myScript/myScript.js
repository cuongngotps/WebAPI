(function () {
	var app = angular.module('myApp', []);

	app.controller('myCtrl', ['$http', '$scope', '$window', '$location', 'addNewUser', function ($http, $scope, $window, $location, addNewUser) {

		$http.get('api/me/users/all')
			.then(function (response) {
				$scope.users = response.data;
			}, function (response) {
				$window.location.href = '/src/login.html';
			});

		$scope.deleteUser = function (id) {

			$http.get('api/me/users/delete/' + id)
				.then(function (response) {

				}, function (response) {

				});
		};

		$scope.addUser = function () {
			addNewUser.add($scope);
		}
	}]);

	app.controller('ctlLogin', ['$http', '$scope', function ($http, $scope) {
		$scope.login = function () {

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