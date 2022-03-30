export default class ToDo {
    constructor(name: string, priority: number, dueDate: string, dueTime: string, status: boolean) {
        this.name = name
        this.priority = priority
        this.dueDate = dueDate
        this.dueTime = dueTime
        this.status = status
    }
    name: string;
    priority: number;
    dueDate: string;
    dueTime: string;
    status = false;
}
