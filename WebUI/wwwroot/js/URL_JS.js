// Отримання HTML-коду з іншої сторінки
var xhr = new XMLHttpRequest();
xhr.open('GET', 'POS.html', true);
xhr.onreadystatechange = function() {
  if (this.readyState !== 4) return;
  if (this.status !== 200) return; // Обробляємо помилки
  var html = this.responseText;
  // Вставлення HTML-коду в елемент з ID "target"
  document.getElementById('target').innerHTML = html;
};
xhr.send();