import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  constructor(private httpClient: HttpClient) { }

  public getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(environment.apiUrl + 'users');
  }

  public updateUser(user: User): Observable<User> {
    console.log(user);
    return this.httpClient
      .post<User>(environment.apiUrl + 'users', user);
  }
}
