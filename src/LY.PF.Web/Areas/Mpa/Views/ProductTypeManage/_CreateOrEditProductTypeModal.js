
(function ($) {
    app.modals.CreateOrEditProductTypeModal = function () {

        var _modalManager;

        var _productTypeService = abp.services.app.productType;

        $(".maxlength-handler").maxlength({
            limitReachedClass: "label label-danger",
            alwaysShow: true,
            threshold: 5,
            placement: 'bottom'
        });

        var _$productTypeInformationForm = null;


        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$productTypeInformationForm = _modalManager.getModal().find("form[name=productTypeInformationsForm]");
            // 初始化 CreateTime 的包含时分秒的日期控件
            //包含时分秒的日期选择器             
            $("input[name=CreationTime]").datetimepicker({
                autoclose: true,
                isRTL: false,
                format: "yyyy-mm-dd hh:ii",
                pickerPosition: ("bottom-left"),
                //默认为E文按钮要中文，自己去找语言包
                todayBtn: true,
                language: "zh-CN"
            });

            // 初始化 UpdateTime 的包含时分秒的日期控件
            //包含时分秒的日期选择器             
            $("input[name=UpdateTime]").datetimepicker({
                autoclose: true,
                isRTL: false,
                format: "yyyy-mm-dd hh:ii",
                pickerPosition: ("bottom-left"),
                //默认为E文按钮要中文，自己去找语言包
                todayBtn: true,
                language: "zh-CN"
            });


        }

        this.save = function () {
            if (!_$productTypeInformationForm.valid()) {
                return;
            }
            //校验通过

            var productType = _$productTypeInformationForm.serializeFormToObject();
            //  console.log(productType);

            _modalManager.setBusy(true);

            _productTypeService.createOrUpdateProductType({
                productTypeEditDto: productType
            }).done(function () {
                //提示信息
                abp.notify.info(app.localize('SavedSuccessfully'));
                //关闭窗体
                _modalManager.close();
                //信息保存成功后调用事件，刷新列表
                abp.event.trigger('app.createOrEditProductTypeModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        }
    }
})(jQuery);

