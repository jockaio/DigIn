'use strict';
app.controller('userprofileController', ['$scope', 'userprofileService', '$mdDialog', function ($scope, userprofileService, $mdDialog) {

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

    //Get user data to fill viewmodel.
    userprofileService.getUserprofileData().then(function (results) {

        $scope.userprofileData = results.data;

        console.log($scope.userprofileData);

        if ($scope.userprofileData.skills == null) {
            $scope.userprofileData.skills = [];
        }

    }, function (error) {
        console.log(error);
        alert(error.data.message);
    });

    $scope.openSkillsForm = function (ev) {
        $scope.skill = {
            name: "",
            description: ""
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
            console.log(answer);
            $scope.userprofileData.skills.push(answer);
            console.log($scope.userprofileData);
            userprofileService.putUserprofileData($scope.userprofileData).then(function (results) {

            }, function (error) {
                console.log(error);
                alert(error.data.message);
            });
        }, function () {
        });
    };



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