<template>
  <v-container>
    <v-btn class="mt-2" @click="reset" small color="accent">
      <v-icon left>mdi-refresh</v-icon>
      reset
    </v-btn>

    <v-card class="mx-auto my-10" max-width="700" tile>
      <!-- Toolbar -->
      <v-toolbar dark color="primary">
        <v-toolbar-title>Project 1</v-toolbar-title>
        <v-text-field
          v-model="searchText"
          append-icon="mdi-magnify"
          placeholder="Search..."
          class="ml-5"
          hide-details
        ></v-text-field>
        <v-spacer></v-spacer>

        <v-menu offset-y>
          <template v-slot:activator="{ attrs, on }">
            <v-btn
              small
              icon
              class="mr-3"
              color="white"
              v-bind="attrs"
              v-on="on"
            >
              <v-icon small>mdi-sort</v-icon>
            </v-btn>
          </template>

          <v-list>
            <v-list-item
              link
              @click="
                sortByDate = true;
                sortByPrio = false;
              "
            >
              <v-list-item-title>Date</v-list-item-title>
            </v-list-item>
            <v-list-item
              link
              @click="
                sortByPrio = true;
                sortByDate = false;
              "
            >
              <v-list-item-title>Priority</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <v-btn @click="dialog = !dialog" small color="accent">
          <v-icon left>mdi-plus</v-icon>
          New
        </v-btn>
      </v-toolbar>

      <!-- New Item menu -->
      <v-dialog
        v-model="dialog"
        transition="slide-y-transition"
        max-width="600"
        overlay-opacity="0.25"
      >
        <v-card class="pb-5 px-5" max-width="600">
          <v-card-title>Add new To-Do</v-card-title>

          <v-text-field
            autofocus
            prepend-icon="mdi-pencil"
            label="Name"
            v-model="newItemName"
          />

          <v-row class="pb-10">
            <v-col>
              <!-- Date Picker menu -->
              <v-menu
                ref="menu"
                v-model="menu"
                :close-on-content-click="false"
                :return-value.sync="newItemDueDate"
                transition="scale-transition"
                offset-y
                min-width="auto"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="newItemDueDate"
                    label="Due Date"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-date-picker v-model="newItemDueDate" no-title scrollable>
                  <v-spacer></v-spacer>
                  <v-btn text color="primary" @click="menu = false">
                    Cancel
                  </v-btn>
                  <v-btn
                    text
                    color="primary"
                    @click="$refs.menu.save(newItemDueDate)"
                  >
                    OK
                  </v-btn>
                </v-date-picker>
              </v-menu>
            </v-col>
            <v-col cols="4">
              <!-- Time Picker Menu -->
              <v-menu
                ref="menu2"
                v-model="menu3"
                :close-on-content-click="false"
                :nudge-right="40"
                :return-value.sync="newItemDueTime"
                transition="scale-transition"
                offset-y
                max-width="290px"
                min-width="290px"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="newItemDueTime"
                    label="Due Time"
                    prepend-icon="mdi-clock-time-four-outline"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-time-picker
                  format="24hr"
                  v-if="menu3"
                  v-model="newItemDueTime"
                  full-width
                  @click:minute="$refs.menu2.save(newItemDueTime)"
                ></v-time-picker>
              </v-menu>
            </v-col>
            <v-col>
              <!-- Priority Selector -->
              <v-select
                v-model="newItemPriority"
                :items="[1, 2, 3, 4]"
                filled
                label="Priority"
                dense
              ></v-select>
            </v-col>
          </v-row>

          <v-btn
            absolute
            right
            color="accent"
            bottom
            @click="
              formatDate();
              addItem();
              dialog = !dialog;
            "
          >
            <v-icon left>mdi-plus</v-icon>Add
          </v-btn>
        </v-card>
      </v-dialog>

      <!-- To-Do Items List -->
      <v-list-item v-for="(item, i) in toDoItems" :key="i">
        <v-checkbox v-model="item.status"></v-checkbox>
        <v-row>
          <v-col>
            <v-list-item-content>
              <v-list-item-title link @click="editItem(item)"
                >{{ item.name }}
                <v-badge dot inline :color="prioColor[item.priority]"></v-badge
              ></v-list-item-title>
              <v-list-item-subtitle v-if="item.dueDate"
                >Due Date: {{ item.dueDate }}</v-list-item-subtitle
              >
              <v-list-item-subtitle v-if="item.dueTime"
                >Due Time: {{ item.dueTime }}</v-list-item-subtitle
              >
              <v-btn
                small
                icon
                class=""
                absolute
                right
                @click="deleteItem(item)"
              >
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </v-list-item-content>
          </v-col>
          <v-col class="mt-5" cols="3"> </v-col>
        </v-row>
      </v-list-item>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import toDos from "client/src/classes/toDos";
import { parseISO } from "date-fns";
import { List } from "linq-collections";

@Component
export default class ToDoList extends Vue {
  get toDoItems() {
    let items = this.toDos;

    if (this.sortByPrio == true) {
      items = new List(items).orderBy((i) => i.priority).toArray();
    }
    if (this.sortByDate == true) {
      items = new List(items).orderBy((i) => i.dueDate).toArray();
    }
    if (this.searchText) {
      items = new List(items)
        .where((i) => i.name.indexOf(this.searchText) >= 0)
        .toArray();
    }
    return items;
  }

  //Layout variables
  menu = false;
  menu2 = false;
  menu3 = false;
  dialog = false;
  sortByPrio = false;
  sortByDate = false;
  searchText = "";
  prioColor = { 1: "red", 2: "orange", 3: "yellow", 4: "light-green" };
  //To-Do Item variables
  toDos = [new toDos("", 1, "", "", false)];
  newItemName = "";
  newItemPriority = 4;
  newItemDueDate = "";
  formatedDueDate = "";
  newItemDueTime = "";

  @Watch("toDos", { deep: true })
  onToDosChanged(): void {
    console.log(this.toDos);
    window.localStorage.setItem("tododata", JSON.stringify(this.toDos));
  }

  addItem() {
    let item = new toDos(
      this.newItemName,
      this.newItemPriority,
      this.newItemDueDate,
      this.newItemDueTime,
      false
    );
    this.toDos.push(item);
    this.resetItemVariables();
  }

  deleteItem(item: toDos) {
    console.log(item);
    let index = this.toDos.findIndex((x) => x.name == item.name);
    this.toDos.splice(index, 1);
  }

  resetItemVariables() {
    this.newItemName = "";
    this.newItemPriority = 4;
    this.newItemDueDate = "";
    this.formatedDueDate = "";
    this.newItemDueTime = "";
  }

  reset() {
    this.toDos.forEach((element) => {
      this.toDos.pop();
    });
    window.localStorage.clear;
  }

  mounted() {
    this.initToDoList();
  }

  initToDoList() {
    let toDoDataStr = window.localStorage.getItem("tododata");
    if (toDoDataStr == null) return;
    let toDoListData = JSON.parse(toDoDataStr);
    this.toDos = toDoListData;
  }

  editItem(item: toDos) {
    this.newItemName = item.name;
    this.newItemPriority = item.priority;
    this.newItemDueDate = item.dueDate;
    this.newItemDueTime = item.dueTime;
    this.dialog = true;
  }
}
</script>