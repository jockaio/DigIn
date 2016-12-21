'use strict';
app.factory('projectsService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:54450/';
    var projectsServiceFactory = {};

    var _getProjectsData = function () {

        return $http.get(serviceBase + 'api/Projects').then(function (results) {
            return results;
        });
    };

    var _getProjectData = function (id) {

        return $http.get(serviceBase + 'api/Projects/'+id).then(function (results) {
            return results;
        });
    };

    var _postProjectData = function (data) {

        return $http.post(serviceBase + 'api/Projects', data).then(function (results) {
            return results;
        });
    };

    projectsServiceFactory.getProjectsData = _getProjectsData;
    projectsServiceFactory.getProjectData = _getProjectData;
    projectsServiceFactory.postProjectData = _postProjectData

    return projectsServiceFactory;

}]);