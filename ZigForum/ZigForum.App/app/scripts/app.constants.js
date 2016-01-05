(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .constant('TOKEN_URL', 'http://localhost:12345/token')
        .constant('API_URL', 'http://localhost:12345/api');
})();