


(function () {
    $(function () {

        var _$indentsTable = $('#IndentsTable');
        var _indentService = abp.services.app.indent;

        var _permissions = {
            create: abp.auth.hasPermission("Pages.Indent.CreateIndent"),
            edit: abp.auth.hasPermission("Pages.Indent.EditIndent"),
            'delete': abp.auth.hasPermission("Pages.Indent.DeleteIndent")

        };


        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/IndentManage/CreateOrEditIndentModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/IndentManage/_CreateOrEditIndentModal.js',
            modalClass: 'CreateOrEditIndentModal'
        });





        _$indentsTable.jtable({

            title: app.localize('Indent'),
            paging: true,
            sorting: true,
            //  multiSorting: true,
            actions: {
                listAction: {
                    method: _indentService.getPagedIndents
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
                                deleteIndent(data.record);
                            }
                        }]
                },
                //此处开始循环数据



                id: {
                    key: true,
                    list: false
                },

                productName: {
                    title: app.localize('ProductName'),
                    width: '5%'
                },


                productType: {
                    title: app.localize('ProductType'),
                    width: '5%'
                },


                scheduleNumber: {
                    title: app.localize('ScheduleNumber'),
                    width: '5%'
                },


                actualNumber: {
                    title: app.localize('ActualNumber'),
                    width: '5%'
                },


                price: {
                    title: app.localize('Price'),
                    width: '5%'
                },


                totalPrice: {
                    title: app.localize('TotalPrice'),
                    width: '5%'
                },


                status: {
                    title: app.localize('Status'),
                    width: '5%'
                },


                remark: {
                    title: app.localize('Remark'),
                    width: '5%'
                },


                createBy: {
                    title: app.localize('CreateBy'),
                    width: '5%'
                },


                createTime: {
                    title: app.localize('CreateTime'),
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
        $('#CreateNewIndentButton').click(function () {
            //可选生成的对话框大小{size:'lg'}or{size:'sm'}
            //需要到_createContainer方法中添加,_args.size
            _createOrEditModal.open();
        });




        //刷新表格信息
        $("#ButtonReload").click(function () {
            getIndents();
        });




        //模糊查询功能
        function getIndents(reload) {
            if (reload) {
                _$indentsTable.jtable('reload');
            } else {
                _$indentsTable.jtable('load', {
                    filtertext: $('#IndentsTableFilter').val()
                });
            }
        }
        //
        //删除当前indent实体
        function deleteIndent(indent) {
            abp.message.confirm(
                app.localize('IndentDeleteWarningMessage', indent.productType),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _indentService.deleteIndent({
                            id: indent.id
                        }).done(function () {
                            getIndents(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }



        //导出为excel文档
        $('#ExportToExcelButton').click(function () {
            _indentService
                .getIndentToExcel({})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });
        //搜索
        $('#GetIndentsButton').click(function (e) {
            e.preventDefault();
            getIndents();
        });

        //制作Indent事件,用于请求变化后，刷新表格信息
        abp.event.on('app.createOrEditIndentModalSaved', function () {
            getIndents(true);
        });

        getIndents();


    });
})();
