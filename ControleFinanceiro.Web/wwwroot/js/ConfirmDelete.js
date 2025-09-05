function initConfirmDelete(options) {
    const {
        selector,
        url,
        tokenSelector = 'input[name="__RequestVerificationToken"]',
        mensagens = {}
    } = options;

    const config = {
        titulo: mensagens.titulo || 'Confirmação de Exclusão',
        texto: mensagens.texto || 'Tem certeza que deseja excluir este item? Esta ação não pode ser desfeita.',
        textoConfirmar: mensagens.textoConfirmar || 'Excluir',
        textoCancelar: mensagens.textoCancelar || 'Cancelar',
        sucesso: mensagens.sucesso || 'Item excluído com sucesso!',
        erro: mensagens.erro || 'Ocorreu um erro ao excluir.'
    };

    document.querySelectorAll(selector).forEach(botao => {
        botao.addEventListener('click', function (e) {
            e.preventDefault();
            const id = botao.getAttribute('data-id');

            Swal.fire({
                title: config.titulo,
                text: config.texto,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#6c757d',
                confirmButtonText: config.textoConfirmar,
                cancelButtonText: config.textoCancelar
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire({
                        title: 'Excluindo...',
                        text: 'Aguarde um momento.',
                        allowOutsideClick: false,
                        didOpen: () => {
                            Swal.showLoading();
                        }
                    });

                    fetch(url, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'RequestVerificationToken': document.querySelector(tokenSelector).value
                        },
                        body: JSON.stringify(id)
                    })
                        .then(response => {
                            if (response.ok) {
                                Swal.fire('Excluído!', config.sucesso, 'success');
                                botao.closest('tr')?.remove();
                            } else {
                                Swal.fire('Erro!', config.erro, 'error');
                            }
                        })
                        .catch(() => {
                            Swal.fire('Erro!', 'Não foi possível conectar ao servidor.', 'error');
                        });
                }
            });
        });
    });
}