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

        function getColumnName(idx){
            var titleTemp = "";
            switch (idx) {
                case 0:
                    titleTemp = "Name";
                    break;
                case 1:
                    titleTemp = "Actions";
                    break;
            }
            return titleTemp;
        }

        $(function () {
            $("#clinicsTable").DataTable({
                columnDefs: [
                    {
                        searchable: false,
                        info: false,
                        orderable: false,
                        targets: 1
                    },
                    {
                        searchPanes: {
                            show: true
                        },
                        targets: [0]
                    }
                ],
                "responsive": true,
                "lengthChange": false,
                "autoWidth": false,
                "paging": true,
                "info": true,
                "searching": true,
                "buttons": {
                    buttons: [
                        {
                            extend: 'searchPanes'
                        },
                        {
                            extend: "copy",
                            exportOptions: {
                                columns: [0],
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
                                columns: [0],
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
                                columns: [0],
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
                                columns: [0],
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
                                columns: [0],
                                format: {
                                    header: function (data, columnIdx) {
                                        return getColumnName(columnIdx);
                                    }
                                }
                            }
                        },
                        {
                            extend: "colvis",
                            columnText: function (dt, idx, title) {
                                return getColumnName(idx);
                            }
                        }
                    ],
                }
            }).buttons().container().appendTo('#clinicsTable_wrapper .col-md-6:eq(0)');
        });
    }
})();
