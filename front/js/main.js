import { authService } from './api.js';
import { updateHeader } from './header.js';

// Функция для инициализации модальных окон
function initializeModals() {
    console.log('Initializing modals...'); // Отладочный вывод

    // Модалки
    const loginModal = document.getElementById('loginModal');
    const registerModal = document.getElementById('registerModal');
    const loginBtn = document.getElementById('loginBtn');
    const registerBtn = document.getElementById('registerBtn');
    const closeButtons = document.querySelectorAll('.modal .close');

    console.log('Login modal:', loginModal); // Отладочный вывод
    console.log('Register modal:', registerModal); // Отладочный вывод
    console.log('Login button:', loginBtn); // Отладочный вывод
    console.log('Register button:', registerBtn); // Отладочный вывод

    // Функция для открытия модального окна
    function openModal(modal) {
        console.log('Opening modal:', modal); // Отладочный вывод
        if (modal) {
            modal.style.display = 'flex';
            modal.classList.add('show');
            document.body.style.overflow = 'hidden';
        }
    }

    // Функция для закрытия модального окна
    function closeModal(modal) {
        console.log('Closing modal:', modal); // Отладочный вывод
        if (modal) {
            modal.classList.remove('show');
            modal.style.display = 'none';
            document.body.style.overflow = '';
        }
    }

    // Открытие модалок
    if (loginBtn && loginModal) {
        console.log('Adding click handler to login button'); // Отладочный вывод
        loginBtn.addEventListener('click', (e) => {
            e.preventDefault();
            console.log('Login button clicked'); // Отладочный вывод
            closeModal(registerModal);
            openModal(loginModal);
        });
    }

    if (registerBtn && registerModal) {
        console.log('Adding click handler to register button'); // Отладочный вывод
        registerBtn.addEventListener('click', (e) => {
            e.preventDefault();
            console.log('Register button clicked'); // Отладочный вывод
            closeModal(loginModal);
            openModal(registerModal);
        });
    }

    // Закрытие модалок по крестику
    closeButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            const modal = btn.closest('.modal');
            closeModal(modal);
        });
    });

    // Закрытие модалок по клику вне
    window.addEventListener('click', (e) => {
        if (e.target.classList.contains('modal')) {
            closeModal(e.target);
        }
    });

    // Закрытие по Escape
    window.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') {
            document.querySelectorAll('.modal.show').forEach(modal => {
                closeModal(modal);
            });
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

            try {
                const response = await authService.login({
                    username: usernameInput.value,
                    password: passwordInput.value
                });

                // Store auth data
                localStorage.setItem('authToken', response.token);
                localStorage.setItem('userRole', response.role);
                localStorage.setItem('userName', response.username);

                // Update UI
                await updateHeader();
                closeModal(loginModal);
                showMessage('Успешный вход!', 'success');

                // Redirect if admin
                if (response.role === 'admin') {
                    window.location.href = '/front/pages/admin/dashboard.html';
                }
            } catch (error) {
                showMessage('Ошибка входа: ' + error.message, 'error');
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

            if (passwordInput.value !== confirmInput.value) {
                showMessage('Пароли не совпадают', 'error');
                return;
            }

            try {
                await authService.register({
                    username: usernameInput.value,
                    email: emailInput.value,
                    phoneNumber: phoneInput.value,
                    password: passwordInput.value
                });

                showMessage('Регистрация успешна! Теперь вы можете войти.', 'success');
                closeModal(registerModal);
                openModal(loginModal);
            } catch (error) {
                showMessage('Ошибка регистрации: ' + error.message, 'error');
            }
        });
    }
}

// Сообщения
function showMessage(message, type) {
    const msg = document.createElement('div');
    msg.className = `status-message status-${type}`;
    msg.textContent = message;
    document.body.appendChild(msg);
    setTimeout(() => msg.remove(), 3000);
}

// Инициализация при загрузке страницы
console.log('main.js loaded');

// Проверяем, что DOM уже загружен
if (document.readyState === 'loading') {
    console.log('DOM still loading, waiting for DOMContentLoaded...');
    document.addEventListener('DOMContentLoaded', initializeApp);
} else {
    console.log('DOM already loaded, initializing immediately...');
    initializeApp();
}

function initializeApp() {
    console.log('Initializing app...');
    console.log('Document body:', document.body);
    console.log('Header placeholder:', document.getElementById('header-placeholder'));
    console.log('Login button:', document.getElementById('loginBtn'));
    console.log('Register button:', document.getElementById('registerBtn'));
    console.log('Login modal:', document.getElementById('loginModal'));
    console.log('Register modal:', document.getElementById('registerModal'));

    initializeModals();
    updateHeader();
}