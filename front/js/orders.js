import { orderService, authService } from './api.js';

class OrdersManager {
    constructor() {
        this.currentFilters = {
            status: 'all',
            date: 'today',
            search: ''
        };
        this.init();
    }

    async init() {
        this.checkAuth();
        this.setupEventListeners();
        await this.loadOrders();
    }

    checkAuth() {
        const token = localStorage.getItem('authToken');
        if (!token) {
            window.location.href = '../index.html';
            return;
        }
        document.getElementById('currentUserName').textContent =
            localStorage.getItem('userName') || 'Адміністратор';
    }

    setupEventListeners() {
        document.getElementById('logoutBtn').addEventListener('click', () => {
            authService.logout();
            window.location.href = '../index.html';
        });

        // Оновлення списку замовлень
        document.getElementById('refreshOrdersBtn').addEventListener('click', () => {
            this.loadOrders();
        });

        // Створення нового замовлення
        document.getElementById('createOrderBtn').addEventListener('click', () => {
            window.location.href = 'create-order.html';
        });
    }

    async loadOrders() {
        try {
            this.showLoading(true);
            const orders = await orderService.getAll();
            this.allOrders = orders;
            this.renderFilters();
            this.applyFilters();
        } catch (error) {
            console.error('Помилка завантаження замовлень:', error);
            this.showError('Не вдалося завантажити замовлення');
        } finally {
            this.showLoading(false);
        }
    }

    renderFilters() {
        const filtersContainer = document.getElementById('orderFilters');
        filtersContainer.innerHTML = `
            <div class="filter-group">
                <select id="statusFilter" class="filter-select">
                    <option value="all">Всі статуси</option>
                    <option value="new">Нові</option>
                    <option value="preparing">В процесі</option>
                    <option value="ready">Готові</option>
                    <option value="delivered">Завершені</option>
                </select>
            </div>
            <div class="filter-group">
                <select id="dateFilter" class="filter-select">
                    <option value="today">Сьогодні</option>
                    <option value="week">Тиждень</option>
                    <option value="month">Місяць</option>
                    <option value="all">Весь час</option>
                </select>
            </div>
            <div class="filter-group">
                <input type="text" id="searchFilter" placeholder="Пошук за номером або клієнтом" class="search-input">
            </div>
        `;

        // Обробники фільтрів
        document.getElementById('statusFilter').addEventListener('change', (e) => {
            this.currentFilters.status = e.target.value;
            this.applyFilters();
        });

        document.getElementById('dateFilter').addEventListener('change', (e) => {
            this.currentFilters.date = e.target.value;
            this.applyFilters();
        });

        document.getElementById('searchFilter').addEventListener('input', (e) => {
            this.currentFilters.search = e.target.value.toLowerCase();
            this.applyFilters();
        });
    }

    applyFilters() {
        let filtered = [...this.allOrders];

        // Фільтрація за статусом
        if (this.currentFilters.status !== 'all') {
            filtered = filtered.filter(order => order.status === this.currentFilters.status);
        }

        // Фільтрація за датою
        const now = new Date();
        switch (this.currentFilters.date) {
            case 'today':
                filtered = filtered.filter(order => {
                    const orderDate = new Date(order.createdAt);
                    return orderDate.toDateString() === now.toDateString();
                });
                break;
            case 'week':
                const weekAgo = new Date(now);
                weekAgo.setDate(now.getDate() - 7);
                filtered = filtered.filter(order => new Date(order.createdAt) >= weekAgo);
                break;
            case 'month':
                const monthAgo = new Date(now);
                monthAgo.setMonth(now.getMonth() - 1);
                filtered = filtered.filter(order => new Date(order.createdAt) >= monthAgo);
                break;
        }

        // Фільтрація за пошуком
        if (this.currentFilters.search) {
            filtered = filtered.filter(order =>
                order.id.toString().includes(this.currentFilters.search) ||
                (order.clientPhone && order.clientPhone.toLowerCase().includes(this.currentFilters.search))
            );
        }

        this.displayOrders(filtered);
    }

    displayOrders(orders) {
        const container = document.getElementById('ordersList');

        if (orders.length === 0) {
            container.innerHTML = '<p class="no-orders">Немає замовлень за обраними критеріями</p>';
            return;
        }

        container.innerHTML = orders.map(order => `
            <div class="order-item" data-order-id="${order.id}">
                <div class="order-header">
                    <span class="order-id">Замовлення #${order.id}</span>
                    <span class="order-date">${this.formatDate(order.createdAt)}</span>
                    <span class="order-status status-${order.status}">
                        ${this.translateStatus(order.status)}
                    </span>
                </div>
                <div class="order-body">
                    <div class="order-client">
                        <span class="client-phone">${order.clientPhone || 'Немає даних'}</span>
                        ${order.deliveryAddress ? `<span class="delivery-address">${order.deliveryAddress}</span>` : ''}
                    </div>
                    <div class="order-items">
                        ${this.formatItems(order.items)}
                    </div>
                    <div class="order-footer">
                        <span class="order-total">Разом: ${order.total} ₴</span>
                        ${order.comment ? `<div class="order-comment">Примітка: ${order.comment}</div>` : ''}
                    </div>
                </div>
                <div class="order-actions">
                    ${this.getActionButtons(order)}
                </div>
            </div>
        `).join('');

        this.setupActionHandlers();
    }

