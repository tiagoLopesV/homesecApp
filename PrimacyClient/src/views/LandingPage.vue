<script setup>

const alertMessage = () => {
  alert('Hello from Quasar!');
};
</script>

<template>
  <q-btn color="primary" label="Click Me" @click="alertMessage" />
  <div class="container">
    <h1>Welcome</h1>
  </div>
</template>
