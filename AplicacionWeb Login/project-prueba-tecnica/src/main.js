import { createApp } from 'vue';
import App from './App.vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue-next';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css';
import axios from 'axios';
import router from './router'; // Importa el router

const app = createApp(App);
app.use(BootstrapVue);
app.use(IconsPlugin);
app.config.globalProperties.$axios = axios;
app.use(router); // Usa Vue Router
app.mount('#app');
