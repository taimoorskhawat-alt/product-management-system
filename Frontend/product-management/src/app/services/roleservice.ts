import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Roleservice {
    private api = "https://localhost:7070/api/role";

  constructor(private http: HttpClient) {}

  getRoles() {
    return this.http.get<any[]>(this.api);
  }

  addRole(role: any) {
    return this.http.post(this.api, role);
  }

  deleteRole(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
  updateRole(id: number, role: any) {
  return this.http.put(
    `https://localhost:7070/api/role/${id}`,
    role
  );
}
}
