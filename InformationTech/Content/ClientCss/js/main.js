/*-----------------------------------------------------------------------------------

  Template Name: Educan Education HTML Template.
  Template URI: #
  Description: Educan is a unique website template designed in HTML with a simple & beautiful look. There is an excellent solution for creating clean, wonderful and trending material design corporate, corporate any other purposes websites.
  Author: HasTech
  Author URI: https://themeforest.net/user/hastech/portfolio
  Version: 1.0
-----------------------------------------------------------------------------------*/

/*-------------------------------
[  Table of contents  ]
---------------------------------
  01. jQuery MeanMenu
  02. wow js active
  03. Portfolio  Masonry (width)
  04. Sticky Header
  05. ScrollUp
  06. Start  Site Info
  07. Search Bar
  08. CounterUp
  09. Countdown
  10. Testimonial Slick Carousel
  11. Testimonial Slick Carousel As Nav
  12. Home Slider
  13. Popular Courses Wrap
  14. Upcoming Event Activation
  15.  Plus Minus Button


  
/*--------------------------------
[ End table content ]
-----------------------------------*/


(function($) {
    'use strict';


/*-------------------------------------------
  01. jQuery MeanMenu
--------------------------------------------- */
    
$('.mobile-menu nav').meanmenu({
      meanMenuContainer: '.mobile-menu-area',
      meanScreenWidth: "991",
      meanRevealPosition: "right",
    });

/*-------------------------------------------
  02. wow js active
--------------------------------------------- */
    new WOW().init();

/*-------------------------------------------
  03. Portfolio  Masonry (width)
--------------------------------------------- */ 
  $('.ml-portfolio-page').imagesLoaded( function() {
        // filter items on button click
        $('#ml-filters').on('click', 'li', function () {
            var filterValue = $(this).attr('data-filter');
            $containerpage.isotope({ filter: filterValue });
        });
        // change is-checked class on buttons
        $('#ml-filters li').on('click', function () {
            $('#ml-filters li').removeClass('is-checked');
            $(this).addClass('is-checked');
            var selector = $(this).attr('data-filter');
            $containerpage.isotope({ filter: selector });
            return false;
        });
        var $containerpage = $('.ml-portfolio-page');
        $containerpage.isotope({
            itemSelector: '.pro-item',
            filter: '*',
            transitionDuration: '0.7s',
            masonry: {
                columnWidth: '.pro-item'
            }
          });
      });



/*-------------------------------------------
  04. Sticky Header
--------------------------------------------- */ 
  $(window).on('scroll',function() {    
     var scroll = $(window).scrollTop();
     if (scroll < 245) {
      $("#sticky-header-with-topbar").removeClass("scroll-header");
     }else{
      $("#sticky-header-with-topbar").addClass("scroll-header");
     }
    });


/*--------------------------
  05. ScrollUp
---------------------------- */
$.scrollUp({
    scrollText: '<i class="icon ion-chevron-up"></i>',
    easingType: 'linear',
    scrollSpeed: 900,
    animation: 'fade'
});


/*------------------------------------    
  06. Start  Site Info
--------------------------------------*/ 

  $('.toggle-menu').on('click', function(){
      if($(this).siblings('.site-info-wrap').hasClass('active')){

          $(this).siblings('.site-info-wrap').removeClass('active').slideUp();
          $(this).removeClass('active');

          if ( $(this).find("i").hasClass('zmdi-menu')){
              $(this).find("i").removeClass('zmdi-menu').addClass('zmdi-close');
            }else{
              $(this).find("i").removeClass('zmdi-close').addClass('zmdi-menu');
            }

      }
      else{
          $('.toggle-menu .site-info-wrap').removeClass('active').slideUp();
          $('.toggle-menu .site-info-wrap').removeClass('active');
          $(this).addClass('active');
          $(this).siblings('.site-info-wrap').addClass('active').slideDown();

          if ( $(this).find("i").hasClass('zmdi-menu')){
              $(this).find("i").removeClass('zmdi-menu').addClass('zmdi-close');
            }
      }
  });

  $('.icon-clear').on('click', function(){
      $('.site-info-wrap').removeClass('active').slideUp();
      $('.toggle-menu').find("i").removeClass('zmdi-close').addClass('zmdi-menu');
  });





/*------------------------------------    
  07. Search Bar
--------------------------------------*/ 
    
  $( '.search__open' ).on( 'click', function () {
    $( 'body' ).toggleClass( 'search__box__show__hide' );
    return false;
  });

  $( '.search__close__btn .search__close__btn_icon' ).on( 'click', function () {
    $( 'body' ).toggleClass( 'search__box__show__hide' );
    return false;
  });




/*-----------------------------
  08. CounterUp
-----------------------------*/
  $('.count').counterUp({
    delay: 60,
    time: 3000
  });




/*---------------------------
  09. Countdown
-----------------------------*/
$('[data-countdown]').each(function() {
        var $this = $(this),
            finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function(event) {
            $this.html(event.strftime('<span class="medilearn-count days"><span class="count-inner"><span class="time-count">%-D</span> <p>Days</p></span></span> : <span class="medilearn-count hour"><span class="count-inner"><span class="time-count">%-H</span> <p>Hours</p></span></span> : <span class="medilearn-count minutes"><span class="count-inner"><span class="time-count">%M</span> <p>Minutes</p></span></span> : <span class="medilearn-count second"><span class="count-inner"><span class="time-count">%S</span> <p>Seconds</p></span></span>'));
        });
    });




/*--------------------------------
  10. Testimonial Slick Carousel
-----------------------------------*/
    $('.testimonial-text-slider').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        draggable: false,
        fade: true,
        asNavFor: '.slider-nav',
        responsive: [
            {
              breakpoint: 600,
              settings: {
                dots: false,
                slidesToShow: 1,
                slidesToScroll: 1,  
                centerPadding: '0px',
                }
            },
            {
              breakpoint: 320,
              settings: {
                autoplay: false,
                dots: false,
                slidesToShow: 1,
                slidesToScroll: 1,
                centerMode: false,
                focusOnSelect: false,
                }
            }
        ]
    });
