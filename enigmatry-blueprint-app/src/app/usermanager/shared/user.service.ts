import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  constructor(private httpClient: HttpClient) { }

  public getUsers(): Observable<User[]> {
    return this.httpClient.get<any>(environment.apiUrl + '/users').pipe<User[]>(
      map(p => p.items)
    );
  }

  public updateUser(user: User): Observable<User> {
    console.log(user);
    return this.httpClient
      .post<User>(environment.apiUrl + '/users', user);
  }

  userById(id: string): Observable<User> {
    return this.httpClient.get<User>(environment.apiUrl + '/users/' + id);
  }

  addUser(user: User): Promise<User> {
    return new Promise((resolver, reject) => {
      resolver(user);
    });
  }
}
