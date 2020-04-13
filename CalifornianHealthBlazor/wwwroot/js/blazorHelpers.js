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
                canAddEvent: false,
                calendarEvents: [

                ],
                onSelectDate: function (sender) {
                    var date = sender.currentTarget.getAttribute("date-formatted-val");
                    DotNet.invokeMethodAsync('CalifornianHealthBlazor', 'UpdateAvailableAppointmentsCaller', date);
                }
            });
        },

        updateAvailableAppointments: (availableAppointments) => {
            $('.appointment-badge').remove();

            for (var i = 0; i < availableAppointments.length; i++) {
                var appointmentNode = '<span class="badge appointment-badge">' + availableAppointments[i] + '</span>';
                $(".event-section").append(appointmentNode);
            };
        },

        selectedTimeChanged: (selectedTime) => {
            if (selectedTime != null) {
                DotNet.invokeMethodAsync('CalifornianHealthBlazor', 'UpdateTimeCaller', selectedTime);
            }
        }
    };

})(jQuery);
