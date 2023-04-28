jQuery(function($) {
    'use strict';
    (function() {
        $(window).on('load', function() {

            /*
            =========================================================================================
            1. Spinner 
            =========================================================================================
            */
            $(".spinner_hol").fadeOut("slow");



            /*
            =========================================================================================
            2.PARALLAX
            =========================================================================================
            */

            parallaxInit();

            function parallaxInit() {
                
                $('.home-parallax').parallax("30%", 0.1);
                $('.testimonial-parallax').parallax("30%", 0.1);
                $('.twitter-parallax').parallax("30%", 0.1);
                /*add as necessary*/
            }
        });
    }());



    (function() {
        $(window).on('scroll', function() {

            /*
            =========================================================================================
            3. NAVBAR 
            =========================================================================================
            */
            if ($(window).scrollTop() > 80) {
                $(".navbar-nav>li>a, .navbar-brand").css({
                    'padding-top': '30px',
                    'padding-bottom': '30px'
                });
                $(".navbar-default").css({
                    'background-color': '#ffffff',
                    'border-bottom': '1px solid #e4e3e3'
                });
                $(".navbar-default").css({
                    'margin-top': '0px'
                });
            } else {
                $(".navbar-nav>li>a, .navbar-brand").css({
                    'padding-top': '60px',
                    'padding-bottom': '30px'
                });
                $(".navbar-default").css({
                    'background-color': 'rgba(59, 59, 59, 0)',
                    'border-bottom': '0px solid #ddd'
                });
            }
            $(".page").each(function() {
                var bb = $(this).attr("id");
                var hei = $(this).outerHeight();
                var grttop = $(this).offset().top - 80;
                if ($(window).scrollTop() > grttop - 1 && $(window).scrollTop() < grttop + hei - 1) {
                    var uu = $(".navbar-nav>li>a[href='#" + bb + "']").parent().addClass("active");
                } else {
                    var uu = $(".navbar-nav>li>a[href='#" + bb + "']").parent().removeClass("active");
                }
            });



            /*
            =========================================================================================
            4. PROGRESS BAR
            =========================================================================================
            */

            $(".prog_cint .tab-pane.fade .progress_cont").each(function() {
                var base = $(this);
                var windowHeight = $(window).height();
                var itemPos = base.offset().top;
                var scrollpos = $(window).scrollTop() + windowHeight - 100;
                if (itemPos <= scrollpos) {
                    var auptcoun = base.find(".progress-bar").attr("aria-valuenow");
                    base.find(".progress-bar").animate({
                        width: auptcoun + "%"
                    }, 1500);
                    var str = base.find(".skill>span").text();
                    var res = str.replace("%", "");
                    if (res == 0) {
                        $({
                            countNumber: 0
                        }).animate({
                            countNumber: auptcoun
                        }, {
                            duration: 1500,
                            easing: 'linear',
                            step: function() {
                                base.find(".skill>span").text(Math.ceil(this.countNumber) + "%");
                            }
                        });
                    }
                }
            });
        });
    }());


    (function() {

        /*
        =========================================================================================
        5.NAV MENU FIX  
        =========================================================================================
        */
        $(".navbar-nav>li>a,  .about_b").click(function() {
            $(this).parent().addClass("active");
            $(".navbar-nav li a").not(this).parent().removeClass("active");
            var TargetId = $(this).attr('href');
            $('html, body').animate({
                scrollTop: $(TargetId).offset().top - 50
            }, 1000, 'swing');
            return false;
        });



        /*
        =========================================================================================
        6. FIX HOME SCREEN HEIGHT  
        =========================================================================================
        */

        setInterval(function() {
                var widnowHeight = $(window).height();
                var sliderHeight = $(".slider").height();
                var padTop = widnowHeight - sliderHeight;
                $(".slider").css({
                    'padding-top': Math.round(padTop / 2) + 'px',
                    'padding-bottom': Math.round(padTop / 2) + 'px'
                });
            }, 10);



        /*
        =========================================================================================
        7. FEATURED PROJECT 
        =========================================================================================
        */

        var owl = $(".fe_project");
        owl.owlCarousel({
            pagination: false,
            items: 6, //6 items above 1000px browser width
            itemsDesktop: [1366, 4], //4 items between 1366px and 901px
            itemsDesktopSmall: [900, 3], // betweem 900px and 601p
            itemsTablet: [600, 2], //2 items between 600 and 0
            itemsMobile: [480, 1] // itemsMobile disabled - inherit from itemsTablet option
        });



        /*
        =========================================================================================
        8. TESTIMONIAL 
        =========================================================================================
        */

        var clients_logo_owl = $(".qutoSilder");
        clients_logo_owl.owlCarousel({
            pagination: true,
            paginationNumbers: true,
            items: 1, //10 items above 1000px browser width
            itemsDesktop: [1000, 1], //5 items between 1000px and 901px
            itemsDesktopSmall: [900, 1], // betweem 900px and 601px
            itemsTablet: [600, 1], //2 items between 600 and 0
            itemsMobile: [480, 1]
        });



        /*
        =========================================================================================
        9. PORTFOLIO SECTION
        =========================================================================================
        */

        $("#second").bootFolio({
            bfLayout: "bflayhover",
            bfFullWidth: "full-width",
            bfHover: "bfhoverCrafty",
            bfAnimation: "scale",
            bfSpace: "nospace",
            bfAniDuration: 500,
            bfCaptioncolor: "rgba(0, 0, 0, 0.72)",
            bfTextcolor: "#ffffff",
            bfTextalign: "center",
            bfNavalign: "center"
        });
        $("a[data-rel^='prettyPhoto']").prettyPhoto();



        /*
        =========================================================================================
        10. PROCESS SECTION
        =========================================================================================
        */

        var clients_logo_owl = $(".process_slider");
        clients_logo_owl.owlCarousel({
            pagination: false,
            items: 1, //10 items above 1000px browser width
            itemsDesktop: [1000, 1], //5 items between 1000px and 901px
            itemsDesktopSmall: [900, 1], // betweem 900px and 601px
            itemsTablet: [600, 1], //2 items between 600 and 0
            itemsMobile: [480, 1]
        });



        /*
        =========================================================================================
        11. TWITTER FEED 
        =========================================================================================
        */

        $('.tweet').twittie({
            dateFormat: '%b. %d, %Y',
            template: '{{tweet}} <span class="time">{{date}}</span>',
            count: 5,
            loadingText: 'Loading!'
        });
        //    TWITTER SLIDER   
        var bb = setInterval(function() {
            var bbb = $(".tweet").find('.tw_slider').text();
            if (!bbb) {} else {
                clearInterval(bb);
                $('.tw_slider').owlCarousel({
                    autoplay: true,
                    pagination: true,
                    paginationNumbers: true,
                    items: 1, //10 items above 1000px browser width
                    itemsDesktop: [1000, 1], //5 items between 1000px and 901px
                    itemsDesktopSmall: [900, 1], // betweem 900px and 601px
                    itemsTablet: [600, 1], //2 items between 600 and 0
                    itemsMobile: [480, 1]
                });
                $(".tw_slider .owl-dots .owl-dot").each(function() {
                    $(this).find("span").text($(this).index() + 1);
                });
            }
        }, 100);




        /*
        =========================================================================================
        12. OUR CLENT  
        =========================================================================================
        */
        
        var clients_logo_owl = $(".clients_logo");
        clients_logo_owl.owlCarousel({
            pagination: false,
            items: 4, //4 items above 1000px browser width
            itemsDesktop: [1000, 3], //3 items between 1000px and 901px
            itemsDesktopSmall: [900, 3], //3 betweem 900px and 601px
            itemsTablet: [600, 2], //2 items between 600 and 0
            itemsMobile: [480, 1],
            margin: 60
        });



        /*
        =========================================================================================
        13. CONTACT  FORM VALIDATION   
        =========================================================================================
        */

        $("#Name").keyup(function() {
            "use strict";
            var value = $(this).val();
            if (value.length > 1) {
                $(this).parent().find(".error_message").remove();
                $(this).css({
                    "border": "1px solid rgba(252, 252, 252, 0.35)"
                })
            } else {
                $(this).parent().find(".error_message").remove();
                $(this).parent().append("<div class='error_message'>Name value should be at least 2</div>");
            }
        });
        $("#Email").keyup(function() {
            "use strict";
            var value = $(this).val();
            var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
            if (testEmail.test(value)) {
                $(this).parent().find(".error_message").remove();
                $(this).css({
                    "border": "1px solid rgba(252, 252, 252, 0.35)"
                })
            } else {
                $(this).parent().find(".error_message").remove();
                $(this).parent().append("<div class='error_message'>Please entire a valid email. </div>");
            }
        });
        $("#contact_submit").click(function() {
            "use strict";
            var nameValue = $("#Name").val();
            if (!nameValue.length) {
                $("#Name").css({
                    "border": "1px solid red"
                });
                $("#Name").parent().find(".error_message").remove();
                $("#Name").parent().append("<div class='error_message'>Name is required</div>");
                return false;
            }
            if (nameValue.length < 1) {
                $("#Name").css({
                    "border": "1px solid red"
                });
                $("#Name").parent().find(".error_message").remove();
                $("#Name").parent().append("<div class='error_message'>Name value should be at least 2</div>").show();
                return false;
            }
            var emailValue = $("#Email").val();
            var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
            if (!emailValue) {
                $("#Email").css({
                    "border": "1px solid red"
                });
                $("#Email").parent().find(".error_message").remove();
                $("#Email").parent().append("<div class='error_message'>Email is required</div>").show();
                return false;
            }
            if (!testEmail.test(emailValue)) {
                $("#Email").css({
                    "border": "1px solid red"
                });
                $("#Email").parent().find(".error_message").remove();
                $("#Email").parent().append("<div class='error_message'>Please entire a valid email.</div>").show();
                return false;
            }
            var phoneValue = $("#Phone").val();
            var messageValue = $("#Message").val();
            if (nameValue && emailValue) {
                $(".feedback_box").slideDown();
                $.ajax({
                    url: 'mail/mail.php',
                    data: {
                        name: nameValue,
                        email: emailValue,
                        phone: phoneValue,
                        message: messageValue
                    },
                    type: 'POST',
                    success: function(result) {
                        "use strict";
                        $(".show_result").append("<div class='result_message'>" + result + "</div>");
                        $(".result_message").slideDown();
                        $("#Name").val("");
                        $("#Email").val("");
                        $("#Phone").val("");
                        $("#Message").val("");
                    }
                });
            }
            return false;
        });



        /*
        =========================================================================================
        14. MAILCHIMP
        =========================================================================================
        */

        $('#mc-form').ajaxChimp({
            url: "https://kutechx.us5.list-manage.com/subscribe/post?u=844ebbd89dd6911195e21dee5&amp;id=7a031b8255"
        });



        /*
        =========================================================================================
        15. WOW JS
        =========================================================================================
        */




        /*
        =========================================================================================
        16. COUNTER
        =========================================================================================
        */

        $('.number>span').counterUp({
            delay: 10,
            time: 2000
        });



        /*
        =========================================================================================
        17. GOOGLE MAP
        =========================================================================================
        */

        google.maps.event.addDomListener(window, 'load', init);

        function init() {
            var mapOptions = {
                scrollwheel: false,
                zoom: 16,
                center: new google.maps.LatLng(23.710551, 90.415643), // New York
                styles: [{
                    featureType: "landscape",
                    stylers: [{
                        saturation: -100
                    }, {
                        lightness: 65
                    }, {
                        visibility: "on"
                    }]
                }, {
                    featureType: "poi",
                    stylers: [{
                        saturation: -100
                    }, {
                        lightness: 51
                    }, {
                        visibility: "simplified"
                    }]
                }, {
                    featureType: "road.highway",
                    stylers: [{
                        saturation: -100
                    }, {
                        visibility: "simplified"
                    }]
                }, {
                    featureType: "road.arterial",
                    stylers: [{
                        saturation: -100
                    }, {
                        lightness: 30
                    }, {
                        visibility: "on"
                    }]
                }, {
                    featureType: "road.local",
                    stylers: [{
                        saturation: -100
                    }, {
                        lightness: 40
                    }, {
                        visibility: "on"
                    }]
                }, {
                    featureType: "transit",
                    stylers: [{
                        saturation: -100
                    }, {
                        visibility: "simplified"
                    }]
                }, {
                    featureType: "administrative.province",
                    stylers: [{
                        visibility: "off"
                    }] /**/
                }, {
                    featureType: "administrative.locality",
                    stylers: [{
                        visibility: "off"
                    }]
                }, {
                    featureType: "administrative.neighborhood",
                    stylers: [{
                        visibility: "on"
                    }] /**/
                }, {
                    featureType: "water",
                    elementType: "labels",
                    stylers: [{
                        visibility: "on"
                    }, {
                        lightness: -25
                    }, {
                        saturation: -100
                    }]
                }, {
                    featureType: "water",
                    elementType: "geometry",
                    stylers: [{
                        hue: "#ffff00"
                    }, {
                        lightness: -25
                    }, {
                        saturation: -97
                    }]
                }]
            };
            var contentString = '<div id="mapcontent">' + '<p>Yup, we are here!</p></div>';
            var infowindow = new google.maps.InfoWindow({
                maxWidth: 320,
                content: contentString
            });
            var mapElement = document.getElementById('map');
            var map = new google.maps.Map(mapElement, mapOptions);
            var image = new google.maps.MarkerImage('images/pin.png', null, null, null, new google.maps.Size(50, 71))
            var myLatLng = new google.maps.LatLng(23.710551, 90.415643);
            var marker = new google.maps.Marker({
                position: myLatLng,
                map: map,
                icon: image,
                title: 'Code Cafe'
            });
            google.maps.event.addListener(marker, 'click', function() {
                infowindow.open(map, marker);
            });
        }
    }());
});