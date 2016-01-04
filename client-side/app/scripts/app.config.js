(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .config(function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: 'scripts/main/main.html',
                    controller: 'MainController',
                    controllerAs: 'vm'
                })
                .when('/signin', {
                    templateUrl: 'scripts/account/signin.html',
                    controller: 'SignInController',
                    controllerAs: 'vm'
                })
                .when('/signup', {
                    templateUrl: 'scripts/account/signup.html',
                    controller: 'SignUpController',
                    controllerAs: 'vm'
                })
                .otherwise({
                    redirectTo: '/'
                });
        });

    authentication.$inject = ['$q', '$location', 'users'];

    function authentication($q, $location, users) {
        if (!users.isAuthenticated()) {
            var returnUrl = $location.path();
            $location.path('/signin');
            $location.search('returnUrl', returnUrl);
            $location.replace();
        }
    }
})();