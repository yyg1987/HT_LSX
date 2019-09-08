
(function () {
    $(function () {

        var _$productTypesTable = $('#ProductTypesTable');
        var _productTypeService = abp.services.app.productType;

        var _permissions = {
            create: abp.auth.hasPermission("Pages.ProductType.CreateProductType"),
            edit: abp.auth.hasPermission("Pages.ProductType.EditProductType"),
            'delete': abp.auth.hasPermission("Pages.ProductType.DeleteProductType")

        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/ProductTypeManage/CreateOrEditProductTypeModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/ProductTypeManage/_CreateOrEditProductTypeModal.js',
            modalClass: 'CreateOrEditProductTypeModal'
        });

        _$productTypesTable.jtable({

            title: app.localize('ProductType'),
            paging: true,
            sorting: true,
            //  multiSorting: true,
            actions: {
                listAction: {
                    method: _productTypeService.getPagedProductTypes
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
                                deleteProductType(data.record);
                            }
                        }]
                },
                //此处开始循环数据



                id: {
                    key: true,
                    list: false
                },

                productTypeName: {
                    title: app.localize('ProductTypeName'),
                    width: '5%'
                },


                isValid: {
                    title: app.localize('IsValid'),
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
        $('#CreateNewProductTypeButton').click(function () {
            //可选生成的对话框大小{size:'lg'}or{size:'sm'}
            //需要到_createContainer方法中添加,_args.size
            _createOrEditModal.open();
        });

        //刷新表格信息
        $("#ButtonReload").click(function () {
            getProductTypes();
        });

        //模糊查询功能
        function getProductTypes(reload) {
            if (reload) {
                _$productTypesTable.jtable('reload');
            } else {
                _$productTypesTable.jtable('load', {
                    filtertext: $('#ProductTypesTableFilter').val()
                });
            }
        }
        //删除当前productType实体
        function deleteProductType(productType) {
            abp.message.confirm(
                app.localize('ProductTypeDeleteWarningMessage', productType.productType),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _productTypeService.deleteProductType({
                            id: productType.id
                        }).done(function () {
                            getProductTypes(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }
        //导出为excel文档
        $('#ExportToExcelButton').click(function () {
            _productTypeService
                .getProductTypeToExcel({})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });
        //搜索
        $('#GetProductTypesButton').click(function (e) {
            e.preventDefault();
            getProductTypes();
        });

        //制作ProductType事件,用于请求变化后，刷新表格信息
        abp.event.on('app.createOrEditProductTypeModalSaved', function () {
            getProductTypes(true);
        });

        getProductTypes();

    });
})();
