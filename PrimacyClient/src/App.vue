<script setup>
import { ref } from 'vue';
import { useQuasar } from 'quasar';

const $q = useQuasar();
$q.dark.set(true);
const leftDrawerOpen = ref(false);

const toggleLeftDrawer = () => {
  leftDrawerOpen.value = !leftDrawerOpen.value;
};

const toggleDarkMode = () => {
  $q.dark.toggle()
}
</script>

<template>
  <q-layout view="hHh lpR fFf">

    <!-- Header -->
    <q-header reveal elevated class="bg-primary text-white">
      <q-toolbar>
        <q-btn dense flat round icon="menu" @click="toggleLeftDrawer" />
        <q-toolbar-title>
          <q-avatar>
            <img src="https://cdn.quasar.dev/logo-v2/svg/logo-mono-white.svg">
          </q-avatar>
          <div @click="$router.push('/')" class="btn">Primacy</div>
        </q-toolbar-title>
        <q-btn dense flat round icon="brightness_4" @click="toggleDarkMode" class="q-ml-md" />
      </q-toolbar>
    </q-header>

    <!-- Left Drawer (Sidebar) -->
    <q-drawer v-model="leftDrawerOpen" side="left" overlay bordered>
      <q-list>
        <q-item clickable>
          <router-link to="entities">
            <q-item-section>Entities</q-item-section>
          </router-link>
        </q-item>
        <q-item clickable v-ripple>
          <router-link to="divisions">
            <q-item-section>Divisions</q-item-section>
          </router-link>
        </q-item>
        <q-item clickable v-ripple>
          <router-link to="sites">
            <q-item-section>Sites</q-item-section>
          </router-link>
        </q-item>
        <!--  <q-item clickable v-ripple @click="$router.push('/buildings')">
          <q-item-section>Buildings</q-item-section>
        </q-item>
        <q-item clickable v-ripple @click="$router.push('/spaces')">
          <q-item-section>Spaces</q-item-section>
        </q-item> -->
      </q-list>
    </q-drawer>

    <!-- Main Page Content -->
    <q-page-container>
      <!-- Dynamic content will be inserted here -->
      <router-view />
    </q-page-container>

  </q-layout>
</template>
