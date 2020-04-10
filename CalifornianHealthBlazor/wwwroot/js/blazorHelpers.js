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
                onSelectDate: function () {
                    console.log('onSelectDate!');
                },
                onAddEvent: function () {
                    console.log('onAddEvent!');
                }
            });
        }
    };

})(jQuery);
