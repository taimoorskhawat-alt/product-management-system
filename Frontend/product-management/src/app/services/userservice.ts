import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Userservice {
    private api = "https://localhost:7070/api/user";

  constructor(private http: HttpClient) {}

  getAllUsers() {
    return this.http.get(`${this.api}/all`);
  }

  updateRole(id: number, role: string) {
    return this.http.put(`${this.api}/update-role/${id}?role=${role}`, {});
  }
}
