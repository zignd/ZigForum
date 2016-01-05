(function () {
    'use strict';

    angular
        .module('zigforumApp')
        .service('users', users);

    users.$inject = ['$http', '$cookies', 'TOKEN_URL'];

    function users($http, $cookies, TOKEN_URL) {
        var sv = this;

        function NoAuthenticationException(message) {
            this.name = 'AuthenticationRequired';
            this.message = message;
        }

        function AuthenticationExpiredException(message) {
            this.name = 'AuthenticationExpired';
            this.message = message;
        }

        function AuthenticationRetrievalException(message) {
            this.name = 'AuthenticationRetrieval';
            this.message = message;
        }

        sv.userData = {
            isAuthenticated: false,
            username: '',
            bearerToken: '',
            expirationDate: null
        };

        function isAuthenticationExpired(expirationDate) {
            var now = new Date();
            expirationDate = new Date(expirationDate);

            if (expirationDate - now > 0) {
                return false;
            } else {
                return true;
            }
        }

        function saveData() {
            removeData();
            $cookies.putObject('auth_data', sv.userData);
        }

        function removeData() {
            $cookies.remove('auth_data');
        }

        function retrieveSavedData() {
            var savedData = $cookies.getObject('auth_data');

            if (typeof savedData === 'undefined') {
                throw new AuthenticationRetrievalException('No authentication data exists');
            } else if (isAuthenticationExpired(savedData.expirationDate)) {
                throw new AuthenticationExpiredException('Authentication token has already expired');
            } else {
                sv.userData = savedData;
                setHttpAuthHeader();
            }
        }

        function clearUserData() {
            sv.userData.isAuthenticated = false;
            sv.userData.username = '';
            sv.userData.bearerToken = '';
            sv.userData.expirationDate = null;
        }

        function setHttpAuthHeader() {
            $http.defaults.headers.common.Authorization = 'Bearer ' + sv.userData.bearerToken;
        }

        this.isAuthenticated = function () {
            if (!(sv.userData.isAuthenticated && !isAuthenticationExpired(sv.userData.expirationDate))) {
                try {
                    retrieveSavedData();
                } catch (e) {
                    return false;
                }
            }

            return true;
        };

        this.removeAuthentication = function () {
            removeData();
            clearUserData();
            $http.defaults.headers.common.Authorization = null;
        };

        this.authenticate = function (username, password, persistData, successCallback, errorCallback) {
            this.removeAuthentication();
            var config = {
                method: 'POST',
                url: TOKEN_URL,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: 'grant_type=password&username=' + username + '&password=' + password
            };

            $http(config)
                .success(function (data) {
                    sv.userData.isAuthenticated = true;
                    sv.userData.username = data.userName;
                    sv.userData.bearerToken = data.access_token;
                    sv.userData.expirationDate = new Date(data['.expires']);
                    setHttpAuthHeader();
                    if (persistData === true) {
                        saveData();
                    }
                    if (typeof successCallback === 'function') {
                        successCallback();
                    }
                })
                .error(function (data) {
                    if (typeof errorCallback === 'function') {
                        if (data && data.error_description) {
                            errorCallback(data.error_description);
                        } else {
                            errorCallback('Unable to contact server; please, try again later.');
                        }
                    }
                });
        };

        try {
            retrieveSavedData();
        } catch (e) { }
    }
})();