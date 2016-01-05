(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('RootController', RootController);

    RootController.$inject = ['$scope', '$location', 'user'];

    function RootController($scope, $location, user) {
        var vm = this;

        $scope.$watch(function () {
            return user.userData.isAuthenticated;
        }, function (data) {
            vm.isAuthenticated = data;
        }, false);

        $scope.$watch(function () {
            return user.userData.username;
        }, function (data) {
            vm.username = data;
        }, false);

        vm.signOut = function () {
            user.removeAuthentication();
            $location.path('/');
            $location.replace();
        }
    }
})();