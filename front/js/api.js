const API_URL = 'http://localhost:5067/api';

function getAuthHeaders() {
    const token = localStorage.getItem('authToken');
    return token ? { 'Authorization': `Bearer ${token}` } : {};
}

async function handleResponse(response) {
    if (!response.ok) {
        const error = await response.json().catch(() => ({ message: 'Произошла ошибка' }));
        throw new Error(error.message || 'Произошла ошибка при выполнении запроса');
    }
    return response.json();
}

// Auth Service
export const authService = {
    async login(credentials) {
        try {
            const response = await fetch(`${API_URL}/auth/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(credentials)
            });

            const data = await handleResponse(response);
            localStorage.setItem('authToken', data.token);
            localStorage.setItem('userRole', data.role);
            localStorage.setItem('userName', data.username);
            return data;
        } catch (error) {
            throw new Error(error.message || 'Ошибка входа. Проверьте правильность логина и пароля.');
        }
    },

    async register(userData) {
        try {
            const response = await fetch(`${API_URL}/auth/register`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(userData)
            });

            return await handleResponse(response);
        } catch (error) {
            throw new Error(error.message || 'Ошибка регистрации. Возможно, пользователь с таким логином уже существует.');
        }
    },

    logout() {
        localStorage.removeItem('authToken');
        localStorage.removeItem('userRole');
        localStorage.removeItem('userName');
        window.location.href = '/front/index.html';
    },

    isAuthenticated() {
        return !!localStorage.getItem('authToken');
    },

    getAuthToken() {
        return localStorage.getItem('authToken');
    },

    getUserRole() {
        return localStorage.getItem('userRole');
    }
};

// Menu Service
export const menuService = {
    async getMenuItems() {
        const headers = { ...getAuthHeaders() };
        const response = await fetch(`${API_URL}/menu`, { headers });
        if (!response.ok) {
            throw new Error('Failed to fetch menu items');
        }
        return await response.json();
    },

    async addMenuItem(item) {
        const response = await fetch(`${API_URL}/menu`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                ...getAuthHeaders()
            },
            body: JSON.stringify(item)
        });

        if (!response.ok) {
            throw new Error('Failed to add menu item');
        }

        return await response.json();
    },

    async updateMenuItem(id, item) {
        const response = await fetch(`${API_URL}/menu/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                ...getAuthHeaders()
            },
            body: JSON.stringify(item)
        });

        if (!response.ok) {
            throw new Error('Failed to update menu item');
        }

        return await response.json();
    },

    async deleteMenuItem(id) {
        const response = await fetch(`${API_URL}/menu/${id}`, {
            method: 'DELETE',
            headers: { ...getAuthHeaders() }
        });

        if (!response.ok) {
            throw new Error('Failed to delete menu item');
        }
    }
};

// Order Service
export const orderService = {
    async createOrder(orderData) {
        const response = await fetch(`${API_URL}/orders`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify(orderData)
        });

        if (!response.ok) {
            throw new Error('Failed to create order');
        }

        return await response.json();
    },

    async getOrders() {
        const response = await fetch(`${API_URL}/orders`, {
            headers: {
                'Authorization': `Bearer ${authService.getAuthToken()}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch orders');
        }

        return await response.json();
    },

    async updateOrderStatus(orderId, status) {
        const response = await fetch(`${API_URL}/orders/${orderId}/status`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify({ status })
        });

        if (!response.ok) {
            throw new Error('Failed to update order status');
        }

        return await response.json();
    },

    async cancelOrder(orderId) {
        const response = await fetch(`${API_URL}/orders/${orderId}/cancel`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${authService.getAuthToken()}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to cancel order');
        }
    }
};

// Employee Service
export const employeeService = {
    async getEmployees() {
        const response = await fetch(`${API_URL}/employees`, {
            headers: {
                'Authorization': `Bearer ${authService.getAuthToken()}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch employees');
        }

        return await response.json();
    },

    async addEmployee(employeeData) {
        const response = await fetch(`${API_URL}/employees`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify(employeeData)
        });

        if (!response.ok) {
            throw new Error('Failed to add employee');
        }

        return await response.json();
    },

    async updateEmployee(id, employeeData) {
        const response = await fetch(`${API_URL}/employees/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify(employeeData)
        });

        if (!response.ok) {
            throw new Error('Failed to update employee');
        }

        return await response.json();
    },

    async deleteEmployee(id) {
        const response = await fetch(`${API_URL}/employees/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${authService.getAuthToken()}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to delete employee');
        }
    }
};

// Settings Service
export const settingsService = {
    async getSettings() {
        const response = await fetch(`${API_URL}/settings`, {
            headers: {
                'Authorization': `Bearer ${authService.getAuthToken()}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch settings');
        }

        return await response.json();
    },

    async updateWorkingHours(workingHours) {
        const response = await fetch(`${API_URL}/settings/working-hours`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify({ workingHours })
        });

        if (!response.ok) {
            throw new Error('Failed to update working hours');
        }

        return await response.json();
    },

    async updateRestaurantStatus(isOpen) {
        const response = await fetch(`${API_URL}/settings/status`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify({ isOpen })
        });

        if (!response.ok) {
            throw new Error('Failed to update restaurant status');
        }

        return await response.json();
    },

    async updateContactInfo(contactInfo) {
        const response = await fetch(`${API_URL}/settings/contact-info`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify(contactInfo)
        });

        if (!response.ok) {
            throw new Error('Failed to update contact info');
        }

        return await response.json();
    },

    async updateSocialMedia(socialMedia) {
        const response = await fetch(`${API_URL}/settings/social-media`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authService.getAuthToken()}`
            },
            body: JSON.stringify(socialMedia)
        });

        if (!response.ok) {
            throw new Error('Failed to update social media links');
        }

        return await response.json();
    }
};