<script setup>
import Entities from 'src/components/adminComponents/SitesComponents.vue';
import { onMounted, ref } from 'vue';
import axios from 'axios';
import { useToggleStore } from 'src/stores/toggleEditMode';

const componentKey = ref(0); // This is to force a re-render after adding a new line to the DB.

const fixed = ref(false)
const newSite = ref({ name: '' });
const loading = ref(false);
const toggleEditMode = useToggleStore();
const divisions = ref([]);

//Fuction to re-render
const forceRerender = () => {
  componentKey.value += 1;
};

const fetchDivisions = async () => {
  try {
    const response = await axios.get("http://localhost:5241/api/divisions");
    divisions.value = response.data;
  } catch (error) {
    console.error("Error fetching divisions:", error);
  }
};

const addSite = async () => {
  if (!newSite.value.name) {
    alert("Please enter a name and select a Division.");
    return;
  }

  loading.value = true;

  try {
    const payload = {
      name: newSite.value.name,
      divisionId: newSite.value.divisionId, // Ensure the Entity ID is correct
    };

    const response = await axios.post("http://localhost:5241/api/sites", payload);

    if (response.status === 201) {
      alert("Site added successfully!");
      fixed.value = false;
      forceRerender();
    }
  } catch (error) {
    console.error("Error adding site:", error);

    if (error.response) {
      // If the error has a response, we can check it here
      alert(`Failed to add site: ${error.response.data || error.message}`);
    } else {
      alert("Failed to add site.");
    }
  } finally {
    loading.value = false;
  }

};

onMounted(fetchDivisions);

</script>


<template>
  <div>
    <h6 class="text-2xl font-bold mb-4">Site List</h6>
    <Entities :key="componentKey" />
  </div>
  
  <div class="q-pa-md q-gutter-sm">
    <q-btn label="Add New Site" color="primary" @click="fixed = true" />
    <q-btn label="Edit Site" color="primary" @click="toggleEditMode.toggleVisibility" />

    <q-dialog v-model="fixed">
      <q-card>
        <q-card-section>
          <div class="text-h6">Add New Site</div>
        </q-card-section>

        <q-separator />

        <q-card-section>
          <q-input v-model="newSite.name" label="Site Name" outlined dense autofocus />

          <!-- Entity Selection Dropdown -->
          <q-select 
            v-model="newSite.divisionId" 
            :options="divisions.map(e => ({ label: e.name, value: e.id }))" 
            label="Select Division" 
            outlined dense emit-value map-options />
        </q-card-section>

        <q-separator />

        <q-card-actions align="right">
          <q-btn flat label="Cancel" color="red" v-close-popup />
          <q-btn flat label="Save" color="green" :loading="loading" @click="addSite" />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>
