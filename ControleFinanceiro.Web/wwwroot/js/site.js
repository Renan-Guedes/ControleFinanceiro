// Global variables
let sidebarCollapsed = false;
let currentModalType = '';

// Initialize app
document.addEventListener('DOMContentLoaded', function () {
    //initializeApp();
    loadCharts();
});

function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');

    if (window.innerWidth <= 768) {
        sidebar.classList.toggle('show');
    } else {
        sidebar.classList.toggle('collapsed');
        mainContent.classList.toggle('expanded');
    }

    sidebarCollapsed = !sidebarCollapsed;
}

function openModal(type) {
    currentModalType = type;
    const modal = new bootstrap.Modal(document.getElementById('financeModal'));
    const modalTitle = document.querySelector('.modal-title');

    // Reset form
    document.getElementById('financeForm').reset();

    // Set modal title based on type
    switch (type) {
        case 'receita':
            modalTitle.textContent = 'Nova Receita';
            break;
        case 'despesa':
            modalTitle.textContent = 'Nova Despesa';
            break;
        case 'categoria':
            modalTitle.textContent = 'Nova Categoria';
            break;
        case 'gasto-fixo':
            modalTitle.textContent = 'Novo Gasto Fixo';
            break;
        default:
            modalTitle.textContent = 'Nova Transação';
    }

    modal.show();
}

function saveTransaction() {
    const form = document.getElementById('transactionForm');
    if (form.checkValidity()) {
        const formData = new FormData(form);
        const data = Object.fromEntries(formData);

        // Here you would typically send data to your backend
        console.log('Saving transaction:', data);

        // Close modal
        const modal = bootstrap.Modal.getInstance(document.getElementById('transactionModal'));
        modal.hide();

        // Show success toast
        showToast(`${currentModalType.charAt(0).toUpperCase() + currentModalType.slice(1)} salva com sucesso!`, 'success');

        // Reset form
        form.reset();

        // Refresh data (in a real app, you would reload from backend)
        refreshData();
    } else {
        showToast('Por favor, preencha todos os campos obrigatórios.', 'error');
    }
}

function showToast(message, type = 'success') {
    const toast = document.getElementById('toast');
    const toastMessage = document.getElementById('toastMessage');

    toastMessage.textContent = message;

    // Add appropriate class for styling
    toast.className = `toast ${type === 'success' ? 'bg-success' : 'bg-danger'} text-white`;

    const bsToast = new bootstrap.Toast(toast);
    bsToast.show();
}

function refreshData() {
    // In a real application, this would fetch updated data from the backend
    console.log('Refreshing financial data...');
}

function loadCharts() {
    // Revenue vs Expense Chart
    const ctx1 = document.getElementById('revenueExpenseChart');
    if (ctx1) {
        new Chart(ctx1, {
            type: 'line',
            data: {
                labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun'],
                datasets: [{
                    label: 'Receitas',
                    data: [5000, 5200, 4800, 5500, 6000, 5800],
                    borderColor: '#10b981',
                    backgroundColor: 'rgba(16, 185, 129, 0.1)',
                    tension: 0.4
                }, {
                    label: 'Despesas',
                    data: [3200, 3400, 3100, 3600, 3800, 3500],
                    borderColor: '#ef4444',
                    backgroundColor: 'rgba(239, 68, 68, 0.1)',
                    tension: 0.4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'top',
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            callback: function (value) {
                                return 'R$ ' + value.toLocaleString();
                            }
                        }
                    }
                }
            }
        });
    }

    // Category Chart
    const ctx2 = document.getElementById('categoryChart');
    if (ctx2) {
        new Chart(ctx2, {
            type: 'doughnut',
            data: {
                labels: ['Alimentação', 'Casa', 'Transporte', 'Saúde', 'Outros'],
                datasets: [{
                    data: [1200, 800, 400, 300, 500],
                    backgroundColor: [
                        '#ef4444',
                        '#f59e0b',
                        '#3b82f6',
                        '#10b981',
                        '#8b5cf6'
                    ]
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'bottom',
                    }
                }
            }
        });
    }
}

// Handle window resize
window.addEventListener('resize', function () {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');

    if (window.innerWidth > 768) {
        sidebar.classList.remove('show');
        if (sidebarCollapsed) {
            sidebar.classList.add('collapsed');
            mainContent.classList.add('expanded');
        } else {
            sidebar.classList.remove('collapsed');
            mainContent.classList.remove('expanded');
        }
    }
});

// Close sidebar on mobile when clicking outside
document.addEventListener('click', function (e) {
    const sidebar = document.getElementById('sidebar');
    const toggleBtn = document.querySelector('.toggle-sidebar');

    if (window.innerWidth <= 768 &&
        !sidebar.contains(e.target) &&
        !toggleBtn.contains(e.target) &&
        sidebar.classList.contains('show')) {
        sidebar.classList.remove('show');
    }
});