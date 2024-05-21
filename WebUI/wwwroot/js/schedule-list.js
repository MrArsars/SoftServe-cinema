   // Отримати поточну дату
    var currentDate = new Date();

    // Отримати початкову дату (сьогодні)
    var startDate = currentDate.getDate();
    var startMonth = currentDate.getMonth() + 1; // Місяці в JavaScript починаються з 0
    var startDateFormatted = ('0' + startDate).slice(-2) + '.' + ('0' + startMonth).slice(-2); // Форматувати дату в потрібний формат

    // Отримати список кнопок розкладу
    var scheduleList = document.getElementById('schedule-list');

    // Додати кнопки для наступних днів
    for (var i = 0; i < 7; i++) 
    {
    var nextDate = new Date();
    nextDate.setDate(startDate + i);
    var nextDay = nextDate.getDate();
    var nextMonth = nextDate.getMonth() + 1; // Місяці в JavaScript починаються з 0
    var nextDateFormatted = ('0' + nextDay).slice(-2) + '.' + ('0' + nextMonth).slice(-2); // Форматувати дату в потрібний формат

    var li = document.createElement('li');
    li.className = 'schedule__item';
    var button = document.createElement('button');
    button.type = 'button';
    button.className = 'schedule__btn';
    button.dataset.filter = '.date_' + nextDateFormatted.replace('.', ''); // Додати клас з датою без крапки
    button.textContent = getDayOfWeek(nextDate.getDay()) + ', ' + nextDateFormatted; // Отримати назву дня тижня та дату
    li.appendChild(button);
    scheduleList.appendChild(li);
    }

    // Функція для отримання назви дня тижня
    function getDayOfWeek(dayIndex) 
    {
    var days = ['Нд', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'];
    return days[dayIndex];
    }