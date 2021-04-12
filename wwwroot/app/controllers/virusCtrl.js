(function () {
    'use strict';

    angular
        .module('app')
        .controller('virusCtrl', virusCtrl);

    function virusCtrl($scope,$http) {
        var vm = this;

        _init(window.MvcModel);

        function _init(model) {
            vm.model = model;
            console.log(vm.model);
        }

        //vm.WriteFileName = function () {
        //    var fileUploaded = document.getElementById("fileChoice");
        //    var path = fileUploaded.value;
        //    var name = path.substring(path.lastIndexOf("\\") + 1, path.length);
        //    var label = document.getElementById("fileLabel");
        //    label.innerHTML = name;
        //}
        //vm.sendForm = function () {
        //    $http({
        //        method: 'POST',
        //        url: '/Virus/Add'
        //    }).success(function (data, status, headers, config) {
        //        aler("succes");
        //    }).error(function (data, status, headers, config) {
        //        aler("error");
        //    });
        //};
    }

    
})();