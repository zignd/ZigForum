(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .constant('TOKEN_URL', 'http://localhost:40912/token')
        .constant('API_URL', 'http://localhost:40912/api');
})();