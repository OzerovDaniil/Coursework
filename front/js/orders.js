import { orderService, authService } from '../api.js';

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
            localStorage.getItem('userName') || 'Администратор';
    }

    setupEventListeners() {
        // Выход из системы
        document.getElementById('logoutBtn').addEventListener('click', () => {
            authService.logout();
            window.location.href = '../index.html';
        });

        // Обновление списка заказов
        document.getElementById('refreshOrdersBtn').addEventListener('click', () => {
            this.loadOrders();
        });

        // Создание нового заказа
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
            console.error('Ошибка загрузки заказов:', error);
            this.showError('Не удалось загрузить заказы');
        } finally {
            this.showLoading(false);
        }
    }

    renderFilters() {
        const filtersContainer = document.getElementById('orderFilters');
        filtersContainer.innerHTML = `
            <div class="filter-group">
                <select id="statusFilter" class="filter-select">
                    <option value="all">Все статусы</option>
                    <option value="new">Новые</option>
                    <option value="preparing">В процессе</option>
                    <option value="ready">Готовы</option>
                    <option value="delivered">Завершённые</option>
                </select>
            </div>
            <div class="filter-group">
                <select id="dateFilter" class="filter-select">
                    <option value="today">Сегодня</option>
                    <option value="week">Неделя</option>
                    <option value="month">Месяц</option>
                    <option value="all">Все время</option>
                </select>
            </div>
            <div class="filter-group">
                <input type="text" id="searchFilter" placeholder="Поиск по номеру или клиенту" class="search-input">
            </div>
        `;

        // Обработчики фильтров
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
        
        // Фильтрация по статусу
        if (this.currentFilters.status !== 'all') {
            filtered = filtered.filter(order => order.status === this.currentFilters.status);
        }
        
        // Фильтрация по дате
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

        // Фильтрация по поиску
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
            container.innerHTML = '<p class="no-orders">Нет заказов по выбранным критериям</p>';
            return;
        }

        container.innerHTML = orders.map(order => `
            <div class="order-item" data-order-id="${order.id}">
                <div class="order-header">
                    <span class="order-id">Заказ #${order.id}</span>
                    <span class="order-date">${this.formatDate(order.createdAt)}</span>
                    <span class="order-status status-${order.status}">
                        ${this.translateStatus(order.status)}
                    </span>
                </div>
                <div class="order-body">
                    <div class="order-client">
                        <span class="client-phone">${order.clientPhone || 'Нет данных'}</span>
                        ${order.deliveryAddress ? `<span class="delivery-address">${order.deliveryAddress}</span>` : ''}
                    </div>
                    <div class="order-items">
                        ${this.formatItems(order.items)}
                    </div>
                    <div class="order-footer">
                        <span class="order-total">Итого: ${order.total} ₽</span>
                        ${order.comment ? `<div class="order-comment">Примечание: ${order.comment}</div>` : ''}
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
        return new Date(dateString).toLocaleString('ru-RU', options);
    }

    translateStatus(status) {
        const statusMap = {
            'new': 'Новый',
            'preparing': 'Готовится',
            'ready': 'Готов',
            'delivered': 'Доставлен'
        };
        return statusMap[status] || status;
    }

    formatItems(items) {
        return items.map(item => `
            <div class="order-item-row">
                <span class="item-name">${item.name}</span>
                <span class="item-quantity">${item.quantity} × ${item.price} ₽</span>
                <span class="item-total">${item.quantity * item.price} ₽</span>
            </div>
        `).join('');
    }

    getActionButtons(order) {
        const buttons = [];
        const userRole = localStorage.getItem('userRole');
        
        // Кнопки изменения статуса
        if (['admin', 'chef'].includes(userRole)) {
            if (order.status === 'new') {
                buttons.push(`
                    <button class="action-btn status-btn" data-status="preparing">
                        Начать готовить
                    </button>
                `);
            } else if (order.status === 'preparing') {
                buttons.push(`
                    <button class="action-btn status-btn" data-status="ready">
                        Готов к выдаче
                    </button>
                `);
            }
        }
        
        if (['admin', 'waiter'].includes(userRole)) {
            if (order.status === 'ready') {
                buttons.push(`
                    <button class="action-btn status-btn" data-status="delivered">
                        Подтвердить доставку
                    </button>
                `);
            }
        }
        
        // Общие кнопки
        buttons.push(`
            <button class="action-btn details-btn">
                Подробности
            </button>
        `);
        
        return buttons.join('');
    }

    setupActionHandlers() {
        // Обработчики кнопок статуса
        document.querySelectorAll('.status-btn').forEach(btn => {
            btn.addEventListener('click', async (e) => {
                const orderId = e.target.closest('.order-item').dataset.orderId;
                const newStatus = e.target.dataset.status;
                await this.updateOrderStatus(orderId, newStatus);
            });
        });

        // Обработчики кнопок подробностей
        document.querySelectorAll('.details-btn').forEach(btn => {
            btn.addEventListener('click', (e) => {
                const orderId = e.target.closest('.order-item').dataset.orderId;
                this.showOrderDetails(orderId);
            });
        });
    }

    async updateOrderStatus(orderId, newStatus) {
        try {
            await orderService.updateStatus(orderId, newStatus);
            await this.loadOrders();
        } catch (error) {
            console.error('Ошибка обновления статуса:', error);
            alert('Не удалось обновить статус заказа');
        }
    }

    showOrderDetails(orderId) {
        const order = this.allOrders.find(o => o.id == orderId);
        if (order) {
            const detailsHtml = `
                <div class="order-details-modal">
                    <h3>Детали заказа #${order.id}</h3>
                    <div class="detail-row">
                        <span class="detail-label">Дата:</span>
                        <span>${this.formatDate(order.createdAt)}</span>
                    </div>
                    <div class="detail-row">
                        <span class="detail-label">Статус:</span>
                        <span class="order-status status-${order.status}">
                            ${this.translateStatus(order.status)}
                        </span>
                    </div>
                    <div class="detail-row">
                        <span class="detail-label">Клиент:</span>
                        <span>${order.clientPhone || 'Не указан'}</span>
                    </div>
                    ${order.deliveryAddress ? `
                    <div class="detail-row">
                        <span class="detail-label">Адрес:</span>
                        <span>${order.deliveryAddress}</span>
                    </div>` : ''}
                    <div class="detail-row">
                        <span class="detail-label">Состав заказа:</span>
                        <div class="order-items-details">
                            ${this.formatItemsDetailed(order.items)}
                        </div>
                    </div>
                    <div class="detail-row total-row">
                        <span class="detail-label">Итого:</span>
                        <span class="order-total">${order.total} ₽</span>
                    </div>
                    ${order.comment ? `
                    <div class="detail-row">
                        <span class="detail-label">Примечание:</span>
                        <span>${order.comment}</span>
                    </div>` : ''}
                </div>
            `;
            
            // Здесь можно реализовать модальное окно или другой способ показа деталей
            console.log(detailsHtml); // Для демонстрации
            alert(`Детали заказа #${order.id}\nКлиент: ${order.clientPhone}\nСумма: ${order.total} ₽`);
        }
    }

    formatItemsDetailed(items) {
        return items.map(item => `
            <div class="order-item-detailed">
                <span class="item-name">${item.name}</span>
                <span class="item-quantity">${item.quantity} × ${item.price} ₽</span>
                <span class="item-total">${item.quantity * item.price} ₽</span>
            </div>
        `).join('');
    }

    showLoading(show) {
        const loader = document.getElementById('ordersLoadingIndicator');
        const list = document.getElementById('ordersList');
        
        if (show) {
            loader.style.display = 'flex';
            list.style.opacity = '0.5';
        } else {
            loader.style.display = 'none';
            list.style.opacity = '1';
        }
    }

    showError(message) {
        const container = document.getElementById('ordersList');
        container.innerHTML = `
            <div class="error-message">
                <span class="error-icon">⚠</span>
                <span>${message}</span>
                <button id="retryBtn" class="secondary-btn">Повторить</button>
            </div>
        `;
        
        document.getElementById('retryBtn').addEventListener('click', () => {
            this.loadOrders();
        });
    }
}

// Инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', () => {
    new OrdersManager();
});