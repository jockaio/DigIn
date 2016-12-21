'use strict';
app.factory('userprofileService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:54450/';
    var userprofileServiceFactory = {};

    var _getUserprofileData = function (id) {

        return $http.get(serviceBase + 'api/UserProfileModels/'+ id).then(function (results) {
            return results;
        });
    };

    var _putUserprofileData = function (data) {

        return $http.put(serviceBase + 'api/UserProfileModels', data).then(function (results) {
            return results;
        });
    };

    var _deleteSkill = function (id) {
        return $http.delete(serviceBase + 'api/DeleteSkill/' + id).then(function (results) {
            return results;
        });
    };

    var _userProfileMatch = function (id) {
        return $http.get(serviceBase + 'api/UserProfileMatch/' + id).then(function (results) {
            return results;
        });
    };

    userprofileServiceFactory.getUserprofileData = _getUserprofileData;
    userprofileServiceFactory.putUserprofileData = _putUserprofileData;
    userprofileServiceFactory.deleteSkill = _deleteSkill;
    userprofileServiceFactory.userProfileMatch = _userProfileMatch;


    return userprofileServiceFactory;

}]);