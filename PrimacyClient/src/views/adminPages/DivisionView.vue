<script setup>
import Entities from 'src/components/adminComponents/DivisionsComponents.vue';
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useToggleStore } from 'src/stores/toggleEditMode';

const componentKey = ref(0); // To force re-render
const fixed = ref(false);
const newDivision = ref({ name: '', entityId: null });
const loading = ref(false);
const toggleEditMode = useToggleStore();

const entities = ref([]); // Store available Entities

// Function to re-render
const forceRerender = () => {
  componentKey.value += 1;
};

// Fetch available entities for selection
// wtf
const fetchEntities = async () => {
  try {
    const response = await axios.get("http://localhost:5241/api/entities");
    entities.value = response.data;
  } catch (error) {
    console.error("Error fetching data: ", error);
  }
};


const addDivision = async () => {
  if (!newDivision.value.name || !newDivision.value.entityId) {
    alert("Please enter a name and select an Entity.");
    return;
  }

  loading.value = true;

  try { 
    const payload = {
      name: newDivision.value.name,
      entityId: newDivision.value.entityId, // Ensure the Entity ID is correct
    };

    const response = await axios.post("http://localhost:5241/api/divisions", payload);

    if (response.status === 201) {
      alert("Division added successfully!");
      fixed.value = false;
      forceRerender();
    }
  } catch (error) {
    console.error("Error adding division:", error);

if (error.response) {
  // If the error has a response, we can check it here
  alert(`Failed to add division: ${error.response.data || error.message}`);
} else {
  alert("Failed to add division.");
}
  } finally {
    loading.value = false;
  }
};

onMounted(fetchEntities);

</script>

<template>
  <div>
    <h6 class="text-2xl font-bold mb-4">Divisions List</h6>
    <Entities :key="componentKey" />
  </div>
  
  <div class="q-pa-md q-gutter-sm">
    <q-btn label="Add New Division" color="primary" @click="fixed = true" />
    <q-btn label="Edit Divisions" color="primary" @click="toggleEditMode.toggleVisibility" />

    <q-dialog v-model="fixed">
      <q-card>
        <q-card-section>
          <div class="text-h6">Add New Division</div>
        </q-card-section>

        <q-separator />

        <q-card-section>
          <q-input v-model="newDivision.name" label="Division Name" outlined dense autofocus />

          <!-- Entity Selection Dropdown -->
          <q-select 
            v-model="newDivision.entityId" 
            :options="entities.map(e => ({ label: e.name, value: e.id }))" 
            label="Select Entity" 
            outlined dense emit-value map-options />
        </q-card-section>

        <q-separator />

        <q-card-actions align="right">
          <q-btn flat label="Cancel" color="red" v-close-popup />
          <q-btn flat label="Save" color="green" :loading="loading" @click="addDivision" />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>
