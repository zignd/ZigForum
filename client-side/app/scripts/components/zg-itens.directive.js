(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .directive('zgItens', zgItens);
        
    function zgItens() {
        return {
            restrict: 'E',
            templateUrl: 'scripts/components/zg-itens.directive.html',
            scope: {
                itens: '=',
                ischildren: '='
            }
        }
    }
})();