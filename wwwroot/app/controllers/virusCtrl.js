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

        function getColumnName(idx) {
            var titleTemp = "";
            switch (idx) {
                case 0:
                    titleTemp = "Age";
                    break;
                case 1:
                    titleTemp = "Gender";
                    break;
                case 2:
                    titleTemp = "Children Amount";
                    break;
                case 3:
                    titleTemp = "VirusPositive";
                    break;
                case 4:
                    titleTemp = "Clinic";
                    break;
                case 5:
                    titleTemp = "Region";
                    break;
                case 6:
                    titleTemp = "Work Mode";
                    break;
                case 7:
                    titleTemp = "Actions";
                    break;
            }
            return titleTemp;
        }

        $(function () {
            $("#virusCasesTable").DataTable({
                columnDefs: [
                    {
                        searchable: false,
                        info: false,
                        orderable: false,
                        targets: 7,
                    }
                ],
                "responsive": true,
                "lengthChange": false,
                "autoWidth": false,
                "paging": true,
                "info": true,
                "searching": true,
                "ordering": false,
                "buttons": [
                    {
                        extend: "copy",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            format: {
                                header: function (data, columnIdx) {
                                    return getColumnName(columnIdx);
                                }
                            }
                        }
                    },
                    {
                        extend: "csv",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            format: {
                                header: function (data, columnIdx) {
                                    return getColumnName(columnIdx);
                                }
                            }
                        }
                    },
                    {
                        extend: "excel",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            format: {
                                header: function (data, columnIdx) {
                                    return getColumnName(columnIdx);
                                }
                            }
                        }
                    },
                    {
                        extend: "pdf",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            format: {
                                header: function (data, columnIdx) {
                                    return getColumnName(columnIdx);
                                }
                            }
                        }
                    },
                    {
                        extend: "print",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            format: {
                                header: function (data, columnIdx) {
                                    return getColumnName(columnIdx);
                                }
                            }
                        }
                    },
                    /*{ jak po wyszukaniu, odznaczy sie jakies kolumny, to zle formatuje tabele
                        extend: "colvis",
                        columnText: function (dt, idx, title) {
                            return getColumnName(idx);
                        }
                    }*/
                ],
            }).buttons().container().appendTo('#virusCasesTable_wrapper .col-md-6:eq(0)');
        });

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