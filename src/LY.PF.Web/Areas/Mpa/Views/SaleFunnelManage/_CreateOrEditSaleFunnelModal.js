
(function ($) {
    app.modals.CreateOrEditSaleFunnelModal = function () {

        var _modalManager;

        var _saleFunnelService = abp.services.app.saleFunnel;

        $(".maxlength-handler").maxlength({
            limitReachedClass: "label label-danger",
            alwaysShow: true,
            threshold: 5,
            placement: 'bottom'
        });

        var _$saleFunnelInformationForm = null;


        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$saleFunnelInformationForm = _modalManager.getModal().find("form[name=saleFunnelInformationsForm]");
            // 初始化 StatementTime 的包含时分秒的日期控件
            //包含时分秒的日期选择器             
            $("input[name=StatementTime]").datetimepicker({
                autoclose: true,
                isRTL: false,
                format: "yyyy-mm-dd hh:ii",
                pickerPosition: ("bottom-left"),
                //默认为E文按钮要中文，自己去找语言包
                todayBtn: true,
                language: "zh-CN"
            });
            // 初始化 StageTime 的包含时分秒的日期控件
            //包含时分秒的日期选择器             
            $("input[name=StageTime]").datetimepicker({
                autoclose: true,
                isRTL: false,
                format: "yyyy-mm-dd hh:ii",
                pickerPosition: ("bottom-left"),
                //默认为E文按钮要中文，自己去找语言包
                todayBtn: true,
                language: "zh-CN"
            });
            // 初始化 LastTime 的包含时分秒的日期控件
            //包含时分秒的日期选择器             
            $("input[name=LastTime]").datetimepicker({
                autoclose: true,
                isRTL: false,
                format: "yyyy-mm-dd hh:ii",
                pickerPosition: ("bottom-left"),
                //默认为E文按钮要中文，自己去找语言包
                todayBtn: true,
                language: "zh-CN"
            });

            // 初始化 NextTime 的包含时分秒的日期控件
            //包含时分秒的日期选择器             
            $("input[name=NextTime]").datetimepicker({
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
            if (!_$saleFunnelInformationForm.valid()) {
                return;
            }
            //校验通过

            var saleFunnel = _$saleFunnelInformationForm.serializeFormToObject();
            //  console.log(saleFunnel);

            _modalManager.setBusy(true);

            _saleFunnelService.createOrUpdateSaleFunnel({
                saleFunnelEditDto: saleFunnel
            }).done(function () {
                //提示信息
                abp.notify.info(app.localize('SavedSuccessfully'));
                //关闭窗体
                _modalManager.close();
                //信息保存成功后调用事件，刷新列表
                abp.event.trigger('app.createOrEditSaleFunnelModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        }
    }
})(jQuery);

