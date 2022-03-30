<template>
  <nav>
    <v-toolbar app dense outlined dark color="primary">
      <v-spacer></v-spacer>
      <v-toolbar-title>
        <span class="font-weight-light">JustDo</span>
        <span class="font-weight-black text-h6">IT</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>

    <v-navigation-drawer app mini-variant permanent expand-on-hover>
      <v-list-item>
        <v-list-item-icon>
          <v-img width="30" src="@/assets/logo.png"></v-img>
        </v-list-item-icon>
        <v-list-item-title>
          <span class="font-weight-light">JustDo</span>
          <span class="font-weight-bold text-h6">IT</span>
        </v-list-item-title>
      </v-list-item>

      <v-divider></v-divider>

      <v-dialog
        v-model="dialog"
        transition="slide-y-transition"
        max-width="600"
        overlay-opacity="0.25"
      >
        <v-card class="pb-5 px-5" max-width="600">
          <v-card-title>Add Project</v-card-title>

          <v-text-field
            class="mb-7"
            autofocus
            prepend-icon="mdi-pencil"
            label="Name"
            v-model="project.name"
          ></v-text-field>

          <v-btn absolute right color="accent" bottom @click="$store.commit('ADD_PROJECT',project.name);dialog=false">
            <v-icon left>mdi-plus</v-icon>Add
          </v-btn>
        </v-card>
      </v-dialog>

      <v-list dense nav>
        <v-list-item link @click="dialog = true" class="accent">
          <v-list-item-icon>
            <v-icon dark>mdi-plus</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title class="white--text"
              >Add Project</v-list-item-title
            >
          </v-list-item-content>
        </v-list-item>

        <v-list-item v-for="(item,i) in $store.state.projects" :key="i" link @click="$store.commit('SELECT_PROJECT',item.name)">
          <v-list-item-icon>
            <v-icon>mdi-application-braces-outline</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.name }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </nav>
</template>

<script>
import { Vue, Component, Prop, Watch } from "vue-property-decorator";

@Component
export default class Navigation extends Vue {

  dialog = false;
  project = {name:''}
  reset() {
    this.newProjectName = "";
  }
}
</script>
