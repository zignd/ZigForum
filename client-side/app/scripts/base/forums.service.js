(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .service('forums', forums);
    
    forums.$inject = ['$http', 'API_URL'];
    
    function forums($http, API_URL) {
        var sv = this;
        var endpoint = API_URL + '/forums';
        
        sv.getAllForums = function (onSuccess, onFail) {
            var config = {
                method: 'GET',
                url: endpoint
            };
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        };
        
        sv.getForumById = function (id, onSuccess, onFail) {
            var config = {
                method: 'GET',
                url: endpoint + '/' + id
            };
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        };
        
        sv.createNewForum = function (newForum, onSuccess, onFail) {
            var config = {
                method: 'POST',
                url: endpoint + '/create',
                data: newForum
            };
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        };
        
        sv.editForum = function (id, editedForum, onSuccess, onFail) {
            var config = {
                method: 'PUT',
                url: endpoint + '/' + id + '/edit',
                data: editedForum
            };
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        };
        
        sv.deleteForum = function (id, onSuccess, onFail) {
            var config = {
                method: 'DELETE',
                url: endpoint + '/' + id + '/delete' 
            };
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        };
    }
})();