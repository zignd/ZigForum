(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .directive('zgItem', zgItem);

    zgItem.$inject = ['$compile'];

    function zgItem($compile) {
        return {
            restrict: 'E',
            templateUrl: 'scripts/components/zg-item.directive.html',
            scope: {
                item: '='
            },
            link: function (scope, element, attrs) {
                var collectionSt = '<zg-itens itens="item.children" ischildren="true"></zg-itens>';
                if (angular.isArray(scope.item.children)) {
                    $compile(collectionSt)(scope, function (cloned, scope) {
                        element.append(cloned);
                    });
                }
            }
        }
    }
})();