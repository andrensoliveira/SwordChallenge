import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { TaskPaging, User } from '@app/_models';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class UserService {

	private path = `${environment.apiUrl}/users`

	constructor(private http: HttpClient) { }

	public getEmployees(): Observable<User[]> {
		return this.http.get<User[]>(`${this.path}/employees`)
	}
}