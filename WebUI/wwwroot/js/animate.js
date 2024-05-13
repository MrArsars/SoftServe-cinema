window.onload = function() {
    var logo = document.querySelector('.sprite');
    var siteTitle = document.querySelector('.site-title');
    logo.style.opacity = 0;
    siteTitle.style.opacity = 0;
    var animation = logo.animate([{opacity: 0}, {opacity: 1}], 1500);
    animation.onfinish = function() {
      siteTitle.animate([{opacity: 0}, {opacity: 1}], 1000);
  
      setTimeout(function() {
        siteTitle.style.opacity = 1;
        main.style.display = 'block';
        document.body.style.overflow = 'auto';
      }, 1000);
    }
  }