    formatDate(dateString) {
        const options = {
            day: 'numeric',
            month: 'numeric',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        };
        return new Date(dateString).toLocaleString('uk-UA', options);
    }

    translateStatus(status) {
        const statusMap = {
            'new': 'Новий',
            'preparing': 'Готується',
            'ready': 'Готовий',
            'delivered': 'Доставлений'
        };
        return statusMap[status] || status;
    }

    formatItems(items) {
        return items.map(item => `
            <div class="order-item-row">
                <span class="item-name">${item.name}</span>
                <span class="item-quantity">${item.quantity} × ${item.price} ₴</span>
                <span class="item-total">${item.quantity * item.price} ₴</span>
            </div>
        `).join('');
    }

    getActionButtons(order) {
        const buttons = [];
        const userRole = localStorage.getItem('userRole');

        // Кнопки зміни статусу
        if (['admin', 'chef'].includes(userRole)) {
            if (order.status === 'new') {
                buttons.push(`
                    <button class="action-btn status-btn" data-status="preparing">
                        Почати готувати
                    </button>
                `);
            } else if (order.status === 'preparing') {
                buttons.push(`
                    <button class="action-btn status-btn" data-status="ready">
                        Готовий до видачі
                    </button>
                `);
            }
        }

        if (['admin', 'waiter'].includes(userRole)) {
            if (order.status === 'ready') {
                buttons.push(`
                    <button class="action-btn status-btn" data-status="delivered">
                        Підтвердити доставку
                    </button>
                `);
            }
        }

        // Загальні кнопки
        buttons.push(`
            <button class="action-btn details-btn">
                Деталі
            </button>
        `);

        return buttons.join('');
    }

    setupActionHandlers() {
        // Обробники кнопок зміни статусу
        document.querySelectorAll('.status-btn').forEach(button => {
            button.addEventListener('click', async (e) => {
                const orderItem = e.target.closest('.order-item');
                const orderId = orderItem.dataset.orderId;
                const newStatus = e.target.dataset.status;

                try {
                    await this.updateOrderStatus(orderId, newStatus);
                    await this.loadOrders();
                } catch (error) {
                    this.showError('Помилка оновлення статусу замовлення');
                }
            });
        });

        // Обробники кнопок деталей
        document.querySelectorAll('.details-btn').forEach(button => {
            button.addEventListener('click', (e) => {
                const orderItem = e.target.closest('.order-item');
                const orderId = orderItem.dataset.orderId;
                this.showOrderDetails(orderId);
            });
        });
    }

    async updateOrderStatus(orderId, newStatus) {
        try {
            await orderService.updateStatus(orderId, newStatus);
        } catch (error) {
            throw new Error('Помилка оновлення статусу замовлення');
        }
    }

    showOrderDetails(orderId) {
        const order = this.allOrders.find(o => o.id === parseInt(orderId));
        if (!order) return;

        const modal = document.createElement('div');
        modal.className = 'modal';
        modal.innerHTML = `
            <div class="modal-content">
                <h2>Деталі замовлення #${order.id}</h2>
                <div class="order-details">
                    <div class="detail-group">
                        <h3>Інформація про клієнта</h3>
                        <p>Телефон: ${order.clientPhone || 'Немає даних'}</p>
                        ${order.deliveryAddress ? `<p>Адреса доставки: ${order.deliveryAddress}</p>` : ''}
                    </div>
                    <div class="detail-group">
                        <h3>Замовлені страви</h3>
                        ${this.formatItemsDetailed(order.items)}
                    </div>
                    <div class="detail-group">
                        <h3>Додаткова інформація</h3>
                        <p>Статус: ${this.translateStatus(order.status)}</p>
                        <p>Дата створення: ${this.formatDate(order.createdAt)}</p>
                        ${order.comment ? `<p>Примітка: ${order.comment}</p>` : ''}
                        <p>Загальна сума: ${order.total} ₴</p>
                    </div>
                </div>
                <button class="close-modal">Закрити</button>
            </div>
        `;

        document.body.appendChild(modal);
        modal.querySelector('.close-modal').addEventListener('click', () => {
            modal.remove();
        });
    }

    formatItemsDetailed(items) {
        return items.map(item => `
            <div class="detail-item">
                <span class="item-name">${item.name}</span>
                <span class="item-quantity">Кількість: ${item.quantity}</span>
                <span class="item-price">Ціна: ${item.price} ₴</span>
                <span class="item-total">Сума: ${item.quantity * item.price} ₴</span>
            </div>
        `).join('');
    }

    showLoading(show) {
        const loader = document.getElementById('ordersLoadingIndicator');
        if (loader) {
            loader.style.display = show ? 'flex' : 'none';
        }
    }

    showError(message) {
        alert(message);
    }
}

// Ініціалізація менеджера замовлень
new OrdersManager();