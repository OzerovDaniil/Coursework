// Базовый URL API (замените на фактический URL, когда он будет доступен)
const API_BASE_URL = 'http://localhost:5000/api';

// Функция для выполнения запросов к API
async function fetchApi(endpoint, method = 'GET', data = null) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    // Добавляем токен авторизации из localStorage если он есть
    const token = localStorage.getItem('authToken');
    if (token) {
        options.headers['Authorization'] = `Bearer ${token}`;
    }

    // Добавляем тело запроса для методов POST/PUT
    if (data && (method === 'POST' || method === 'PUT')) {
        options.body = JSON.stringify(data);
    }

    try {
        const response = await fetch(`${API_BASE_URL}/${endpoint}`, options);
        
        // Проверяем статус ответа
        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Произошла ошибка при запросе к API');
        }

        return await response.json();
    } catch (error) {
        console.error('API Error:', error);
        throw error;
    }
}

// Функции для работы с API
const api = {
    // Аутентификация
    login: (username, password) => {
        return fetchApi('auth/login', 'POST', { username, password });
    },
    register: (userData) => {
        return fetchApi('auth/register', 'POST', userData);
    },
    
    // Работа с меню
    getMenuItems: () => {
        return fetchApi('menu');
    },
    getMenuItem: (id) => {
        return fetchApi(`menu/${id}`);
    },
    
    // Работа с заказами
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