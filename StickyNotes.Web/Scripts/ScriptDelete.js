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
        fetch(`/Notas/DeleteConfirmed/${id}`, {
            method: "POST"
        })
            .then(response => {
                if (response.ok) {
                    document.querySelector(`.btn-delete[data-id="${id}"]`).closest(".note-card").remove();
                } else {
                    alert("Error al eliminar la nota.");
                }
            })
            .catch(error => console.error("Error:", error));
    }
}
