﻿window.blazorHelpers = {
    scrollToFragment: (elementId) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({
                behavior: 'smooth'
            });
        }
    }
};