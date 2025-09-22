function initConfirmarTransacao(options) {
    const {
        selector,
        url,
        tokenSelector = 'input[name="__RequestVerificationToken"]',
        mensagens = {}
    } = options;

    const config = {
        titulo: mensagens.titulo || 'Confirmação de Baixa',
        texto: mensagens.texto || 'Tem certeza que deseja realizar a baixa?',
        textoConfirmar: mensagens.textoConfirmar || 'Confirmar',
        textoCancelar: mensagens.textoCancelar || 'Cancelar',
        sucesso: mensagens.sucesso || 'Baixa realizada com sucesso!',
        erro: mensagens.erro || 'Ocorreu um erro. Entre em contato com o suporte!'
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
                confirmButtonColor: '#28A745',
                cancelButtonColor: '#6c757d',
                confirmButtonText: config.textoConfirmar,
                cancelButtonText: config.textoCancelar
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire({
                        title: 'Realizando Baixa...',
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
                        body: JSON.stringify(parseInt(id))
                    })
                    .then(response => {
                        if (response.ok) {
                            Swal.fire('Concluído!', config.sucesso, 'success');

                            // Atualiza a linha correspondente
                            const linha = botao.closest("tr");

                            // Atualizar badge de status
                            const statusCell = linha.querySelector("td:nth-child(6) span");
                            if (statusCell) {
                                statusCell.classList.remove("bg-danger");
                                statusCell.classList.add("bg-success");
                                // Pega o tipo da transação pra saber se é Receita ou Despesa
                                const tipo = linha.getAttribute("data-tipo");
                                statusCell.textContent = (tipo == "1") ? "Recebido" : "Pago";
                            }

                            // Trocar os botões
                            const cellAcoes = linha.querySelector("td:nth-child(7)");
                            if (cellAcoes) {
                                cellAcoes.innerHTML = `
                                <button class="btn btn-sm btn-secondary" disabled>
                                    <i class="bi bi-check-circle-fill"></i> Pago
                                </button>
                                <button type="button" class="btn btn-outline-warning border-0">
                                    <i class="bi bi-pencil"></i>
                                </button>
                                <a href="#" data-id="${id}" class="botao-Excluir btn btn-outline-danger border-0">
                                    <i class="bi bi-trash"></i>
                                </a>`;
                            }
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