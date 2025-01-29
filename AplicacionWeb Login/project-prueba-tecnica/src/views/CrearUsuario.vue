<template>
    <div class="container d-flex justify-content-center align-items-center vh-100">
      <div class="card shadow-lg p-4" style="width: 450px;">
        <div class="card-header text-center fw-bold fs-4">Registrar Usuario</div>
        <div class="card-body">
          <form v-on:submit="registrarUsuario">

            <div class="alert alert-warning" role="alert" v-if="error">
                {{error_msg}}
            </div>

            <div class="alert alert-success" role="alert" v-if="success">
             Usuario registrado con éxito
            </div>


            <!-- Cédula -->
            <div class="mb-3">
              <label for="cedula" class="form-label">Cédula</label>
              <input type="text" class="form-control" id="cedula" v-model="user.cedula" required />
            </div>
  
            <!-- Nombre -->
            <div class="mb-3">
              <label for="nombre" class="form-label">Nombre</label>
              <input type="text" class="form-control" id="nombre" v-model="user.nombre" required />
            </div>
  
            <!-- Apellido -->
            <div class="mb-3">
              <label for="apellido" class="form-label">Apellido</label>
              <input type="text" class="form-control" id="apellido" v-model="user.apellido" required />
            </div>
  
            <!-- Email -->
            <div class="mb-3">
              <label for="email" class="form-label">Correo Electrónico</label>
              <input type="email" class="form-control" id="email" v-model="user.email" required />
            </div>
  
            <!-- Contraseña -->
            <div class="mb-3">
              <label for="password" class="form-label">Contraseña</label>
              <input type="password" class="form-control" id="password" v-model="user.password" required />
            </div>
  
            <!-- Botón de Registrar -->
            <button type="submit" class="btn btn-primary w-100">Registrar</button>

            <a href="/Inicio"  class="btn btn-info w-100 mt-3">Regresar</a>
          </form>
        </div>
      </div>
    </div>
  </template>
  
  <script>

    import axios from 'axios'; 

  export default {
    name: "RegisterUser",
    data() {
      return {
        user: {
          cedula: "",
          nombre: "",
          apellido: "",
          email: "",
          password: "",
          error: false,
          success:true,
          error_msg : ""
        }
      };
    },
    methods: {
      registrarUsuario() {
        
        let jsonbody = {
        cedula: this.user.cedula,
        nombre: this.user.nombre,
        apellido: this.user.apellido,
        email: this.user.email,
        password: this.user.password
      };

      axios.post('https://localhost:44386/api/Usuarios/CrearUsuario', jsonbody)
        .then(response => {

          if(response.data.status == true)
          {
            this.success = true;
          }
          else
          {
            this.error = true;
            this.error_msg = response.data.message;
          }
       
        })
        .catch(error => {
          console.error("Error en la solicitud:", error); // Maneja errores
          this.error = true;
          this.error_msg = "Error en el inicio de sesión. Inténtalo de nuevo.";
        });
      }

    }
  };
  </script>
  
  <style scoped>
  /* Estilos adicionales */
  .card {
    border-radius: 10px;
  }
  
  .btn-primary {
    background-color: #007bff;
    border-color: #007bff;
  }
  
  .btn-primary:hover {
    background-color: #0056b3;
    border-color: #004085;
  }
  </style>
  