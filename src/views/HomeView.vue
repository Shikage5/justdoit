<template>
    <ToDoList />
</template>

<script lang="ts">
import Vue from "vue";
import ToDoList from "@/components/ToDoList.vue";

export default Vue.extend({
  name: "Home",
  components: {
    ToDoList,
  },
});
</script>
