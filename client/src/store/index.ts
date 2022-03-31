import Vue from "vue";
import Vuex from "vuex";
import { List } from "linq-collections";
import ToDo from "@/classes/ToDo";
import rest from "../rest";
import { parseJSON } from "date-fns";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    projectNames: <string[]>[],
    toDos: <ToDo[]>[],
    selectedProjectName: "default",
    sortByPrio: false,
  },
  getters: {
    getProjectName(state) {
      return state.selectedProjectName;
    },
  },
  mutations: {
    SET_PROJECTNAMES(state, projectNames: string[]) {
      state.projectNames = projectNames;
    },
    SELECT_PROJECT(state, projectName: string) {
      state.selectedProjectName = projectName;
    },
    SET_TODOS(state, toDos) {
      state.toDos = toDos;
    },
    SWITCH_SORTING(state) {
      state.sortByPrio = !state.sortByPrio;
    },
  },
  actions: {
    async getToDoItems({ commit }) {
      let toDos = await rest
        .url("DoIt/get")
        .query({ projectName: this.state.selectedProjectName })
        .get()
        .json();
      console.log(toDos);
      commit("SET_TODOS", toDos);
    },
    async selectProject({ commit }, projectName) {
      commit("SELECT_PROJECT", projectName);
      this.dispatch("getToDoItems");
    },
    async getProjectNames({ commit }) {
      let projectNames = await rest.url("DoIt/getProjectNames").get().json();
      commit("SET_PROJECTNAMES", projectNames);
    },
  },
  modules: {},
});
