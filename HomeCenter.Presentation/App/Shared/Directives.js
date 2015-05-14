var HomeCenter;
(function (HomeCenter) {
    function Slider() {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attrs, model) {
                var elem = element.slider({
                    orientation: attrs.sliderOrientation || 'vertical',
                    min: parseFloat(attrs.sliderMin || 1),
                    max: parseFloat(attrs.sliderMax || 100),
                    step: parseFloat(attrs.sliderStep || 1),
                    range: attrs.sliderRange || 'min',
                    value: scope[attrs.ngModel],
                    slide: function (event, ui) {
                        scope.$apply(function () {
                            model.$setViewValue(ui.value);
                            if (attrs.sliderSlide)
                                scope.$eval(attrs.sliderSlide);
                        });
                    },
                    stop: function (event, ui) {
                        scope.$apply(function () {
                            if (attrs.sliderStop)
                                scope.$eval(attrs.sliderStop);
                        });
                    },
                    start: function (event, ui) {
                        scope.$apply(function () {
                            if (attrs.start) {
                                scope.$eval(attrs.start);
                            }
                        });
                    }
                });

                scope.$watch(attrs.ngModel, function (value) {
                    elem.slider('value', value);
                });
            }
        };
    }
    HomeCenter.Slider = Slider;
    ;

    function Cron() {
        return {
            restrict: 'A',
            link: function (scope, element, attrs, model) {
                var elem = element.cronGen();
            }
        };
    }
    HomeCenter.Cron = Cron;
    ;

    function Select() {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attrs, model) {
                var elem = element.selectpicker({
                    style: attrs.selectStyle || '',
                    size: parseInt(attrs.selectSize || 4)
                });
                scope.$watch(attrs.ngModel, function (newVal, oldVal) {
                    scope.$evalAsync(function () {
                        if (!attrs.ngOptions || /track by/.test(attrs.ngOptions))
                            element.val(newVal);
                        element.selectpicker('refresh');
                    });
                });

                scope.$watch(attrs.ngDisabled, function (newVal, oldVal) {
                    if (newVal) {
                        element.attr('disabled', 'disabled');
                    } else {
                        element.removeAttr('disabled');
                    }
                    element.selectpicker('refresh');
                });

                model.$render = function () {
                    scope.$evalAsync(function () {
                        element.selectpicker('refresh');
                    });
                };
            }
        };
    }
    HomeCenter.Select = Select;
    ;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=Directives.js.map
