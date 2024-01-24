$(document).ready(function () {
    // Active Pages 
    $(".menu li a").click(function () {
        if ($(".menu li").hasClass("active")) {
            $(".menu li").removeClass("active");
        }
        $(this).parents("li").addClass("active");
    });
    $('.modal').on('shown.bs.modal', function () {
        $(this).find('[autofocus]').focus();
    });
    // Check InActivity
    (function () {
        let idleDurationSecs = 2400, idleTimeout;
        let resetIdleTimeout = function () {
            if (idleTimeout) clearTimeout(idleTimeout);
            idleTimeout = setTimeout(function () {
                alert("Hết thời gian chờ do không hoạt động trong thời gian dài !");
                location.href = "/admin/dang-xuat";
            }, idleDurationSecs * 1000);
        };
        resetIdleTimeout();
        ['click', 'touchstart', 'mousemove'].forEach(function (evt) {
            document.addEventListener(evt, resetIdleTimeout, false);
        });
    })();

});