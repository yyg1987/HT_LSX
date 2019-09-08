
(function ($) {
    app.modals.CreateOrEditDistrictModal = function () {

        var _modalManager;

        var _districtService = abp.services.app.district;

		$(".maxlength-handler").maxlength({
            limitReachedClass: "label label-danger",
            alwaysShow: true,
            threshold: 5,
            placement: 'bottom'
        });

        var _$districtInformationForm = null;


        this.init = function (modalManager) {
            _modalManager = modalManager;
			            _$districtInformationForm = _modalManager.getModal().find("form[name=districtInformationsForm]");

			 
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
            if (!_$districtInformationForm.valid()) {
                return;
            }
            //校验通过

            var district = _$districtInformationForm.serializeFormToObject();
          //  console.log(district);

            _modalManager.setBusy(true);

            _districtService.createOrUpdateDistrict({
                districtEditDto: district
            }).done(function () {
                //提示信息
                abp.notify.info(app.localize('SavedSuccessfully'));
                //关闭窗体
                _modalManager.close();
                //信息保存成功后调用事件，刷新列表
                abp.event.trigger('app.createOrEditDistrictModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        }
    }
})(jQuery);

   