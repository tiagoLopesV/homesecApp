import { defineRouter } from '#q-app/wrappers'
import { createRouter, createMemoryHistory, createWebHistory, createWebHashHistory } from 'vue-router'
import LandingPage from '../views/LandingPage.vue';
import EntityView from 'src/views/adminPages/EntityView.vue';
import DivisionView from 'src/views/adminPages/DivisionView.vue';
import SiteView from 'src/views/adminPages/SiteView.vue';

const routes = [
  { path: '/', component: LandingPage },
  {
    path: '/divisions',
    name: 'Divisions',
    component: DivisionView
  },
  {
    path: '/entities',
    name: 'Entities',
    component: EntityView
  },
  {
    path: '/sites',
    name: "Sites",
    component: SiteView
  },
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
];

export default defineRouter(function (/* { store, ssrContext } */) {
    const createHistory = process.env.SERVER
      ? createMemoryHistory
      : (process.env.VUE_ROUTER_MODE === 'history' ? createWebHistory : createWebHashHistory)
  
    const Router = createRouter({
      scrollBehavior: () => ({ left: 0, top: 0 }),
      routes,
  
      // Leave this as is and make changes in quasar.conf.js instead!
      // quasar.conf.js -> build -> vueRouterMode
      // quasar.conf.js -> build -> publicPath
      history: createHistory(process.env.VUE_ROUTER_BASE)
    })
  
    return Router
  });
  