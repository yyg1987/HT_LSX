


(function () {
    $(function () {

        var _$saleFunnelsTable = $('#SaleFunnelsTable');
        var _saleFunnelService = abp.services.app.saleFunnel;

        var _permissions = {
            create: abp.auth.hasPermission("Pages.SaleFunnel.CreateSaleFunnel"),
            edit: abp.auth.hasPermission("Pages.SaleFunnel.EditSaleFunnel"),
            'delete': abp.auth.hasPermission("Pages.SaleFunnel.DeleteSaleFunnel")

        };


        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/SaleFunnelManage/CreateOrEditSaleFunnelModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/SaleFunnelManage/_CreateOrEditSaleFunnelModal.js',
            modalClass: 'CreateOrEditSaleFunnelModal'
        });





        _$saleFunnelsTable.jtable({

            title: app.localize('SaleFunnel'),
            paging: true,
            sorting: true,
            //  multiSorting: true,
            actions: {
                listAction: {

                    method: _saleFunnelService.getPagedSaleFunnels
                }
                
            },

            fields: {

                actions: {
                    width: '2%',
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
                                deleteSaleFunnel(data.record);
                            }
                        }]
                },

                //此处开始循环数据



                id: {
                    key: true,
                    list: false
                },
                creationTime: {
                    title: app.localize('CreationTime'),
                    width: '2%'
                },
                districtName: {
                    title: app.localize('DistrictName'),
                    width: '2%'
                },


                saler: {
                    title: app.localize('Saler'),
                    width: '2%'
                },


                clientName: {
                    title: app.localize('ClientName'),
                    width: '2%'
                },


                adress: {
                    title: app.localize('Adress'),
                    width: '2%'
                },


                clientTypeName: {
                    title: app.localize('ClientTypeName'),
                    width: '2%'
                },


                productTypeName: {
                    title: app.localize('ProductTypeName'),
                    width: '2%'
                },


                productName: {
                    title: app.localize('ProductName'),
                    width: '2%'
                },


                productModel: {
                    title: app.localize('ProductModel'),
                    width: '2%'
                },


                price: {
                    title: app.localize('Price'),
                    width: '2%'
                },


                number: {
                    title: app.localize('Number'),
                    width: '2%'
                },


                sumPrice: {
                    title: app.localize('SumPrice'),
                    width: '2%'
                },


                statementTime: {
                    title: app.localize('StatementTime'),
                    width: '2%'
                },


                contendNumber: {
                    title: app.localize('ContendNumber'),
                    width: '2%'
                },


                opportunitie: {
                    title: app.localize('Opportunitie'),
                    width: '2%'
                },


                stage: {
                    title: app.localize('Stage'),
                    width: '2%'
                },


                stageTime: {
                    title: app.localize('StageTime'),
                    width: '2%'
                },


                chanceSum: {
                    title: app.localize('ChanceSum'),
                    width: '2%'
                },


                lastTime: {
                    title: app.localize('LastTime'),
                    width: '2%'
                },


                nextAction: {
                    title: app.localize('NextAction'),
                    width: '2%'
                },


                nextTime: {
                    title: app.localize('NextTime'),
                    width: '2%'
                },


                rivalA: {
                    title: app.localize('RivalA'),
                    width: '2%'
                },


                productModelA: {
                    title: app.localize('ProductModelA'),
                    width: '2%'
                },


                rivalB: {
                    title: app.localize('RivalB'),
                    width: '2%'
                },


                productModelB: {
                    title: app.localize('ProductModelB'),
                    width: '2%'
                },


                contact: {
                    title: app.localize('Contact'),
                    width: '2%'
                },


                contactMobile: {
                    title: app.localize('ContactMobile'),
                    width: '2%'
                },


                purchase: {
                    title: app.localize('Purchase'),
                    width: '2%'
                },


                purchaseMobile: {
                    title: app.localize('PurchaseMobile'),
                    width: '2%'
                },


                dean: {
                    title: app.localize('Dean'),
                    width: '2%'
                },


                deanMobile: {
                    title: app.localize('DeanMobile'),
                    width: '2%'
                },


                leadSource: {
                    title: app.localize('LeadSource'),
                    width: '2%'
                },


                createBy: {
                    title: app.localize('CreateBy'),
                    width: '2%'
                },


                updateTime: {
                    title: app.localize('UpdateTime'),
                    width: '2%'
                },


                updateBy: {
                    title: app.localize('UpdateBy'),
                    width: '2%'
                },

            }

        });


        //打开添加窗口SPA
        $('#CreateNewSaleFunnelButton').click(function () {
            //可选生成的对话框大小{size:'lg'}or{size:'sm'}
            //需要到_createContainer方法中添加,_args.size
            _createOrEditModal.open();
        });




        //刷新表格信息
        $("#ButtonReload").click(function () {
            getSaleFunnels();
        });




        //模糊查询功能
        function getSaleFunnels(reload) {
            if (reload) {
                _$saleFunnelsTable.jtable('reload');
            } else {
                _$saleFunnelsTable.jtable('load', {
                    filtertext: $('#SaleFunnelsTableFilter').val()
                });
            }
        }
        //
        //删除当前saleFunnel实体
        function deleteSaleFunnel(saleFunnel) {
            abp.message.confirm(
                app.localize('SaleFunnelDeleteWarningMessage', saleFunnel.clientType),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _saleFunnelService.deleteSaleFunnel({
                            id: saleFunnel.id
                        }).done(function () {
                            getSaleFunnels(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }



        //导出为excel文档
        $('#ExportToExcelButton').click(function () {
            _saleFunnelService
                .getSaleFunnelToExcel({})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });
        //搜索
        $('#GetSaleFunnelsButton').click(function (e) {
            e.preventDefault();
            getSaleFunnels();
        });

        //制作SaleFunnel事件,用于请求变化后，刷新表格信息
        abp.event.on('app.createOrEditSaleFunnelModalSaved', function () {
            getSaleFunnels(true);
        });

        getSaleFunnels();


    });
})();
