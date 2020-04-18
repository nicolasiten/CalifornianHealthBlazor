(function ($) {

    var dotNetReference = null;

    window.blazorHelpers = {
        init: (reference) => {
            dotNetReference = reference;
        },

        scrollToFragment: (elementId) => {
            var element = document.getElementById(elementId);

            if (element) {
                element.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        },

        renderCalendar: (dotNetReference) => {
            $('#evoCalendar').evoCalendar({
                todayHighlight: true,
                sidebarToggler: true,
                eventListToggler: true,
                canAddEvent: false,
                calendarEvents: [

                ],
                onSelectDate: function (sender) {
                    var date = sender.currentTarget.getAttribute("date-formatted-val");
                    dotNetReference.invokeMethodAsync('UpdateAvailableAppointments', date);
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
                dotNetReference.invokeMethodAsync('UpdateTime', selectedTime);
            }
        }
    };

})(jQuery);
