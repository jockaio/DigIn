'use strict';
app.factory('skillsService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:54450/';
    var skillsServiceFactory = {};

    var _getSkillsCategories = function () {

        return $http.get(serviceBase + 'api/SkillsCategories').then(function (results) {
            return results;
        });
    };

    var _getSkills = function (id) {

        return $http.get(serviceBase + 'api/Skills/' + id).then(function (results) {
            return results;
        });
    };

    var _putSkill = function (id, skill) {

        return $http.put(serviceBase + 'api/Skills/'+ id , skill).then(function (results) {
            return results;
        });
    };

    var _postSkill = function (skill) {

        return $http.post(serviceBase + 'api/Skills', skill).then(function (results) {
            return results;
        });
    };

    var _deleteSkill = function (id) {
        return $http.delete(serviceBase + 'api/Skills/' + id).then(function (results) {
            return results;
        });
    };

    skillsServiceFactory.getSkillsCategories = _getSkillsCategories;
    skillsServiceFactory.getSkills = _getSkills;
    skillsServiceFactory.editSkill = _putSkill;
    skillsServiceFactory.saveSkill = _postSkill;
    skillsServiceFactory.deleteSkill = _deleteSkill;

    return skillsServiceFactory;

}]);