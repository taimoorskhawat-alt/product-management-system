import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, } from '@angular/router';
import { AuthService } from '../../services/authservice';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink,CommonModule,ReactiveFormsModule,],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
    constructor(
    private auth: AuthService,
    private router: Router
  ) {}
  isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  dropdownOpen = false;

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }

  closeDropdown() {
    this.dropdownOpen = false;
  }

  logout() {
    localStorage.removeItem('token');
    this.closeDropdown();
    window.location.href = '/login'; 
  }
getUserName(): string {
  const user = localStorage.getItem('user');
  if (!user) return '';
  return JSON.parse(user).userName;
}
}
