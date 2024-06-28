document.addEventListener('DOMContentLoaded', function () {
    // Ваша проверка возраста здесь, например, если пользователь имеет 18 лет
    let isOver18 = true; // Замените на логику проверки возраста

    if (isOver18) {
        // Добавляем класс fadeIn ко всем элементам с указанным классом
        const elements = document.querySelectorAll('.fadeIn');
        elements.forEach(el => {
            el.style.opacity = 0; // Начальная прозрачность элемента
            el.style.animationDelay = `${Math.random() * 0.5}s`; // Случайная задержка для каждого элемента
            el.classList.add('animated', 'fadeIn');
        });
    }
});
