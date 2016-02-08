(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('ForumDetailController', ForumDetailController);

    ForumDetailController.$inject = ['$routeParams', 'forums', 'posts'];

    function ForumDetailController($routeParams, forums, posts) {
        var vm = this;

        vm.forum = {};
        vm.posts = [];

        forums.getById(getByIdOnSuccess, getByIdOnFail, $routeParams.id);
        posts.getByForumId(getByForumIdOnSuccess, getByForumIdOnFail, $routeParams.id);
        
        function getByIdOnSuccess(data) {
            vm.forum = data;
        }
        
        function getByIdOnFail(error) {
            alert(error);
        }
        
        function getByForumIdOnSuccess(data) {
            vm.posts = data;
        }
        
        function getByForumIdOnFail(error) {
            alert(error);
        }
    }
})();
