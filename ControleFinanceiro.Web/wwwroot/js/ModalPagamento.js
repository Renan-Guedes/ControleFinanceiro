function initModalPagamento(modalId) {
    const pagamentoModal = document.getElementById(modalId);
    if (!pagamentoModal) return;

    pagamentoModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        document.getElementById('pagamentoId').value = button.getAttribute('data-id');
        document.getElementById('pagamentoDescricao').value = button.getAttribute('data-descricao');
        document.getElementById('pagamentoValor').value = parseFloat(button.getAttribute('data-valor'))
            .toLocaleString("pt-BR", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        document.getElementById('pagamentoBanco').value = button.getAttribute('data-banco');
    });
}