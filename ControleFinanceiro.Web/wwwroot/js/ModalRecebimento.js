function initModalRecebimento(modalId) {
    const recebimentoModal = document.getElementById(modalId);
    if (!recebimentoModal) return;

    recebimentoModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        document.getElementById('recebimentoId').value = button.getAttribute('data-id');
        document.getElementById('recebimentoDescricao').value = button.getAttribute('data-descricao');
        document.getElementById('recebimentoValor').value = parseFloat(button.getAttribute('data-valor'))
            .toLocaleString("pt-BR", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        document.getElementById('recebimentoBanco').value = button.getAttribute('data-banco');
    });
}