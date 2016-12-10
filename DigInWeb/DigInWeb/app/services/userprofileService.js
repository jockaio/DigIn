'use strict';
app.factory('userprofileService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:54450/';
    var userprofileServiceFactory = {};

    var _getUserprofileData = function () {

        return $http.get(serviceBase + 'api/UserProfileModels').then(function (results) {
            return results;
        });
    };

    var _putUserprofileData = function (data) {

        return $http.put(serviceBase + 'api/UserProfileModels', data).then(function (results) {
            return results;
        });
    };

    userprofileServiceFactory.getUserprofileData = _getUserprofileData;
    userprofileServiceFactory.putUserprofileData = _putUserprofileData;


    return userprofileServiceFactory;

}]);