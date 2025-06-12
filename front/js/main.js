document.addEventListener('DOMContentLoaded', function () {
    // Перевіряємо, чи авторизований користувач
    const authToken = localStorage.getItem('authToken');
    if (authToken) {
        // Якщо токен є, перенаправляємо на сторінку замовлень
        window.location.href = 'pages/orders.html';
    }

    // Обробляємо форму входу
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', async function (e) {
            e.preventDefault();

            const username = document.getElementById('username').value;
            const password = document.getElementById('password').value;

            try {
                const response = await api.login(username, password);
                localStorage.setItem('authToken', response.token);
                localStorage.setItem('userRole', response.role);
                window.location.href = 'pages/orders.html';
            } catch (error) {
                alert('Помилка входу: ' + error.message);
            }
        });
    }

    // Обробка перемикання розділів в адмін-панелі
    const navItems = document.querySelectorAll('.admin-nav li');
    const sections = document.querySelectorAll('.admin-section');

    if (navItems.length > 0 && sections.length > 0) {
        navItems.forEach(item => {
            item.addEventListener('click', () => {
                // Прибираємо active у всіх
                navItems.forEach(nav => nav.classList.remove('active'));
                sections.forEach(section => section.classList.remove('active'));

                // Додаємо active до вибраного
                item.classList.add('active');
                const sectionId = item.getAttribute('data-section') + 'Section';
                document.getElementById(sectionId).classList.add('active');
            });
        });
    }
    const Items = document.querySelectorAll('.category-list li');

    if (Items.length > 0) {
        Items.forEach(item => {
            item.addEventListener('click', () => {
                // Прибираємо active у всіх
                Items.forEach(nav => nav.classList.remove('active'));

                // Додаємо active до вибраного
                item.classList.add('active');
            })
        });
    }
});