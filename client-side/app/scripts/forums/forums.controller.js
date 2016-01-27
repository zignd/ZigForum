(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('ForumsController', ForumsController);

    ForumsController.$inject = ['forums'];

    function ForumsController(forums) {
        var vm = this;
        
        vm.topLevelForums = [];
        
        forums.getAllTopLevel(function (data) {
            vm.topLevelForums = data;
        }, function (error) {
            alert(error);
        });
    }
})();