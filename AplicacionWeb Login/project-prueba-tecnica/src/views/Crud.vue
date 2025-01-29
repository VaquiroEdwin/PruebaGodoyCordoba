<template>
    <div class="container d-flex flex-column align-items-center mt-5">

        <div class="alert alert-warning" role="alert" v-if="error">
        {{error_msg}}
      </div>

      <!-- Botón centrado -->
      <button class="btn btn-primary mb-4" @click="cargarDatos">
        Listar Usuarios
      </button>
  
      <!-- Tabla de datos -->
      <div class="table-responsive w-100">
        <table class="table table-bordered text-center">
          <thead class="table-dark">
            <tr>
              <th scope="col">Cédula</th>
              <th scope="col">Nombre Completo</th>
              <th scope="col">Email</th>
              <th scope="col">Última Fecha de Acceso</th>
              <th scope="col">Puntaje</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="usuarios.length === 0">
              <td colspan="5" class="text-muted">No hay datos disponibles</td>
            </tr>
            <tr v-for="usuario in usuarios" :key="usuario.cedula">
              <td>{{ usuario.cedula }}</td>
              <td>{{ usuario.nombreCompleto }}</td>
              <td>{{ usuario.email }}</td>
              <td>{{ usuario.ultimaFechaAcceso }}</td>
              <td>{{ usuario.puntaje }}</td>
            </tr>
          </tbody>
        
        </table>

        <a href="/Inicio" class="btn btn-primary btn-lg mt-3">Regresar</a>

      </div>
    </div>




  </template>
  
  <script>

import axios from 'axios'; 

  export default {
    name: "UsuariosTabla",
    data() {
      return {
        usuarios: [],
        error : false // Se inicializa vacío, se puede poblar con datos reales luego
      };
    },
    methods: {
      cargarDatos() {
       
        axios.get('https://localhost:44386/api/Usuarios/ListarUsuarios')
        .then(response => {
            if(response.data.status == true)
            {
                this.usuarios = response.data.data.map(usuario => 
                ({
                    cedula: usuario.cedula,
                    nombreCompleto: usuario.nombre + " " + usuario.apellido, // Combina nombre y apellido
                    email: usuario.email,
                    ultimaFechaAcceso: usuario.fechaAcceso, // Verifica si el nombre del campo es correcto
                    puntaje: usuario.puntaje
                }));
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
  