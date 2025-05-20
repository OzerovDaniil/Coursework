// Проверка авторизации при загрузке страницы
document.addEventListener('DOMContentLoaded', function() {
    const user = getCurrentUser();
    updateUI(user);
    
    // Обработчик формы входа
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', async function(e) {
            e.preventDefault();
            const username = document.getElementById('username').value;
            const password = document.getElementById('password').value;
            
            try {
                // Здесь должен быть вызов к вашему API
                // Пример:
                const response = await fetch('/api/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ username, password })
                });
                
                const data = await response.json();
                
                if (response.ok) {
                    // Сохраняем пользователя
                    localStorage.setItem('currentUser', JSON.stringify(data.user));
                    updateUI(data.user);
                    window.location.href = 'pages/menu.html';
                } else {
                    alert('Ошибка входа: ' + (data.message || 'Неверные данные'));
                }
            } catch (error) {
                alert('Ошибка сети: ' + error.message);
            }
        });
    }
});

// Получение текущего пользователя
function getCurrentUser() {
    const userData = localStorage.getItem('currentUser');
    return userData ? JSON.parse(userData) : null;
}

// Обновление интерфейса в зависимости от прав
function updateUI(user) {
    // Скрываем/показываем админ-ссылку
    const adminLink = document.querySelector('a[href="pages/admin.html"]');
    if (adminLink) {
        adminLink.style.display = user?.isAdmin ? 'block' : 'none';
    }
    
    // Если пользователь авторизован, перенаправляем с главной
    if (user && window.location.pathname.endsWith('index.html')) {
        window.location.href = 'pages/menu.html';
    }
}

// Для регистрации (добавьте форму в index.html)
export async function handleRegistration(email, password, adminCode = null) {
    try {
        const response = await fetch('/api/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                email,
                password,
                isAdmin: adminCode === 'SECRET_ADMIN_CODE' // Замените на реальный код
            })
        });
        
        const data = await response.json();
        
        if (response.ok) {
            localStorage.setItem('currentUser', JSON.stringify(data.user));
            return data.user;
        } else {
            throw new Error(data.message || 'Ошибка регистрации');
        }
    } catch (error) {
        console.error('Registration error:', error);
        throw error;
    }
}