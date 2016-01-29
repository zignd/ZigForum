(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .controller('PostDetailController', PostDetailController);
    
    PostDetailController.$inject = ['$routeParams', 'posts'];
    
    function PostDetailController($routeParams, posts) {
        var vm = this;
        
        vm.post = {};
        
        posts.getById(function (data) {
            vm.post = data;
        }, function (error) {
            alert(error);
        }, $routeParams.id);
    }
})();