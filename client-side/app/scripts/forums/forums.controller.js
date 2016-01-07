(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('ForumsController', ForumsController);

    ForumsController.$inject = ['$location', 'forums'];

    function ForumsController($location, forums) {
        var vm = this;
        
        vm.topLevelForums = [];
        
        forums.getAllTopLevel(function (data) {
            vm.topLevelForums = data;
        }, function (error) {
            alert(error);
        });
    }
})();