document.addEventListener('DOMContentLoaded', function() {
  const tabs = document.querySelectorAll('.tab');
  const tabContents = document.querySelectorAll('.tab-content');

  tabs.forEach(tab => {
    tab.addEventListener('click', () => {
      // Закрити всі вмістові блоки
      tabContents.forEach(content => {
        content.classList.remove('active');
      });

      // Отримати ідентифікатор вкладки з атрибуту data-tab
      const tabId = tab.getAttribute('data-tab');

      // Показати вміст, пов'язаний з обраною вкладкою
      const activeContent = document.querySelector(`.tab-content[data-tab="${tabId}"]`);
      if (activeContent) {
        activeContent.classList.add('active');
      }
    });
  });
});