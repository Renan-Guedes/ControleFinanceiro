document.addEventListener("DOMContentLoaded", function () {
    // Regras com mensagens
    const regras = [
        { id: "CategoriaId", cond: (v) => v && v !== "0", msg: "Escolha uma categoria." },
        { id: "BancoId", cond: (v) => v && v !== "0", msg: "Escolha um banco." },
        { id: "Descricao", cond: (v) => v && v.trim() !== "", msg: "Informe uma descrição." },
        { id: "ValorPlanejado", cond: (v) => v && parseFloat(v) > 0, msg: "Digite um valor maior que 0." },
    ];

    // Função para validar e exibir mensagem
    const validarCampo = (campo, cond, msg) => {
        let valor = campo.value;
        let help = campo.nextElementSibling;

        // se não existir um span de ajuda, cria um
        if (!help || !help.classList.contains("invalid-feedback")) {
            help = document.createElement("div");
            help.className = "invalid-feedback";
            campo.insertAdjacentElement("afterend", help);
        }

        if (!cond(valor)) {
            campo.classList.add("is-invalid");
            campo.classList.remove("is-valid");
            help.textContent = msg;
            help.style.display = "block";
            return false;
        } else {
            campo.classList.remove("is-invalid");
            campo.classList.add("is-valid");
            help.textContent = "";
            help.style.display = "none";
            return true;
        }
    };

    // Adiciona eventos de validação em cada campo
    regras.forEach(r => {
        const campo = document.getElementById(r.id);
        if (campo) {
            campo.addEventListener("input", () => validarCampo(campo, r.cond, r.msg));
            campo.addEventListener("change", () => validarCampo(campo, r.cond, r.msg));
        }
    });

    // Validação geral no submit
    const form = document.getElementById("formTransacao");
    form.addEventListener("submit", function (e) {
        let valido = true;
        regras.forEach(r => {
            const campo = document.getElementById(r.id);
            if (!validarCampo(campo, r.cond, r.msg)) {
                valido = false;
            }
        });

        if (!valido) {
            e.preventDefault(); // cancela o envio se houver erro
        }
    });
});
