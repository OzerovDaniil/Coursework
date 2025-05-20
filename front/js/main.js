// Ждем загрузки DOM
document.addEventListener('DOMContentLoaded', function() {
    // Проверяем, авторизован ли пользователь и его роль
    const authToken = localStorage.getItem('authToken');
    const userRole = localStorage.getItem('userRole');
    const adminTab = document.getElementById('admin-tab');

    // Управление видимостью вкладки "Администрирование"
    if (adminTab) {
        if (authToken && userRole === 'admin') {
            adminTab.style.display = 'inline'; // Показываем вкладку для админа
        } else {
            adminTab.style.display = 'none'; // Скрываем для обычных пользователей
        }
    }

    // Если пользователь авторизован, перенаправляем на страницу заказов
    if (authToken) {
        window.location.href = 'pages/orders.html';
    }

    // Обрабатываем форму входа
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', async function(e) {
            e.preventDefault();

            const username = document.getElementById('username').value;
            const password = document.getElementById('password').value;

            try {
                const response = await api.login(username, password);
                localStorage.setItem('authToken', response.token);
                localStorage.setItem('userRole', response.role);
                // Обновляем видимость вкладки после входа
                if (adminTab && response.role === 'admin') {
                    adminTab.style.display = 'inline';
                }
                window.location.href = 'pages/orders.html';
            } catch (error) {
                alert('Ошибка входа: ' + error.message);
            }
        });
    }

    // Обрабатываем форму регистрации
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        registerForm.addEventListener('submit', async function(e) {
            e.preventDefault();

            const username = document.getElementById('username').value;
            const firstName = document.getElementById('firstName').value;
            const lastName = document.getElementById('lastName').value;
            const email = document.getElementById('email').value;
            const phone = document.getElementById('phone').value;
            const password = document.getElementById('password').value;
            const role = document.getElementById('role').value; // Получаем роль из формы

            const userData = {
                username,
                firstName,
                lastName,
                email,
                phone,
                password,
                role // Отправляем выбранную роль на бэкенд
            };

            try {
                const response = await api.register(userData);
                localStorage.setItem('authToken', response.token);
                localStorage.setItem('userRole', response.role);
                // Обновляем видимость вкладки после регистрации
                if (adminTab && response.role === 'admin') {
                    adminTab.style.display = 'inline';
                }
                // Перенаправляем на страницу входа после регистрации
                window.location.href = '../index.html';
            } catch (error) {
                alert('Ошибка регистрации: ' + error.message);
            }
        });
    }

    // Обработка переключения разделов в админ-панели
    const navItems = document.querySelectorAll('.admin-nav li');
    const sections = document.querySelectorAll('.admin-section');

    if (navItems.length > 0 && sections.length > 0) {
        navItems.forEach(item => {
            item.addEventListener('click', () => {
                // Убираем active у всех
                navItems.forEach(nav => nav.classList.remove('active'));
                sections.forEach(section => section.classList.remove('active'));

                // Добавляем active к выбранному
                item.classList.add('active');
                const sectionId = item.getAttribute('data-section') + 'Section';
                document.getElementById(sectionId).classList.add('active');
            });
        });
    }

    // Обработка переключения категорий
    const items = document.querySelectorAll('.category-list li');
    if (items.length > 0) {
        items.forEach(item => {
            item.addEventListener('click', () => {
                // Убираем active у всех
                items.forEach(nav => nav.classList.remove('active'));

                // Добавляем active к выбранному
                item.classList.add('active');
            });
        });
    }
});