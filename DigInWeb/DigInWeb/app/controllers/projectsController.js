'use strict';
app.controller('projectsController', ['$scope', '$location', 'projectsService', '$routeParams', '$timeout', function ($scope, $location, projectsService, $routeParams, $timeout) {
    if ($routeParams.new != "new") {
        init($routeParams.projectID);
    }

    $scope.projectsOverviewData = [];
    $scope.projectData = {
        name: "",
        description: ""
    };
    $scope.message = "";

    //Get projects to fill viewmodel.
    function init(projectId) {
        console.log(projectId);
        if (projectId == undefined) {
            projectsService.getProjectsData().then(function (results) {
                console.log(results);
                $scope.projectsOverviewData = results.data;

            }, function (error) {
                console.log(error);
                alert(error.data.message);
            });
        } else {
            projectsService.getProjectData(projectId).then(function (results) {

                $scope.projectData = results.data;

            }, function (error) {
                console.log(error);
                alert(error.data.message);
            });
        }
    }

    $scope.saveProject = function () {
        console.log($scope.projectData);
        projectsService.postProjectData($scope.projectData).then(function (results) {
            $scope.message = "Ditt nya uppdrag har nu skapats. Du skickas vidare om 3 sekunder...";
            
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/projects/' + results.data.id);
            }, 3000);
            
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    }
    
}]);