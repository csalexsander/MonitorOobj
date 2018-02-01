new Chart(document.getElementById("pie-chart-memory"), {
    type: 'pie',
    data: {
        labels: ["Memoria", "Livre"],
        datasets: [{
            label: "Monitoramento de Memoria",
            backgroundColor: ["#8B0000", "#FFD700"],
            data: [433, 1000]
        }]
    },
    options: {
        title: {
            display: true,
            text: 'Uso atual de Memoria'
        }
    }
});

new Chart(document.getElementById("pie-chart-message"), {
    type: 'pie',
    data: {
        labels: ["Resposta", "Recebe"],
        datasets: [{
            label: "Monitoramento Filas",
            backgroundColor: ["#0000CD", "#006400"],
            data: [433, 1000]
        }]
    },
    options: {
        title: {
            display: true,
            text: 'Monitoramento Filas'
        }
    }
});