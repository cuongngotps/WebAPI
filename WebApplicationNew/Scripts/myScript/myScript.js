(function () {
	var app = angular.module('myApp', []);

	app.controller('myCtrl', ['$http', '$scope', 'addNewUser', function ($http, $scope, addNewUser) {

		$http.get('api/me/users/all')
			.then(function (response) {
				$scope.users = response.data;
			}, function (response) {

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