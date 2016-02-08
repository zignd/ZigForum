(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .controller('PostDetailController', PostDetailController);
    
    PostDetailController.$inject = ['$routeParams', 'posts', 'comments'];
    
    function PostDetailController($routeParams, posts, comments) {
        var vm = this;
        
        vm.post = {};
        vm.comments = [];
        
        posts.getById(getByIdOnSuccess, getByIdOnFail, $routeParams.id);
        comments.getByPostId(getByPostIdOnSuccess, getByPostIdOnFail, $routeParams.id);

        function getByIdOnSuccess(data) {
            vm.post = data;
        }
        
        function getByIdOnFail(error) {
            alert(error);
        }
        
        function getByPostIdOnSuccess(data) {
            vm.comments = data;
        }
        
        function getByPostIdOnFail(error) {
            alert(error);
        }
    }
})();