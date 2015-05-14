module HomeCenter
{
    export function Slider() : ng.IDirective
    {
        return {
            require: 'ngModel', // http://stackoverflow.com/questions/12789136/optimizing-angularjs-directive-that-updates-ng-model-and-ng-style-on-focus
            restrict: 'A',
            link: (scope: ng.IScope, element : JQuery, attrs: any, model : ng.INgModelController) =>
            {

                var elem = element.slider({
                    orientation: attrs.sliderOrientation || 'vertical',
                    min: parseFloat(attrs.sliderMin || 1),
                    max: parseFloat(attrs.sliderMax || 100),
                    step: parseFloat(attrs.sliderStep || 1),
                    range: attrs.sliderRange || 'min',
                    value: scope[attrs.ngModel],
                    slide: (event, ui) =>
                    {
                        scope.$apply(() =>
                        {
                            model.$setViewValue(ui.value);
                            if (attrs.sliderSlide)
                                scope.$eval(attrs.sliderSlide);
                        });
                    },
                    stop: (event, ui) =>
                    {
                        scope.$apply(() =>
                        {
                            if (attrs.sliderStop)
                                scope.$eval(attrs.sliderStop);
                        });
                    },
                    start: (event, ui) =>
                    {
                        scope.$apply(() =>
                        {
                            if (attrs.start)
                            {
                                scope.$eval(attrs.start);
                            }
                                
                        });
                    }
                });

                scope.$watch(attrs.ngModel, value =>
                {
                    elem.slider('value', value);
                });

            }
        };
    };

    export function Cron(): ng.IDirective {
        return {
            restrict: 'A',
            link: (scope: ng.IScope, element: any, attrs: any, model: ng.INgModelController) => {
                var elem = element.cronGen();
            }
        };
    };

    export function Select(): ng.IDirective {
        return {
            require: 'ngModel', 
            restrict: 'A',
            link: (scope: ng.IScope, element: JQuery, attrs: any, model: ng.INgModelController) => {

                var elem = element.selectpicker({
                    style: attrs.selectStyle || '',
                    size: parseInt(attrs.selectSize || 4)
                });
                scope.$watch(attrs.ngModel, (newVal, oldVal) => {
                    scope.$evalAsync(() => {
                        if (!attrs.ngOptions || /track by/.test(attrs.ngOptions)) element.val(newVal);
                        element.selectpicker('refresh');
                    });
                });
                
                scope.$watch(attrs.ngDisabled, (newVal, oldVal) => {
                    if (newVal) {
                        element.attr('disabled', 'disabled');
                    } else {
                        element.removeAttr('disabled');
                    }
                    element.selectpicker('refresh');
                });


                model.$render = () => {
                    scope.$evalAsync(() => {
                        element.selectpicker('refresh');
                    });
                }
            }
        };
    };
} 