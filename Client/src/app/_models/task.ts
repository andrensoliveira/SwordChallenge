export class TaskEntry {
	taskId: number
	user: string
	summary: string
	createdAt: Date
	performedDate: Date
}

export class TaskPaging {
	count: number
	tasks: TaskEntry[]
}