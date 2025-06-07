<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useToggleStore } from 'src/stores/toggleEditMode';

// table columns
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
  { name: 'actions' }
]

// API data
const entities = ref([]);
const editedEntity = ref({ id: null, name: '' });
const isEditing = ref(false);
const store = useToggleStore();

// Store expanded rows and their divisions
const expandedEntityId = ref(null);
const divisions = ref({});

const fetchEntities = async () => {
  try {
    const response = await axios.get("http://localhost:5241/api/entities");
    entities.value = response.data;
  } catch (error) {
    console.error("Error fetching data: ", error);
  }
};

// Fetch divisions for a specific entity
const fetchDivisionsByEntity = async (entityId) => {
  try {
    const response = await axios.get(`http://localhost:5241/api/divisions/entity/${entityId}`);
    console.log(response.data); // Debugging line to confirm data is received

    // Store fetched divisions inside the object using entityId as key
    divisions.value[entityId] = response.data;
  } catch (error) {
    console.error("Error fetching divisions for entity:", error);
    divisions.value[entityId] = []; // Ensure it's an empty array on failure
  }
};



// Toggle expanded entity and load divisions if needed
const toggleEntity = async (entityId) => {
  if (expandedEntityId.value === entityId) {
    expandedEntityId.value = null; // Collapse if already open
  } else {
    if (!divisions.value[entityId]) {
      await fetchDivisionsByEntity(entityId); // Wait for divisions to load
    }
    expandedEntityId.value = entityId; // Expand only after divisions are fetched
  }
};

const startEditing = (entity) => {
  editedEntity.value = { ...entity };
  isEditing.value = true;
};

const saveEdit = async () => {
  try {
    await axios.put(`http://localhost:5241/api/entities/${editedEntity.value.id}`,
      {
        id: editedEntity.value.id,
        name: editedEntity.value.name
      },
      { headers: { "Content-Type": "application/json" } });

    alert("Entitie updated successufuly");
    fetchEntities();
    editedEntity.value = { id: null, name: '' };
    isEditing.value = false;
  } catch (error) {
    console.error("Error updating...", error);
    console.log(editedEntity.value.id);
    alert("failed to update Entity!");
  }
};

const deleteEntity = async (id) => {
  if (!confirm("Are you sure you want to delete this entity?")) return;

  try {
    await axios.delete(`http://localhost:5241/api/entities/${id}`,
      { headers: { "Content-Type": "application/json" } }
    );
    alert("Entity deleted successfully!");
    fetchEntities();
  } catch (error) {
    console.error("Error deleting entity:", error);
    alert("Failed to delete entity.");
  }
};


onMounted(fetchEntities);

</script>

<template>

  <div class="q-pa-md" v-if="entities.length > 0">
    <q-table flat bordered title="Entities" :rows="entities" :columns="columns" row-key="id">
      <!-- Entity Row -->
      <template v-slot:body="props">
        <q-tr :props="props" @click="toggleEntity(props.row.id)" class="cursor-pointer">
          <q-td key="id">{{ props.row.id }}</q-td>
          <q-td key="name">{{ props.row.name }}</q-td>

          <q-td key="actions" v-if="store.isVisible" :props="props">
            <q-btn label="Edit" color="primary" icon="edit" dense flat @click="startEditing(props.row)" />
            <q-btn color="negative" icon="delete" dense flat @click="deleteEntity(props.row.id)" />
          </q-td>

        </q-tr>


        <!-- Divisions Dropdown -->
        <q-tr v-if="expandedEntityId === props.row.id">
          <q-td colspan="3">
            <q-list bordered>
              <q-item-label header>Divisions</q-item-label>

              <!-- Check if divisions exist before looping -->
              <template v-if="divisions[props.row.id] && divisions[props.row.id].length > 0">
                <q-item v-for="division in divisions[props.row.id]" :key="division.id">
                  <q-item-section>
                    <q-item-label>{{ division.name }}</q-item-label>
                  </q-item-section>
                </q-item>
              </template>

              <!-- Show "No divisions found" if the array is empty -->
              <q-item v-else>
                <q-item-section>
                  <q-item-label class="text-grey">No divisions found</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-td>
        </q-tr>
      </template>
    </q-table>

    <!-- Edit Entity Dialog -->
    <q-dialog v-model="isEditing">
      <q-card>
        <q-card-section>
          <div class="text-h6">Edit Entity</div>
        </q-card-section>
        <q-separator />
        <q-card-section>
          <q-input v-model="editedEntity.name" label="Entity Name" outlined dense />
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

<!-- <template>
  <div class="q-pa-md" v-if="entities.length > 0">
    <q-table  flat bordered 
              title="Entities" 
              :rows="entities" 
              :columns="columns" 
              row-key="id">

      <template v-slot:body-cell-actions="props">
        <q-td v-if="store.isVisible" :props="props">
          <q-btn label="Edit" color="primary" icon="edit" dense flat @click="startEditing(props.row)" />
          <q-btn color="negative" icon="delete" dense flat @click="deleteEntity(props.row.id)" />
        </q-td>
      </template>
    </q-table>

    <q-dialog v-model="isEditing">
      <q-card>
        <q-card-section>
          <div class="text-h6">Edit Entity</div>
        </q-card-section>

        <q-separator />

        <q-card-section>
          <q-input v-model="editedEntity.name" label="Entity Name" outlined dense />
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
</template> -->

<style lang="sass">
.my-sticky-header-table
  /* height or max-height is important */
  height: 310px

  .q-table__top,
  .q-table__bottom,
  thead tr:first-child th
    /* bg color is important for th; just specify one */
    background-color: #00b4ff

  thead tr th
    position: sticky
    z-index: 1
  thead tr:first-child th
    top: 0

  /* this is when the loading indicator appears */
  &.q-table--loading thead tr:last-child th
    /* height of all previous header rows */
    top: 48px

  /* prevent scrolling behind sticky top row on focus */
  tbody
    /* height of all previous header rows */
    scroll-margin-top: 48px
</style>