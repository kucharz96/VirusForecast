(function () {
    'use strict';

    angular
        .module('app')
        .controller('accountCtrl', accountCtrl);

    function accountCtrl($scope) {
        var vm = this;

        _init(window.MvcModel);

        function _init(model) {
            vm.model = model;
            console.log(vm.model);
        }

        vm.Hallo = function (email) {
            alert(email);
        }
    }
})();
