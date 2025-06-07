import { defineStore } from "pinia";
import { ref } from "vue";

export const useToggleStore = defineStore("toggle", () => {
  const isVisible = ref(false);

  function toggleVisibility() {
    isVisible.value = !isVisible.value;
    console.log("Edit mode toggled:", isVisible.value);
  }
  
  return { isVisible, toggleVisibility };
});
