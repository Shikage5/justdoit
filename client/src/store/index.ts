
import Vue from "vue";
import Vuex from "vuex";
import { List } from "linq-collections";
import ToDo from "@/classes/ToDo";
import Project from "@/classes/Project";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    projects: [{ name: "default", toDos: [new ToDo("", 1, "", "", false)] }],
    selectedProjectName: "default",
  },
  getters: {
    getSelectedList(state): ToDo[] {
      return new List(state.projects).single(
        (p) => p.name == state.selectedProjectName
      ).toDos;
    },
    getProjectName(state) {
      return state.selectedProjectName;
    },
  },
  mutations: {
    ADD_PROJECT(state, projectName: string) {
      state.projects.push(new Project(projectName));
    },
    SELECT_PROJECT(state, projectName: string) {
      state.selectedProjectName = projectName;
    },
  },
  actions: {},
  modules: {},
});
