$(document).ready(function() {
    // Додамо обробник подій для кліків на посиланнях категорій
    $('.category-link').click(function(event) {
      // Зупиняємо стандартну дію посилання
      event.preventDefault();
      // Отримуємо значення атрибуту data-category з натиснутого посилання
      var category = $(this).data('category');
      // Робимо AJAX запит
      $.ajax({
        url: 'php/get_articles.php',
        method: 'POST',
        dataType: 'json',
        data: { category: category },
        success: function(response) {
          // Вставляємо отримані статті в відповідний блок
          $('#articles-container').html(response.articles);
        },
        error: function(jqXHR, textStatus, errorThrown) {
          // Виводимо повідомлення про помилку
          alert('Помилка завантаження статей!');
        }
      });
    });
  });