import { authService } from './api.js';

document.addEventListener('DOMContentLoaded', function () {
    // Модалки
    const loginModal = document.getElementById('loginModal');
    const registerModal = document.getElementById('registerModal');
    const loginBtn = document.getElementById('loginBtn');
    const registerBtn = document.getElementById('registerBtn');
    const closeButtons = document.querySelectorAll('.modal .close');

    // Открытие модалок
    if (loginBtn && loginModal) {
        loginBtn.addEventListener('click', () => loginModal.classList.add('show'));
    }
    if (registerBtn && registerModal) {
        registerBtn.addEventListener('click', () => registerModal.classList.add('show'));
    }
    // Закрытие модалок по крестику
    closeButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            btn.closest('.modal').classList.remove('show');
        });
    });
    // Закрытие модалок по клику вне
    window.addEventListener('click', (e) => {
        if (e.target.classList.contains('modal')) {
            e.target.classList.remove('show');
        }
    });
    // Закрытие по Escape
    window.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') {
            document.querySelectorAll('.modal.show').forEach(m => m.classList.remove('show'));
        }
    });

    // Форма входа
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            const usernameInput = document.getElementById('loginUsername');
            const passwordInput = document.getElementById('loginPassword');
            if (!usernameInput || !passwordInput) return;
            const username = usernameInput.value;
            const password = passwordInput.value;
            try {
                const response = await authService.login({ username, password });
                localStorage.setItem('authToken', response.token);
                localStorage.setItem('userRole', response.role);
                localStorage.setItem('userName', response.username);
                updateHeaderUI();
                loginModal.classList.remove('show');
                window.location.reload();
            } catch (error) {
                showMessage('Помилка входу: ' + error.message, 'error');
            }
        });
    }
    // Форма регистрации
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        registerForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            const usernameInput = document.getElementById('registerUsername');
            const emailInput = document.getElementById('registerEmail');
            const phoneInput = document.getElementById('registerPhone');
            const passwordInput = document.getElementById('registerPassword');
            const confirmInput = document.getElementById('registerConfirmPassword');
            if (!usernameInput || !emailInput || !phoneInput || !passwordInput || !confirmInput) return;
            const username = usernameInput.value;
            const email = emailInput.value;
            const phoneNumber = phoneInput.value;
            const password = passwordInput.value;
            const passwordConfirm = confirmInput.value;
            if (password !== passwordConfirm) {
                showMessage('Паролі не співпадають', 'error');
                return;
            }
            try {
                await authService.register({ username, email, phoneNumber, password });
                showMessage('Реєстрація успішна! Тепер ви можете увійти.', 'success');
                registerModal.classList.remove('show');
                loginModal.classList.add('show');
            } catch (error) {
                showMessage('Помилка реєстрації: ' + error.message, 'error');
            }
        });
    }
    // Сообщения
    function showMessage(message, type) {
        const msg = document.createElement('div');
        msg.className = `status-message status-${type}`;
        msg.textContent = message;
        document.body.appendChild(msg);
        setTimeout(() => msg.remove(), 3000);
    }
    // Обновление header в зависимости от авторизации
    function updateHeaderUI() {
        const authToken = localStorage.getItem('authToken');
        const userName = localStorage.getItem('userName');
        const userRole = localStorage.getItem('userRole');
        const authButtons = document.querySelector('.auth-buttons');
        const userInfo = document.querySelector('.user-info');
        const adminLink = document.querySelector('.admin-link');
        if (authToken && userName) {
            if (authButtons) authButtons.style.display = 'none';
            if (userInfo) {
                userInfo.style.display = '';
                const usernameSpan = userInfo.querySelector('.username');
                if (usernameSpan) usernameSpan.textContent = `Вітаємо, ${userName}!`;
                const logoutBtn = userInfo.querySelector('.logout-btn');
                if (logoutBtn) {
                    logoutBtn.onclick = () => {
                        authService.logout();
                        updateHeaderUI();
                        window.location.href = '/front/index.html';
                    };
                }
            }
            if (adminLink && (userRole === 'admin' || userRole === 'employee')) {
                adminLink.style.display = '';
            } else if (adminLink) {
                adminLink.style.display = 'none';
            }
        } else {
            if (authButtons) authButtons.style.display = '';
            if (userInfo) userInfo.style.display = 'none';
            if (adminLink) adminLink.style.display = 'none';
        }
    }
    updateHeaderUI();

    // Перевірка авторизації при завантаженні сторінки
    const authToken = localStorage.getItem('authToken');
    if (authToken) {
        // Якщо користувач вже авторизований, оновлюємо інтерфейс
        updateUIForLoggedInUser();
    }

    // Функція оновлення інтерфейсу для авторизованого користувача
    function updateUIForLoggedInUser() {
        const userName = localStorage.getItem('userName');
        const userRole = localStorage.getItem('userRole');

        // Оновлюємо кнопки авторизації
        const authButtons = document.querySelector('.auth-buttons');
        if (authButtons) {
            authButtons.innerHTML = `
                <span class="user-info">Вітаємо, ${userName}!</span>
                <button id="logoutBtn" class="btn btn-secondary">Вийти</button>
            `;

            // Додаємо обробник для кнопки виходу
            const logoutBtn = document.getElementById('logoutBtn');
            if (logoutBtn) {
                logoutBtn.addEventListener('click', () => {
                    localStorage.removeItem('authToken');
                    localStorage.removeItem('userRole');
                    localStorage.removeItem('userName');
                    window.location.reload();
                });
            }
        }

        // Оновлюємо меню в залежності від ролі
        const menu = document.querySelector('.menu');
        if (menu && (userRole === 'admin' || userRole === 'employee')) {
            const adminLink = document.createElement('li');
            adminLink.innerHTML = `<a href="/front/pages/admin/dashboard.html">Панель керування</a>`;
            menu.appendChild(adminLink);
        }
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