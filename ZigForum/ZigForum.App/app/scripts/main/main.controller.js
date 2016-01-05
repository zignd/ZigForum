(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('MainController', MainController);

    MainController.$inject = ['users'];

    function MainController(users) {
        var vm = this;
    }
})();
