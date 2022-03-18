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

      <v-list dense nav>
        <v-list-item v-for="item in items" :key="item.title" link>
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      items: [
        { title: "Project 1", icon: "mdi-code-braces-box" },
        { title: "Project 2", icon: "mdi-code-braces-box" },
        { title: "Project 3", icon: "mdi-code-braces-box" },
      ],
      right: null,
    };
  },
};
</script>
