(function () {
    'use strict';

    var app = angular
        .module('app', []);

        app.run(["$rootScope", function ($rootScope) {
            $rootScope.RootUrl = window.RootUrl;
        }]);
})();