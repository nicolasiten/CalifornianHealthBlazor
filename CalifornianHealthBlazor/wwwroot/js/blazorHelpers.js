(function ($) {

    window.blazorHelpers = {
        scrollToFragment: (elementId) => {
            var element = document.getElementById(elementId);

            if (element) {
                element.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        },

        renderCalendar: () => {
            $('#evoCalendar').evoCalendar({
                todayHighlight: true,
                sidebarToggler: true,
                eventListToggler: true,
                eventDisplayDefault: false,
                canAddEvent: true,
                calendarEvents: [

                ],
                onSelectDate: function (sender) {
                    var date = sender.currentTarget.getAttribute("date-val");
                    DotNet.invokeMethodAsync('CalifornianHealthBlazor', 'DayClicked', date);
                },
                onAddEvent: function () {
                    console.log('onAddEvent!');
                }
            });
        }
    };

})(jQuery);
