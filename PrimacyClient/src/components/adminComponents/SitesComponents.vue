<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useToggleStore } from 'src/stores/toggleEditMode';

const sites = ref([]);
const divisions = ref([]);
const editedSite = ref({ id: null, name: ''});
const isEditing = ref(false);

//Columns
const columns = [
    {
        name: 'id',
        label: 'Id',
        align: 'left',
        field: row => row.id,
        sortable: true
    },
    {
        name: 'name',
        label: 'Name',
        align: 'left',
        field: row => row.name,
        sortable: true
    },
    {
        name: 'divisionName',
        label: 'Division Name',
        align: 'left',
        field: row => row.divisionName,
        sortable: true
    },
    { name: 'actions' }
]


const store = useToggleStore();


const fetchSites = async () => {
    try {
        const response = await axios.get("http://localhost:5241/api/sites");
        sites.value = response.data;
    } catch (error) {
        console.error("Error fetching data: ", error);
    }
};

const fetchDivisions = async () => {
    try {
        const response = await axios.get("http://localhost:5241/api/divisions");
        divisions.value = response.data;
    } catch (error) {
        console.error("Error fetching data: ", error);
    }
};

const startEditing = (site) => {
    editedSite.value = { ...site };
    isEditing.value = true;
};

const saveEdit = async () => {
  try {
    await axios.put(`http://localhost:5241/api/divisions/${editedSite.value.id}`,
      { 
        id: editedSite.value.id,
        name: editedSite.value.name,
        divisionId: editedSite.value.divisionId
      },
      { headers: { "Content-Type": "application/json" } });

    alert("Site updated successufuly");
    fetchSites();
    editedSite.value = { id: null, name: '', divisionId: null};
    isEditing.value = false;
  } catch (error) {
    console.error("Error updating...", error);
    alert("failed to update Site!");   
  }
};

const deleteSite = async (id) => {
  if (!confirm("Are you sure you want to delete this Site?")) return;

  try {
    await axios.delete(`http://localhost:5241/api/sites/${id}`,
    { headers: { "Content-Type": "application/json" } }
    );
    alert("Site deleted successfully!");
    fetchSites();
  } catch (error) {
    console.error("Error deleting site:", error);
    alert("Failed to delete site.");
  }
};

onMounted(() => {
    fetchSites();
    fetchDivisions();
});

</script>

<template>
    <div class="q-pa-md" v-if="sites.length > 0">
      <q-table  flat bordered 
                title="Sites" 
                :rows="sites" 
                :columns="columns" 
                row-key="id">
  
        <template v-slot:body-cell-actions="props">
          <q-td v-if="store.isVisible" :props="props">
            <q-btn label="Edit" color="primary" icon="edit" dense flat @click="startEditing(props.row)" />
            <q-btn color="negative" icon="delete" dense flat @click="deleteSite(props.row.id)" />
          </q-td>
        </template>
      </q-table>
  
      <q-dialog v-model="isEditing">
        <q-card>  
          <q-separator />
  
          <q-card-section>
            <q-input v-model="editedSite.name" label="Site Name" outlined dense />
            <q-select v-model="editedSite.divisionId" 
                :options="divisions.map(e => ({ label: e.name, value: e.id }))" 
                label="Select Division" 
                outlined dense emit-value map-options />
          </q-card-section>
  
          <q-separator />
  
          <q-card-actions align="right">
            <q-btn flat label="Cancel" color="red" v-close-popup />
            <q-btn flat label="Save" color="green" @click="saveEdit" />
          </q-card-actions>
        </q-card>
      </q-dialog>
    </div>
    <p v-else class="text-gray-500">No data found...</p>
  </template>