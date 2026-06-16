import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../models/login.model';
import { RegisterModel } from '../models/register.model';
import { jwtDecode } from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = "https://localhost:7070/api/auth";

  constructor(private http: HttpClient) {}
 register(data: RegisterModel) {
    return this.http.post(
      `${this.api}/register`,
      data
    );
  }
  login(data: LoginModel) {
    return this.http.post<any>(`${this.api}/login`, data);
  }
    logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    
  }
    isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
  getRole(): string | null {
  const token = localStorage.getItem('token');
  if (!token) return null;

  const payload = JSON.parse(atob(token.split('.')[1]));

  return payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
}
getTokenExpiry(): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;

    const payload = JSON.parse(atob(token.split('.')[1]));
    return Date.now() < payload.exp * 1000;
  }
}