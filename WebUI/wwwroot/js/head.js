window.addEventListener('scroll', function() {
    const header = document.querySelector('header');
    if (window.scrollY > 50) { // Змінюйте значення за потреби
        header.classList.add('scrolled');
    } else {
        header.classList.remove('scrolled');
    }
});