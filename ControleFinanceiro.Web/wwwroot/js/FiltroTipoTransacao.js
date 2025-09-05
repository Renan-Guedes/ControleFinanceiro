function initFiltro({ filtroId, limparId, tabelaId, cardId }) {
    const filtro = document.getElementById(filtroId);
    const limpar = document.getElementById(limparId);
    const tabela = document.getElementById(tabelaId);
    const filtroCard = document.getElementById(cardId);

    if (!filtro || !tabela || !filtroCard) return;

    filtro.addEventListener("change", function () {
        const valor = this.value;
        const linhas = tabela.querySelectorAll("tr[data-tipo]");

        filtroCard.classList.remove("border-success", "border-danger", "bg-light", "bg-opacity-10");
        filtroCard.classList.add("bg-light", "bg-opacity-10");

        if (valor === "1") filtroCard.classList.add("border-success");
        else if (valor === "2") filtroCard.classList.add("border-danger");

        linhas.forEach(linha => {
            linha.style.display = !valor || linha.getAttribute("data-tipo") === valor ? "" : "none";
        });
    });

    limpar.addEventListener("click", function () {
        filtro.value = "";
        filtro.dispatchEvent(new Event("change"));
        filtroCard.classList.remove("border-success", "border-danger", "bg-light", "bg-opacity-10");
    });
}