function aplicarMascaraMoeda(input) {
    input.addEventListener("input", (e) => {
        let value = e.target.value.replace(/\D/g, "");
        let numberValue = parseFloat(value) / 100;
        if (!isNaN(numberValue)) {
            e.target.value = numberValue.toLocaleString("pt-BR", {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            });
        } else {
            e.target.value = "";
        }
    });

    if (input.form) {
        input.form.addEventListener("submit", () => {
            if (input.value) {
                let raw = input.value.replace(/\./g, "");
                input.value = raw;
            }
        });
    }
}

function initMascaraMoeda(seletor) {
    document.querySelectorAll(seletor).forEach(aplicarMascaraMoeda);
}