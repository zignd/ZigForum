(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .service('forums', forums);
    
    forums.$inject = ['$http', 'API_URL'];
    
    function forums($http, API_URL) {
        var sv = this;
        var endpoint = API_URL + '/forums';
        
        /**
         * Retrieves all forums
         */
        sv.get = function (onSuccess, onFail, page, pageSize) {
            var params = {};
            if (typeof page === 'undefined') { params.page = page; }
            if (typeof pageSize === 'undefined') { params.pageSize = pageSize; }
            
            var config = {
                method: 'GET',
                url: endpoint,
                params: params
            };
            
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        };
        
        /**
         * Retrieves all top level forums
         */
        sv.getAllTopLevel = function (onSuccess, onFail, page, pageSize) {
            var params = {};
            if (typeof page === 'undefined') { params.page = page; }
            if (typeof pageSize === 'undefined') { params.pageSize = pageSize; }
            
            var config = {
                method: 'GET',
                url: endpoint + '/toplevel',
                params: params
            };
            
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        }
        
        /**
         * Retrieves a forum by its id
         */
        sv.getById = function (onSuccess, onFail, id) {
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
        
        /**
         * Creates a new forum
         */
        sv.createNew = function (onSuccess, onFail, newForum) {
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
        
        /**
         * Edits an existing forum
         */
        sv.edit = function (onSuccess, onFail, id, editedForum) {
            var config = {
                method: 'PATCH',
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
        
        /**
         * Deletes an existing forum
         */
        sv.delete = function (onSuccess, onFail, id) {
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