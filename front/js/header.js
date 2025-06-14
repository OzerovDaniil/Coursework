import { authService } from './api.js';

// Функция для обновления шапки в зависимости от состояния авторизации
export async function updateHeader() {
    const authButtons = document.querySelector('.auth-buttons');
    const userInfo = document.querySelector('.user-info');
    const adminLink = document.querySelector('.admin-link');
    const customerOrdersLink = document.querySelector('[data-role="customer"]');

    if (authService.isAuthenticated()) {
        // Показываем информацию о пользователе
        if (authButtons) authButtons.style.display = 'none';
        if (userInfo) {
            userInfo.style.display = 'flex';
            const username = localStorage.getItem('userName');
            userInfo.querySelector('.username').textContent = username;
        }

        // Обновляем видимость пунктов меню в зависимости от роли
        const userRole = authService.getUserRole();
        if (adminLink) {
            adminLink.style.display = userRole === 'admin' ? 'block' : 'none';
        }
        if (customerOrdersLink) {
            customerOrdersLink.style.display = userRole === 'customer' ? 'block' : 'none';
        }
    } else {
        // Показываем кнопки авторизации
        if (authButtons) authButtons.style.display = 'flex';
        if (userInfo) userInfo.style.display = 'none';
        if (adminLink) adminLink.style.display = 'none';
        if (customerOrdersLink) customerOrdersLink.style.display = 'none';
    }

    updateActiveMenuItem();
}

// Функция для обновления активного пункта меню
export function updateActiveMenuItem() {
    const currentPath = window.location.pathname;
    const menuItems = document.querySelectorAll('.nav-menu a');

    menuItems.forEach(item => {
        if (item.getAttribute('href') === currentPath) {
            item.classList.add('active');
        } else {
            item.classList.remove('active');
        }
    });
}

// Инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', () => {
    // Обработчик для кнопки выхода
    const logoutBtn = document.querySelector('.logout-btn');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', () => {
            authService.logout();
            updateHeader();
            window.location.href = '/';
        });
    }

    // Обработчик для мобильного меню
    const menuToggle = document.querySelector('.menu-toggle');
    const navMenu = document.querySelector('.nav-menu');
    if (menuToggle && navMenu) {
        menuToggle.addEventListener('click', () => {
            navMenu.classList.toggle('show');
            menuToggle.classList.toggle('active');
        });
    }

    // Закрытие мобильного меню при клике вне его
    document.addEventListener('click', (event) => {
        if (navMenu && navMenu.classList.contains('show') &&
            !event.target.closest('.nav-menu') &&
            !event.target.closest('.menu-toggle')) {
            navMenu.classList.remove('show');
            menuToggle.classList.remove('active');
        }
    });

    updateHeader();
}); 