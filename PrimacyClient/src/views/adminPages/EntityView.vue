<script setup>
import Entities from 'src/components/adminComponents/EntitiesComponent.vue';
import { ref } from 'vue';
import axios from 'axios';
import { useToggleStore } from 'src/stores/toggleEditMode';

const componentKey = ref(0); // This is to force a re-render after adding a new line to the DB.

const fixed = ref(false)
const newEntity = ref({name: ''});
const loading = ref(false);
const toggleEditMode = useToggleStore();

//Fuction to re-render
const forceRerender = () => {
  componentKey.value += 1;
};

const editEntitiesButton = () => {
  toggleEditMode.toggleVisibility();;
  console.log("Edit Pressed!!!!");
};

const addEntity = async () => {
  if (!newEntity.value.name) {
    alert("Please enter a name.");
    return;
  }

  loading.value = true;

  try {
    await axios.post("http://localhost:5241/api/entities", newEntity.value);
    alert("Entity added successfully!");

    newEntity.value.name = "";
    fixed.value = false;
    forceRerender();
  } catch (error) {
    console.error("Error adding entity:", error);
    alert("Failed to add entity.");
  } finally {
    loading.value = false;
  }
};

</script>


<template>
  <div>
    <h6 class="text-2xl font-bold mb-4">Entities List</h6>
    <Entities :key="componentKey"/>
  </div>
  
  <div class="q-pa-md q-gutter-sm">
   
    <q-btn label="Add New Entity" color="primary" @click="fixed = true" />
    <q-btn label="Edit Entities" color="primary" @click="editEntitiesButton" />

    <q-dialog v-model="fixed">
      <q-card>
        <q-card-section>
          <div class="text-h6">Add New Entity</div>
        </q-card-section>

        <q-separator />

        <q-card-section>
          <q-input
            v-model="newEntity.name"
            label="Entity Name"
            outlined
            dense
            autofocus
          />
        </q-card-section>

        <q-separator />

        <q-card-actions align="right">
          <q-btn flat label="Cancel" color="red" v-close-popup />
          <q-btn
            flat
            label="Save"
            color="green"
            :loading="loading"
            @click="addEntity"
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>
