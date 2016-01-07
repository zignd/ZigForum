(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('RootController', RootController);

    RootController.$inject = ['$scope', '$location', 'users'];

    function RootController($scope, $location, users) {
        var vm = this;

        $scope.$watch(function () {
            return users.userData.isAuthenticated;
        }, function (data) {
            vm.isAuthenticated = data;
        }, false);

        $scope.$watch(function () {
            return users.userData.username;
        }, function (data) {
            vm.username = data;
        }, false);

        vm.signOut = function () {
            users.removeAuthentication();
            $location.path('/');
            $location.replace();
        }
    }
})();