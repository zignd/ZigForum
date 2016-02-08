(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .directive('zgForum', zgForum);

    zgForum.$inject = [];

    function zgForum() {
        return {
            restrict: 'E',
            templateUrl: 'scripts/components/zg-forum.directive.html',
            scope: {
                forum: '=',
                posts: '='
            }
        }
    }
})();