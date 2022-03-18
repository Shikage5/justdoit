<template>
    <v-app>
      <Navigation />
      <v-main class="red lighten-5">
        <router-view></router-view>
      </v-main>
    </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import Navigation from "@/components/Navigation.vue";
import ToDoList from "@/components/ToDoList.vue";

export default Vue.extend({
  name: "App",
  components: {
    Navigation,
    ToDoList
  },
  data: () => ({}),
});
</script>
