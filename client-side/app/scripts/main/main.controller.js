(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .controller('MainController', MainController);

    MainController.$inject = ['user'];

    function MainController(user) {
        var vm = this;
    }
})();