/*---------------------------------------
  11. Testimonial Slick Carousel As Nav
-----------------------------------------*/
    $('.testimonial-image-slider').slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        asNavFor: '.testimonial-text-slider',
        dots: true,
        arrows: false,
        centerMode: true,
        focusOnSelect: true,
        centerPadding: '10px',
        responsive: [
            {
              breakpoint: 600,
              settings: {
                dots: true,
                slidesToShow: 1,
                slidesToScroll: 1,  
                centerPadding: '10px',
                }
            },
            {
              breakpoint: 320,
              settings: {
                autoplay: true,
                dots: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                centerMode: false,
                focusOnSelect: false,
                }
            }
        ]
    });


/*-----------------------------------------------
  12. Home Slider
-------------------------------------------------*/

  if ($('.slider__activation__wrap').length) {
    $('.slider__activation__wrap').owlCarousel({
      loop: true,
      margin:0,
      nav:true,
      autoplay: false,
      navText: [ '<i class="icon ion-ios-arrow-left"></i>', '<i class="icon ion-ios-arrow-right"></i>' ],
      autoplayTimeout: 10000,
      items:1,
      dots: false,
      lazyLoad: true,
      responsive:{
        0:{
          items:1
        },
        600:{
          items:1
        },
        800:{
          items:1
        },
        1024:{
          items:1
        },
        1200:{
          items:1
        },
        1400:{
          items:1
        },
        1920:{
          items:1
        }
      }
    });
  }


/*-----------------------------------------------
  13. Popular Courses Wrap
-------------------------------------------------*/


  $(".popular__courses__wrap").owlCarousel({
      loop:true,
      margin:0,
      nav:true,
      autoplay: true,
      navText: [ '<i class="icon ion-ios-arrow-left"></i>', '<i class="icon ion-ios-arrow-right"></i>' ],
      autoplayTimeout: 10000,
      items:3,
      dots: false,
      lazyLoad: true,
      responsive:{
        0:{
          items:1
        },
        600:{
          items:2
        },
        800:{
          items:2
        },
        1024:{
          items:3
        },
        1200:{
          items:3
        },
        1400:{
          items:3
        },
        1920:{
          items:3
        }
      }
    });


/*-----------------------------------------------
  14. Upcoming Event Activation
-------------------------------------------------*/


  $(".upcoming__owl__activation").owlCarousel({
      loop:true,
      margin:0,
      nav:false,
      autoplay: true,
      autoplayTimeout: 10000,
      items:1,
      dots: true,
      lazyLoad: true,
      responsive:{
        0:{
          items:1
        },
        600:{
          items:1
        },
        800:{
          items:1
        },
        1024:{
          items:1
        },
        1200:{
          items:1
        },
        1400:{
          items:1
        },
        1920:{
          items:1
        }
      }
    });





/*-------------------------------
  15.  Plus Minus Button 
--------------------------------*/


    $(".cart-plus-minus").append('<div class="dec qtybutton">-</i></div><div class="inc qtybutton">+</div>');

    $(".qtybutton").on("click", function () {
        var $button = $(this);
        var oldValue = $button.parent().find("input").val();
        if ($button.text() == "+") {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        $button.parent().find("input").val(newVal);
    });








})(jQuery);




