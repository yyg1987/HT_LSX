


(function () {
    $(function () {

        var _$districtsTable = $('#DistrictsTable');
        var _districtService = abp.services.app.district;

        var _permissions = {
            create: abp.auth.hasPermission("Pages.District.CreateDistrict"),
            edit: abp.auth.hasPermission("Pages.District.EditDistrict"),
            'delete': abp.auth.hasPermission("Pages.District.DeleteDistrict")

        };


        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/DistrictManage/CreateOrEditDistrictModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/DistrictManage/_CreateOrEditDistrictModal.js',
            modalClass: 'CreateOrEditDistrictModal'
        });





        _$districtsTable.jtable({

            title: app.localize('District'),
            paging: true,
            sorting: true,
            //  multiSorting: true,
            actions: {
                listAction: {
                    method: _districtService.getPagedDistricts
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
                                deleteDistrict(data.record);
                            }
                        }]
                },
                //此处开始循环数据



                id: {
                    key: true,
                    list: false
                },

                districtName: {
                    title: app.localize('DistrictName'),
                    width: '5%'
                },
                
                parentDistrictId: {
                    title: app.localize('ParentDistrictId'),
                    width: '5%',
                    display: function (data) {
                        var $span = $('<span></span>');
                        var pid = data.record.parentDistrictId;
                        if (pid !== 0) {
                            _districtService.getDistrictById({ id: pid, async: false }).done(function (result) {
                                $span.append('<span class="label label-info" data-toggle="tooltip" >' + result.districtName + '</span>&nbsp;');
                            });
                        }

                        $span.find('[data-toggle=tooltip]').tooltip();

                        return $span;
                    }

                },
                isValid: {
                    title: app.localize('IsValid'),
                    width: '5%'
                },


                address: {
                    title: app.localize('Address'),
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
        $('#CreateNewDistrictButton').click(function () {
            //可选生成的对话框大小{size:'lg'}or{size:'sm'}
            //需要到_createContainer方法中添加,_args.size
            _createOrEditModal.open();
        });




        //刷新表格信息
        $("#ButtonReload").click(function () {
            getDistricts();
        });




        //模糊查询功能
        function getDistricts(reload) {
            if (reload) {
                _$districtsTable.jtable('reload');
            } else {
                _$districtsTable.jtable('load', {
                    filtertext: $('#DistrictsTableFilter').val()
                });
            }
        }
        //
        //删除当前district实体
        function deleteDistrict(district) {
            abp.message.confirm(
                app.localize('DistrictDeleteWarningMessage', district.productType),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _districtService.deleteDistrict({
                            id: district.id
                        }).done(function () {
                            getDistricts(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }



        //导出为excel文档
        $('#ExportToExcelButton').click(function () {
            _districtService
                .getDistrictToExcel({})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });
        //搜索
        $('#GetDistrictsButton').click(function (e) {
            e.preventDefault();
            getDistricts();
        });

        //制作District事件,用于请求变化后，刷新表格信息
        abp.event.on('app.createOrEditDistrictModalSaved', function () {
            getDistricts(true);
        });

        getDistricts();


    });
})();
