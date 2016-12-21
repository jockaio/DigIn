'use strict';
app.controller('userprofileController', ['$scope', 'userprofileService', '$mdDialog', '$routeParams', function ($scope, userprofileService, $mdDialog, $routeParams) {

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

    //Edit fields
    $scope.mouseOverDescription = false;
    $scope.editDescription = false;
    $scope.showSkillsForm = false;

    //Get user data to fill viewmodel.
    userprofileService.getUserprofileData($routeParams.userProfileID).then(function (results) {
        
        $scope.userprofileData = results.data;

        if ($scope.userprofileData.skills == null) {
            $scope.userprofileData.skills = [];
        }

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
    $scope.saveSkill = function(id) {
        //Add skill
        $scope.userprofileData.skills.push($scope.skill);
        //Save profile
        userprofileService.putUserprofileData($scope.userprofileData).then(function (results) {
            $scope.userprofileData = results.data;
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
        //Save user profile
        userprofileService.putUserprofileData($scope.userprofileData).then(function (results) {
            $scope.userprofileData = results.data;
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    }

    //Should not be used
    $scope.openSkillsForm = function (ev, skill) {
        if (skill == null) {
            $scope.skill = {
                name: "",
                description: ""
            }
        }

        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'app/views/shared/skillformpopup.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
        })
        .then(function (answer) {
            $scope.userprofileData.skills.push(answer);
            userprofileService.putUserprofileData($scope.userprofileData).then(function (results) {
                $scope.userprofileData = results.data;
            }, function (error) {
                console.log(error);
                alert(error.data.message);
            });
        }, function () {
        });
    };

    $scope.testMethod = function () {
        console.log("clicked");
    }

    $scope.deleteSkill = function (skill) {
        userprofileService.deleteSkill(skill.id).then(function (results) {
            $scope.userprofileData.skills = results.data;
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    };

    $scope.editSkill = function (skill) {
        
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