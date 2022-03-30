import Vue from "vue";
import Vuex from "vuex";
import { List } from "linq-collections";
import ToDo from "@/classes/ToDo";
import Project from "@/classes/Project";
import rest from "../rest"
import { parseJSON } from "date-fns";

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
    SET_TODOS(state, toDos) {
      state.projects[0].toDos = toDos
    },
  },
  actions: {
    async getToDoItems({commit}){
      let toDos= await rest.url("DoIt/get").get().json();
      console.log(toDos);
      commit('SET_TODOS', toDos)
    },
  },
  modules: {},
});
