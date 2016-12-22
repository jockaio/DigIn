'use strict';
app.controller('userprofileController', ['$scope', 'userprofileService', '$mdDialog', '$routeParams', '$filter', 'skillsService', function ($scope, userprofileService, $mdDialog, $routeParams, $filter, skillsService) {

    $scope.userprofileData = {
        firstName: "",
        lastName: "",
        email: "",
        skills: []
    }

    $scope.skill = {
        name: "",
        description: ""
    }

    $scope.skillsCategories = [];

    //Edit fields
    $scope.mouseOverDescription = false;
    $scope.editDescription = false;
    $scope.showSkillsForm = false;

    //Get user data to fill viewmodel.
    userprofileService.getUserprofileData($routeParams.userProfileID).then(function (results) {
        
        $scope.userprofileData = results.data;

    }, function (error) {
        console.log(error);
        alert(error.data.message);
    });

    //Get skillsCategories
    skillsService.getSkillsCategories().then(function (results) {
        $scope.skillsCategories = results.data;
    }, function (error) {
        console.log(error);
        alert(error.data.message);
    });

    //Get user skills
    skillsService.getSkills($routeParams.userProfileID).then(function (results) {
        $scope.userprofileData.skills = results.data;
    }, function (error) {
        console.log(error);
        alert(error.data.message);
    });

    //Save profile information
    $scope.saveProfile = function () {
        userprofileService.putUserprofileData($scope.userprofileData).then(function (results) {
            $scope.userprofileData = results.data;
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    }

    //Save skill
    $scope.saveSkill = function() {
        
        //Save skill
        skillsService.saveSkill($scope.skill).then(function (results) {
            $scope.userprofileData.skills = results.data;
            //Hide skills form
            $scope.showSkillsForm = false;
            //Reset skill
            $scope.skill = {
                name: "",
                description: ""
            }
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    }

    $scope.saveEditedSkill = function (data, id) {
        var skill = $filter('filter')($scope.userprofileData.skills, { id: id })[0];
        skill.skillsCategoryID = data.skillsCategoryID;
        skill.description = data.description;
        //Save edited skill
        skillsService.editSkill(id, skill).then(function (results) {
            $scope.skills = results.data;
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    }

    $scope.testMethod = function () {
        console.log("clicked");
    }

    $scope.deleteSkill = function (skill) {
        skillsService.deleteSkill(skill.id).then(function (results) {
            $scope.userprofileData.skills = results.data;
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    };

    $scope.showSkillsCategory = function (skill) {
        var selected = [];
        if (skill.skillsCategoryID) {
            selected = $filter('filter')($scope.skillsCategories, skill.skillsCategoryID);
        }
        return selected.length ? selected[0].name : 'Välj kategori';
    };

    $scope.toInt = function (key) {
        // 10 is the radix, which is the base
        return parseInt(key, 10);
    }

    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    }

}]);