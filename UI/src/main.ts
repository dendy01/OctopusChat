import { createApp } from 'vue';
import App from './App.vue';
import router from './routes/routes';
import { createPinia } from 'pinia';
import './styles/main.css';

const pinia = createPinia();

createApp(App).use(pinia).use(router).mount('#app');
