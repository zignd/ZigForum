(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('ForumDetailController', ForumDetailController);

    ForumDetailController.$inject = ['$routeParams', 'forums'];

    function ForumDetailController($routeParams, forums) {
        var vm = this;

        vm.forum = {};

        forums.getById(function (data) {
            vm.forum = data;
        }, function (error) {
            alert(error);
        }, $routeParams.id);
    }
})();
