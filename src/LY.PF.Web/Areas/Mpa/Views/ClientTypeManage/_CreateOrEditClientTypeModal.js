
(function ($) {
    app.modals.CreateOrEditClientTypeModal = function () {

        var _modalManager;

        var _clientTypeService = abp.services.app.clientType;

        $(".maxlength-handler").maxlength({
            limitReachedClass: "label label-danger",
            alwaysShow: true,
            threshold: 5,
            placement: 'bottom'
        });

        var _$clientTypeInformationForm = null;


        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$clientTypeInformationForm = _modalManager.getModal().find("form[name=clientTypeInformationsForm]");


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
            if (!_$clientTypeInformationForm.valid()) {
                return;
            }
            //校验通过

            var clientType = _$clientTypeInformationForm.serializeFormToObject();
            //  console.log(clientType);

            _modalManager.setBusy(true);

            _clientTypeService.createOrUpdateClientType({
                clientTypeEditDto: clientType
            }).done(function () {
                //提示信息
                abp.notify.info(app.localize('SavedSuccessfully'));
                //关闭窗体
                _modalManager.close();
                //信息保存成功后调用事件，刷新列表
                abp.event.trigger('app.createOrEditClientTypeModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        }
    }
})(jQuery);

