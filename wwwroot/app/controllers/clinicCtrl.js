(function () {
    'use strict';

    angular
        .module('app')
        .controller('clinicCtrl', clinicCtrl);

    function clinicCtrl($scope) {
        var vm = this;

        _init(window.MvcModel);

        function _init(model) {
            vm.model = model;
            console.log(vm.model);
        }
    }
})();
