<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useToggleStore } from 'src/stores/toggleEditMode';


const divisions = ref([]);
const entities = ref([]);
const editedDivision = ref({ id: null, name: ''});
const isEditing = ref(false);
//const entities = ref([]);
//const entityId = ref([]);

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
        name: 'entityName',
        label: 'Entity Name',
        align: 'left',
        field: row => row.entityName,
        sortable: true
    },
    { name: 'actions' }
]


const store = useToggleStore();


const fetchDivisions = async () => {
    try {
        const response = await axios.get("http://localhost:5241/api/divisions");
        divisions.value = response.data;
    } catch (error) {
        console.error("Error fetching data: ", error);
    }
};

const fetchEntities = async () => {
    try {
        const response = await axios.get("http://localhost:5241/api/entities");
        entities.value = response.data;
    } catch (error) {
        console.error("Error fetching entities: ", error);
    }
};

const startEditing = (division) => {
    editedDivision.value = { ...division };
    isEditing.value = true;
};

const saveEdit = async () => {
  try {
    await axios.put(`http://localhost:5241/api/divisions/${editedDivision.value.id}`,
      { 
        id: editedDivision.value.id,
        name: editedDivision.value.name,
        entityId: editedDivision.value.entityId
      },
      { headers: { "Content-Type": "application/json" } });

    alert("Division updated successufuly");
    fetchDivisions();
    editedDivision.value = { id: null, name: '', entityId: null};
    isEditing.value = false;
  } catch (error) {
    console.error("Error updating...", error);
    console.log(editedDivision.value.id);
    alert("failed to update Division!");   
  }
};

const deleteDivision = async (id) => {
  if (!confirm("Are you sure you want to delete this division?")) return;

  try {
    await axios.delete(`http://localhost:5241/api/divisions/${id}`,
    { headers: { "Content-Type": "application/json" } }
    );
    alert("Entity deleted successfully!");
    fetchDivisions();
  } catch (error) {
    console.error("Error deleting entity:", error);
    alert("Failed to delete entity.");
  }
};

onMounted(() => {
  fetchDivisions();
  fetchEntities();
});

</script>

<template>
    <div class="q-pa-md" v-if="divisions.length > 0">
      <q-table  flat bordered 
                title="Divisions" 
                :rows="divisions" 
                :columns="columns" 
                row-key="id">
  
        <template v-slot:body-cell-actions="props">
          <q-td v-if="store.isVisible" :props="props">
            <q-btn label="Edit" color="primary" icon="edit" dense flat @click="startEditing(props.row)" />
            <q-btn color="negative" icon="delete" dense flat @click="deleteDivision(props.row.id)" />
          </q-td>
        </template>
      </q-table>
  
      <q-dialog v-model="isEditing">
        <q-card>  
          <q-separator />
  
          <q-card-section>
            <q-input v-model="editedDivision.name" label="Division Name" outlined dense />
            <q-select v-model="editedDivision.entityId" 
                :options="entities.map(e => ({ label: e.name, value: e.id }))" 
                label="Select Entity" 
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