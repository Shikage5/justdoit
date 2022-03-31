export default class ToDo {
  constructor(
    projectName:string,
    name: string,
    priority: number,
    dueDate: string,
    dueTime: string,
    status: boolean
  ) {
    this.id = 0;
    this.projectName = projectName;
    this.name = name;
    this.priority = priority;
    this.dueDate = dueDate;
    this.dueTime = dueTime;
    this.status = status;
  }
  id: number;
  projectName : string;
  name: string;
  priority: number;
  dueDate: string;
  dueTime: string;
  status = false;
}
