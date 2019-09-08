

function addfunc() {
    add();
}

var addNumber = 0;
function add() {
    addNumber++;

    var it = $("#ItemTemplate>form").clone(true, true);
    var targetname = "hideIframe" + addNumber;
    it.attr("target", targetname);
    var ifram = it.find("iframe");
    ifram.attr("name", targetname);

    //progress
    var progress = it.find('.progress');
    var bar = it.find('.bar');
    var percent = it.find('.percent');
    var span = it.find("[flagOutput]");

    //初始不显示进度条
    progress.hide();

    it.ajaxForm({
        beforeSend: function () {
            span.removeClass();
            span.empty();
            progress.show(100);
            var percentVal = '0%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + '%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        success: function () {
            var percentVal = '100%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        complete: function (xhr) {
            progress.hide(100);
            var data = JSON.parse(xhr.responseText);
            span.text(data.result.message);
            span.removeClass();
            switch (data.result.state) {
                case 'success':
                    span.addClass("alert alert-success");
                    break;
                case 'error':
                    span.addClass("alert alert-danger");
                    break;
                default:
            }
            if (data.result.state === 'success') {
                getSaleOrders();
                setTimeout(function () {
                    it.hide(100, function () {
                        it.remove();
                    });
                }, 1000);
            }
        }
    });

    it.find("[flagSubmit]").on("click", submit);
    it.find("[flagCancel]").on("click", cancel);

    $("#addPanel").append(it);
}

function submit(e) {
    e.preventDefault();
    var frm = $(this).closest("form");
    if (!frm.valid()) {
        return false;
    }
    if (!checkFile(frm)) {
        return;
    }
    frm.submit();
}
function cancel(e) {
    e.preventDefault();
    $(this).closest("form").remove();
}
function checkFile(frm) {
    var maxFileSize = 1024 * 1024 * 30;
    var mfsMsg = "文件不能大于30M";
    //var nameRgx =  /.*/; ///^[^_]{1,}_[^_]{1,}_[ABCD]_\d{1,}\.\d{1,}$/;
    var notMatchMsg = "文件名格式不正确";
    var $input = frm.find(':file');
    var fs = $input[0].files;
    var size = fs[0].size;
    var name = fs[0].name;
    var idx = name.lastIndexOf('.');
        name = name.slice(0, idx);
    //if (!nameRgx.test(name)) {
    //    setOutput(frm, 'error', notMatchMsg);
    //    return false;
    //}
    if (size > maxFileSize) {
        setOutput(frm, 'error', mfsMsg);
        return false;
    }
    //var devAppType = $("#DevAppType").val();
    //if (devAppType == 1 && name.indexOf("Juicer") != 0) {
    //    setOutput(frm, 'error', "Juicer 类型只能上传 juicer_xxxx.APK");
    //    return false;
    //} else if (devAppType == 2 && name.indexOf("Camera") != 0) {
    //    setOutput(frm, 'error', "Camera类型只能取Camera_xxxx.APK");
    //    return false;
    //}

    return true;
}

function setOutput(frm, state, msg) {
    var span = frm.find("[flagOutput]");
    span.text(msg);
    span.removeClass();
    switch (state) {
        case 'success':
            span.addClass("alert alert-success");
            break;
        case 'error':
            span.addClass("alert alert-danger");
            break;
        default:
    }
}

function Download(id) {
    if (id === null) {
        layer.msg("请选择要下载的数据行", "warning");
    }
    else {
        $.ajax({
            type: "post",
            url: "/Mpa/SaleOrderManage/Download",
            data: { KeyValue: id },
            dataType: "json",
            success: function (data) {
                if (data.state === "success") {
                    window.location.href = data.message;
                } else {
                    $.modalAlert(data.message, data.state);
                }
            }
        });
    }
}

$(function () {

    getPagedSaleOrder();
    getSaleOrders();
})

var _$saleOrdersTable = $('#SaleOrdersTable');
var _saleOrderService = abp.services.app.saleOrder;

var _permissions = {
    create: abp.auth.hasPermission("Pages.SaleOrder.CreateSaleOrder"),
    edit: abp.auth.hasPermission("Pages.SaleOrder.EditSaleOrder"),
    'delete': abp.auth.hasPermission("Pages.SaleOrder.DeleteSaleOrder")

};

var _createOrEditModal = new app.ModalManager({
    viewUrl: abp.appPath + 'Mpa/SaleOrderManage/CreateOrEditSaleOrderModal',
    scriptUrl: abp.appPath + 'Areas/Mpa/Views/SaleOrderManage/_CreateOrEditSaleOrderModal.js',
    modalClass: 'CreateOrEditSaleOrderModal'
});

function getPagedSaleOrder() {

    var _permissions = {
        create: abp.auth.hasPermission("Pages.SaleOrder.CreateSaleOrder"),
        edit: abp.auth.hasPermission("Pages.SaleOrder.EditSaleOrder"),
        'delete': abp.auth.hasPermission("Pages.SaleOrder.DeleteSaleOrder")

    };


    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Mpa/SaleOrderManage/CreateOrEditSaleOrderModal',
        scriptUrl: abp.appPath + 'Areas/Mpa/Views/SaleOrderManage/_CreateOrEditSaleOrderModal.js',
        modalClass: 'CreateOrEditSaleOrderModal'
    });

    _$saleOrdersTable.jtable({

        title: app.localize('SaleOrder'),
        paging: true,
        sorting: true,
        //  multiSorting: true,
        actions: {
            listAction: {
                method: _saleOrderService.getPagedSaleOrders
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
                            deleteSaleOrder(data.record);
                        }
                    }]
            },
            //此处开始循环数据



            id: {
                key: true,
                list: false
            },

            //imageUrl: {
            //    title: app.localize('ImageUrl'),
            //    width: '5%',
            //    display: function (data) {

            //        return '<a style="cursor: pointer; text-decoration:none;"  onclick="open_win(\'' + data.record.imageUrl + '\');" href="#" data-rel="tooltip" title="查看用户">' +
            //            '<img src="' + data.record.imageUrl + '" alt="头像未找到"   style="width:80px;height:30px;" />' + '</a>';
            //    }

            //},

            productName: {
                title: app.localize('ProductName'),
                width: '5%'
            },


            productTypeName: {
                title: app.localize('ProductType'),
                width: '5%'
            },


            //scheduleNumber: {
            //    title: app.localize('ScheduleNumber'),
            //    width: '5%'
            //},


            //actualNumber: {
            //    title: app.localize('ActualNumber'),
            //    width: '5%'
            //},


            //price: {
            //    title: app.localize('Price'),
            //    width: '5%'
            //},


            //totalPrice: {
            //    title: app.localize('TotalPrice'),
            //    width: '5%'
            //},


            statusName: {
                title: app.localize('StatusName'),
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

}

