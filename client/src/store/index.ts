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
    selectedProjectName: "",
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
    ADD_PROJECT(state, stdToDO) {
      state.toDos.push(stdToDO);
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
    async addProject({ commit }, projectName) {
      let stdToDO = new ToDo(projectName, "", 1, "", "", false);
      let resp = await rest.url("DoIt/new").post(stdToDO).json();
      commit("ADD_PROJECT", resp);
      this.dispatch("getProjectNames");
    },
    async deleteProject({commit},projectName) {
      await rest.url("DoIt/deleteProject").query({ projectName }).delete();
      this.dispatch("getToDoItems");
      this.dispatch("getProjectNames");
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
