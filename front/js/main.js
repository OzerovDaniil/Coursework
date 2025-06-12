document.addEventListener('DOMContentLoaded', function() {
    // Проверяем, авторизован ли пользователь
    const authToken = localStorage.getItem('authToken');
    if (authToken) {
        // Если токен есть, перенаправляем на страницу заказов
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
                window.location.href = 'pages/orders.html';
            } catch (error) {
                alert('Ошибка входа: ' + error.message);
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
    const Items = document.querySelectorAll('.category-list li');

    if (Items.length > 0) {
        Items.forEach(item => {
            item.addEventListener('click', () => {
                // Убираем active у всех
                Items.forEach(nav => nav.classList.remove('active'));
        
                // Добавляем active к выбранному
                item.classList.add('active');
            })
        });
    }
});