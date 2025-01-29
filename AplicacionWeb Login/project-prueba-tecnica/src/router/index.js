import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../views/Login.vue';
import Index from '../views/Index.vue'
import FormUsuario from '../views/CrearUsuario.vue'
import CrudUsuario from '../views/Crud.vue'

const routes = [
  { path: '/' ,component: LoginPage },
  { path: '/Inicio',  component: Index },
  { path: '/CrearUsuario', component: FormUsuario },
  { path: '/GestionarUsuarios', component: CrudUsuario },
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
