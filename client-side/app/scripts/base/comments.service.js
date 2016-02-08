(function () {
    'use strict';
    
    angular
        .module('zigforumApp')
        .service('comments', comments);
        
    comments.$inject = ['$http', 'API_URL'];
    
    function comments($http, API_URL) {
        var sv = this;
        var endpoint = API_URL + '/comments';
        
        /**
         * Retrieves comments by a post id
         */
        sv.getByPostId = function (onSuccess, onFail, id) {
            var config = {
                method: 'GET',
                url: endpoint + '/postid/' + id
            };
            
            $http(config)
                .then(function (response) {
                    onSuccess(response.data);
                }, function (response) {
                    onFail(response.statusText);
                });
        }
        
        /**
         * Creates a new comment
         */
        sv.createNew = function (onSuccess, onFail, newPost) {
            var config = {
                method: 'POST',
                url: endpoint + '/create',
                data: newPost
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