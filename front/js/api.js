const API_BASE_URL = 'http://localhost:5067/api';

// Функція для виконання запитів до API
async function fetchApi(endpoint, method = 'GET', data = null) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    // Додаємо токен авторизації з localStorage якщо він є
    const token = localStorage.getItem('authToken');
    if (token) {
        options.headers['Authorization'] = `Bearer ${token}`;
    }

    // Додаємо тіло запиту для методів POST/PUT
    if (data && (method === 'POST' || method === 'PUT')) {
        options.body = JSON.stringify(data);
    }

    try {
        const response = await fetch(`${API_BASE_URL}/${endpoint}`, options);

        // Перевіряємо статус відповіді
        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Сталася помилка при запиті до API');
        }

        return await response.json();
    } catch (error) {
        console.error('Помилка API:', error);
        throw error;
    }
}

// Функції для роботи з API
const api = {
    // Автентифікація
    login: (username, password) => {
        return fetchApi('auth/login', 'POST', { username, password });
    },

    // Робота з меню
    getMenuItems: () => {
        return fetchApi('menu');
    },
    getMenuItem: (id) => {
        return fetchApi(`menu/${id}`);
    },

    // Робота з замовленнями
    getOrders: () => {
        return fetchApi('orders');
    },
    createOrder: (orderData) => {
        return fetchApi('orders', 'POST', orderData);
    },
    updateOrderStatus: (orderId, status) => {
        return fetchApi(`orders/${orderId}/status`, 'PUT', { status });
    }
};