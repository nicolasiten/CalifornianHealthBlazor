(function($) {
    
    $(".navbar-collapse a").on('click', function() {
        $(".navbar-collapse.collapse").removeClass('in');
    });

    //jQuery to collapse the navbar on scroll
    $(window).scroll(function() {
        if ($(".navbar-default").offset().top > 50) {
          $(".navbar-fixed-top").addClass("top-nav-collapse");
        } else {
          $(".navbar-fixed-top").removeClass("top-nav-collapse");
        }
    });

    $(document).on('click', '.appointment-badge', function (event) {
        $(".appointment-badge.active").removeClass("active");
        var target = $(event.target);
        target.addClass('active');
        console.log(target[0].innerText);
    });

})(jQuery);
