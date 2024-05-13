// JavaScript-код для показу анімації та плавного переходу до основного контенту
document.addEventListener('DOMContentLoaded', function() {
    // показуємо анімацію
    document.getElementById('preloader').style.opacity = 1;
  
    // приховуємо основний контент
    document.getElementById('content').style.opacity = 0;
  
    // після 3 секунд приховуємо заглушку та показуємо основний контент
    setTimeout(function() {
      document.getElementById('preloader').style.opacity = 0;
      document.getElementById('content').style.opacity = 1;
    }, 3000);
  });