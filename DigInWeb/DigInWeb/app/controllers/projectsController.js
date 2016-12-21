'use strict';
app.controller('projectsController', ['$scope', '$location','projectsService', '$routeParams', function ($scope, $location, projectsService, $routeParams) {
    if ($routeParams.new != "new") {
        init($routeParams.projectID);
    }

    $scope.projectsOverviewData = [];
    $scope.projectData = {
        name: "",
        description: ""
    };

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
            $location.path('/projects/' + results.data.id);
        }, function (error) {
            console.log(error);
            alert(error.data.message);
        });
    }
    
}]);