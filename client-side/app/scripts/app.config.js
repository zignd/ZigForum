(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
            cfpLoadingBarProvider.includeSpinner = false;
        }])
        .config(function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: 'scripts/main/main.html',
                    controller: 'MainController',
                    controllerAs: 'vm'
                })
                .when('/forums', {
                    templateUrl: 'scripts/forums/forums.html',
                    controller: 'ForumsController',
                    controllerAs: 'vm'
                })
                .when('/forum/:id', {
                    templateUrl: 'scripts/forums/forum-detail.html',
                    controller: 'ForumDetailController',
                    controllerAs: 'vm'
                })
                .when('/posts/:id', {
                    templateUrl: 'scripts/posts/post-detail.html',
                    controller: 'PostDetailController',
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