var app = angular.module('DigInApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ngMaterial', 'xeditable']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.when("/userprofile/:userProfileID", {
        controller: "userprofileController",
        templateUrl: "/app/views/userprofile.html"
    });

    $routeProvider.when("/myprojectsoverview", {
        controller: "projectsController",
        templateUrl: "/app/views/myprojectsoverview.html"
    });

    $routeProvider.when("/projects/:projectID", {
        controller: "projectsController",
        templateUrl: "/app/views/project.html"
    });

    $routeProvider.when("/createproject/:new", {
        controller: "projectsController",
        templateUrl: "/app/views/createproject.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);