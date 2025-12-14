document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".btn-delete").forEach(btn => {
        btn.addEventListener("click", (e) => {
            e.preventDefault();
            const id = btn.getAttribute("data-id");
            eliminarNota(id);
        });
    });
});

function eliminarNota(id) {
    if (confirm("¿Seguro que deseas eliminar esta nota?")) {
        // Usar el mismo enfoque que TogglePin
        fetch(`/Notas/DeleteConfirmed/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: `id=${id}`
        })
        .then(response => {
            if (response.ok || response.redirected) {
                location.reload();
            } else {
                alert("Error al eliminar la nota.");
            }
        })
        .catch(error => {
            console.error("Error:", error);
            alert("Error al eliminar la nota.");
        });
    }
}
