import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User, TaskEntry, Notification } from '@app/_models';
import { AuthenticationService, TaskService, UserService } from '@app/_services';

@Component({
	templateUrl: 'home.component.html',
	styleUrls: ['home.component.css']
})
export class HomeComponent {

	loading = false;
	currentUser: User;
	taskList: TaskEntry[];
	filters: FormGroup
	taskForm: FormGroup
	message: string = ''
	employeeList: User[]
	notificationList: Notification[] = []
	canEdit: boolean = false
	taskCount: number = 0
	lastPage: number = 0

	constructor(private taskService: TaskService, private userService: UserService, private authenticationService: AuthenticationService, private activatedRoute: ActivatedRoute, private router: Router) {

		this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
		this.message = this.currentUser.role != 'Manager' ? 'Your Tasks' : `Your team's task list`
		this.filters = new FormGroup({
			pagesize: new FormControl(this.activatedRoute.snapshot.queryParams.pagesize ? +(this.activatedRoute.snapshot.queryParams.pagesize) : 10),
			pagenumber: new FormControl(this.activatedRoute.snapshot.queryParams.pageindex ? +(this.activatedRoute.snapshot.queryParams.pageindex) : 0),
			userId: new FormControl(this.currentUser.id)
		})

		this.onNewTask()
	}

	ngOnInit() {

		this.currentUser.role == 'Manager' ? this.getEmployees() : this.sendQuery()

		var modal = document.getElementById("modal");
		var btn = document.getElementById("btn");
		var saveBtn = document.getElementById("saveBtn");
		var close = document.getElementById("close");

		btn.onclick = function () {
			modal.style.display = "block";
		}

		saveBtn.onclick = function () {
			modal.style.display = "none";
		}

		close.onclick = function () {
			modal.style.display = "none";
		}

		window.onclick = function (event) {
			if (event.target == modal) {
				modal.style.display = "none";
			}
		}
	}

	private getEmployees() {
		this.loading = true
		this.userService.getEmployees().subscribe(res => {
			this.employeeList = res
			this.sendQuery()
		}, () => {
			this.loading = false
			this.message = 'Unable to retrieve employee list!'
		})
	}

	private getNotifications() {
		this.taskService.getNotifications().subscribe(res => {
			this.notificationList = res
			this.loading = false
			this.message = `Your team's task list`
		}, () => {
			this.loading = false
			this.message = 'Unable to retrieve notification list!'
		})
	}

	private sendQuery() {
		this.loading = true
		if (this.currentUser.role == 'Manager') this.getNotifications()
		this.taskService.getTasks(this.filters.value).subscribe(res => {
			res.tasks.forEach(x => {
				if (this.currentUser.role == 'Manager' && this.employeeList) {
					let employee = this.employeeList.find(y => y.id == parseInt(x.user))
					x.user = `${employee.firstName} ${employee.lastName}`
				}
				if (new Date(x.performedDate).getFullYear() == 1)
					x.performedDate = null
			})

			if (res && res.count > 0) {
				this.taskList = res.tasks
				this.taskCount = res.count
				this.lastPage = Math.ceil(this.taskCount / 10) - 1
				this.message = this.currentUser.role != 'Manager' ? 'Your Tasks' : `Your team's task list`
			} else
				this.message = 'There are no tasks available.'

			this.loading = false
		}, () => {
			this.loading = false
			this.message = 'Failed to retrieve tasks!'

		})
		this.router.navigate([], {
			queryParams: {
				pageindex: this.filters.controls.pagenumber.value,
				pagesize: this.filters.controls.pagesize.value
			},
			replaceUrl: true,
		})
	}

	onSave() {
		this.loading = true
		this.taskService.saveTask(this.taskForm.value).subscribe(res => {
			this.loading = false
			this.message = this.taskForm.controls.taskId.value == 0 ? 'Task created successfully!' : 'Task was edited successfully!'
			this.taskForm.reset()
			this.sendQuery()
		}, () => {
			this.loading = false
			this.message = this.taskForm.controls.taskId.value == 0 ? 'Failed to create task!' : 'Failed to edit task!'
		})
	}

	onNewTask() {
		if (this.taskForm) this.taskForm.reset()
		this.canEdit = true
		this.taskForm = new FormGroup({
			taskId: new FormControl(0),
			applicationUserId: new FormControl(this.currentUser.id, Validators.required),
			summary: new FormControl('', [Validators.required, Validators.maxLength(2500)]),
			createUser: new FormControl(this.currentUser.id),
			completed: new FormControl(false)
		})
	}

	onEdit(task: TaskEntry) {
		this.canEdit = task.performedDate ? false : true
		this.taskForm.setValue({
			taskId: task.taskId,
			applicationUserId: this.currentUser.id,
			summary: task.summary,
			createUser: this.currentUser.id,
			completed: task.performedDate ? true : false
		})
		var modal = document.getElementById("modal");
		modal.style.display = 'block'
	}

	onDelete(id: number) {
		this.loading = true
		this.taskService.DeleteTask(id).subscribe(() => {
			this.loading = false
			this.message = 'Task deleted successfully!'
			this.sendQuery()
		}, () => {
			this.loading = false
			this.message = 'Failed to delete task!'
			Math.ceil(22 / 10)
		})
	}

	onReadNotification(id: number) {
		this.loading = true
		this.taskService.UpdateRead(id).subscribe(() => {
			this.getNotifications()
		}, () => {
			this.loading = false
		})
	}

	public goToPage(page) {

		if (page >= this.lastPage) page = this.lastPage
		if (page <= 0) page = 0

		this.filters.controls.pagenumber.setValue(page)
		this.sendQuery()
	}
}