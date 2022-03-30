import ToDo from "./ToDo";

export default class Project {
  constructor(name: string) {
    this.name = name;
    this.toDos = <ToDo[]>[];
  }

  name: string;
  toDos: ToDo[];
}
