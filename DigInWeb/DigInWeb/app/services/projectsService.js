'use strict';
app.factory('projectsService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:54450/';
    var projectsServiceFactory = {};

    var _getUserprofileData = function (id) {

        return $http.get(serviceBase + 'api/UserProfileModels/' + id).then(function (results) {
            return results;
        });
    };

    projectsServiceFactory.getUserprofileData = _getUserprofileData;
    projectsServiceFactory.putUserprofileData = _putUserprofileData;
    projectsServiceFactory.deleteSkill = _deleteSkill;
    projectsServiceFactory.userProfileMatch = _userProfileMatch;


    return projectsServiceFactory;

}]);