(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .directive('zgPost', zgPost);
    
    function zgPost() {
        return {
            restrict: 'E',
            templateUrl: 'scripts/components/zg-post.directive.html',
            scope: {
                post: '='
            }
        }
    }
})();