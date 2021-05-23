import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { TaskPaging, Notification } from '@app/_models';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class TaskService {

	private path = `${environment.apiUrl}/tasks`

	constructor(private http: HttpClient) { }

	public getTasks(filters): Observable<TaskPaging> {

		const params = new URLSearchParams()

		params.set('userId', `${filters.userId}`)
		params.set('pageIndex', `${filters.pagenumber}`)
		params.set('pageSize', `${filters.pagesize}`)

		return this.http.get<TaskPaging>(`${this.path}?${params}`)
	}

	public getNotifications(): Observable<Notification[]> {
		return this.http.get<Notification[]>(`${this.path}/notifications`)
	}

	public UpdateRead(id: number) {
		return this.http.put<Notification[]>(`${this.path}/notification/${id}`, id)
	}

	public saveTask(data) {
		return data.taskId == 0 ? this.http.post(`${this.path}`, data) : this.http.put(`${this.path}`, data)
	}

	public DeleteTask(id) {
		return this.http.delete(`${this.path}/${id}`)
	}
}