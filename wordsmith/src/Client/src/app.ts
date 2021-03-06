import {Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';
import * as $ from "jquery";
export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Wordsmith Inc';
    config.map([
      { route: ['', 'start'], name: 'start',      moduleId: './templates/start',      nav: true },
    ]);

    this.router = router;
  }

  attached() {
      // jQuery for page scrolling feature - requires jQuery Easing plugin
      $('a.page-scroll').bind('click', function (event) {
          var $anchor = $(this);
          $('html, body').stop().animate({
              scrollTop: ($($anchor.attr('href')).offset().top - 50)
          }, 1250, 'easeInOutExpo');
          event.preventDefault();
      });

      // Highlight the top nav as scrolling occurs
      $('body').scrollspy({
          target: '.navbar-fixed-top',
          offset: 51
      });

      // Closes the Responsive Menu on Menu Item Click
      $('.navbar-collapse ul li a').click(function () {
          $('.navbar-toggle:visible').click();
      });

      // Offset for Main Navigation
      $('#mainNav').affix({
          offset: {
              top: 100
          }
      })

      // Initialize and Configure Scroll Reveal Animation
      
      ScrollReveal = ScrollReveal();
      ScrollReveal.reveal('.sr-icons', {
          duration: 600,
          scale: 0.3,
          distance: '0px'
      }, 200);
      ScrollReveal.reveal('.sr-button', {
          duration: 1000,
          delay: 200
      });
      ScrollReveal.reveal('.sr-contact', {
          duration: 600,
          scale: 0.3,
          distance: '0px'
      }, 300);

     
  }
}
