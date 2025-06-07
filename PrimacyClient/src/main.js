import './assets/main.css';
import { createApp } from 'vue';
import { Quasar } from 'quasar';
import { createPinia } from 'pinia';

import App from './App.vue';
import router from './router/router';

import 'quasar/dist/quasar.css';
import '@quasar/extras/material-icons/material-icons.css';
import 'quasar/src/css/index.sass';

const app = createApp(App);

app.use(Quasar, {
    plugins: {},
});

app.use(createPinia());
app.use(router);

app.mount('#app');
