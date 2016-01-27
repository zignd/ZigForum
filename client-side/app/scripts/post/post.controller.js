(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .controller('PostController', PostController);
    
    PostController.$inject = ['$routeParams', 'post'];
    
    function PostController($routeParams, post) {
        var vm = this;
        
        vm.post = {};
        
        post.getById(function (data) {
            vm.post = data;
        }, function (error) {
            alert(error);
        }, $routeParams.id);
    }
})();