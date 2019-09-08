


(function () {
    $(function () {

        var _$clientTypesTable = $('#ClientTypesTable');
        var _clientTypeService = abp.services.app.clientType;

        var _permissions = {
            create: abp.auth.hasPermission("Pages.ClientType.CreateClientType"),
            edit: abp.auth.hasPermission("Pages.ClientType.EditClientType"),
            'delete': abp.auth.hasPermission("Pages.ClientType.DeleteClientType")

        };


        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/ClientTypeManage/CreateOrEditClientTypeModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/ClientTypeManage/_CreateOrEditClientTypeModal.js',
            modalClass: 'CreateOrEditClientTypeModal'
        });





        _$clientTypesTable.jtable({

            title: app.localize('ClientType'),
            paging: true,
            sorting: true,
            //  multiSorting: true,
            actions: {
                listAction: {
                    method: _clientTypeService.getPagedClientTypes
                }
            },

            fields: {

                actions: {
                    width: '5%',
                    type: 'record-actions',
                    cssClass: 'btn btn-xs btn-primary blue',
                    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                    items: [
                        {
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteClientType(data.record);
                            }
                        }]
                },
                //此处开始循环数据



                id: {
                    key: true,
                    list: false
                },

                clientTypeName: {
                    title: app.localize('ClientTypeName'),
                    width: '5%'
                },

                remark: {
                    title: app.localize('Remark'),
                    width: '5%'
                },


                isValid: {
                    title: app.localize('IsValid'),
                    width: '5%',
                    display: function (data) {

                        if (data.record.isValid) {
                            return "<span class=\"label label-success\">正常</span>";
                        }

                        return "<span class=\"label label-danger\">禁用</span>";

                    }

                },


                createBy: {
                    title: app.localize('CreateBy'),
                    width: '5%'
                },


                creationTime: {
                    title: app.localize('CreationTime'),
                    width: '5%'
                },


                updateBy: {
                    title: app.localize('UpdateBy'),
                    width: '5%'
                },


                updateTime: {
                    title: app.localize('UpdateTime'),
                    width: '5%'
                },

            }

        });


        //打开添加窗口SPA
        $('#CreateNewClientTypeButton').click(function () {
            //可选生成的对话框大小{size:'lg'}or{size:'sm'}
            //需要到_createContainer方法中添加,_args.size
            _createOrEditModal.open();
        });




        //刷新表格信息
        $("#ButtonReload").click(function () {
            getClientTypes();
        });




        //模糊查询功能
        function getClientTypes(reload) {
            if (reload) {
                _$clientTypesTable.jtable('reload');
            } else {
                _$clientTypesTable.jtable('load', {
                    filtertext: $('#ClientTypesTableFilter').val()
                });
            }
        }
        //
        //删除当前clientType实体
        function deleteClientType(clientType) {
            abp.message.confirm(
                app.localize('ClientTypeDeleteWarningMessage', clientType.productType),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _clientTypeService.deleteClientType({
                            id: clientType.id
                        }).done(function () {
                            getClientTypes(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }



        //导出为excel文档
        $('#ExportToExcelButton').click(function () {
            _clientTypeService
                .getClientTypeToExcel({})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });
        //搜索
        $('#GetClientTypesButton').click(function (e) {
            e.preventDefault();
            getClientTypes();
        });

        //制作ClientType事件,用于请求变化后，刷新表格信息
        abp.event.on('app.createOrEditClientTypeModalSaved', function () {
            getClientTypes(true);
        });

        getClientTypes();


    });
})();