//打开添加窗口SPA
$('#CreateNewSaleOrderButton').click(function () {
    //可选生成的对话框大小{size:'lg'}or{size:'sm'}
    //需要到_createContainer方法中添加,_args.size
    _createOrEditModal.open();
})

//刷新表格信息
$("#ButtonReload").click(function () {
    getSaleOrders();
})

//模糊查询功能
function getSaleOrders(reload) {
    if (reload) {
        _$saleOrdersTable.jtable('reload');
    } else {
        _$saleOrdersTable.jtable('load', {
            filtertext: $('#SaleOrdersTableFilter').val()
        });
    }
}

//删除当前saleOrder实体
function deleteSaleOrder(saleOrder) {
    abp.message.confirm(
        app.localize('SaleOrderDeleteWarningMessage', saleOrder.productType),
        function (isConfirmed) {
            if (isConfirmed) {
                _saleOrderService.deleteSaleOrder({
                    id: saleOrder.id
                }).done(function () {
                    getSaleOrders(true);
                    abp.notify.success(app.localize('SuccessfullyDeleted'));
                });
            }
        }
    );
}

//导出为excel文档
$('#ExportToExcelButton').click(function () {
    _saleOrderService
        .getSaleOrderToExcel({})
        .done(function (result) {
            app.downloadTempFile(result);
        });
})
//搜索
$('#GetSaleOrdersButton').click(function (e) {
    e.preventDefault();
    getSaleOrders();
})

//制作SaleOrder事件,用于请求变化后，刷新表格信息
abp.event.on('app.createOrEditSaleOrderModalSaved', function () {
    getSaleOrders(true);
})



