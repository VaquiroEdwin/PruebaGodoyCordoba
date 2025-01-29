<template>
    <div class="container d-flex justify-content-center align-items-center vh-100">
      <div class="card shadow-lg p-3" style="width: 400px">
        <div class="card-header text-center fw-bold fs-4">¿Qué deseas hacer?</div>
        <div class="card-body d-grid gap-3">
          <button class="btn btn-primary btn-lg d-flex align-items-center justify-content-center gap-2">
            <i class="fas fa-user-plus fa-lg"></i> Crear un usuario
          </button>
  
          <button class="btn btn-secondary btn-lg d-flex align-items-center justify-content-center gap-2">
            <i class="fas fa-users-cog fa-lg"></i> Listar, Editar o Eliminar usuario
          </button>
        </div>
      </div>
    </div>
  </template>
  
<script>
export default  
{
    name : "MenuPart"
}
</script>

