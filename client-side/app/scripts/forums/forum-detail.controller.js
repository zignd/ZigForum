(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('ForumDetailController', ForumDetailController);

    ForumDetailController.$inject = ['$location', '$routeParams', 'forums'];

    function ForumDetailController($location, $routeParams, forums) {
        var vm = this;

        vm.forum = {};

        forums.getById(function (data) {
            vm.forum = data;
        }, function (error) {
            alert(error);
        }, $routeParams.id);
    }
})();
