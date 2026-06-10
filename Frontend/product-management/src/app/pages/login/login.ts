import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/authservice';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterLink],
  templateUrl:'./login.html',
  styleUrl:'./login.css',
})
export class Login {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {

    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {

    this.auth.login(this.form.value).subscribe({
      next: (res: any) => {

        console.log("LOGIN RESPONSE:", res);

      
        localStorage.setItem('token', res.token);
localStorage.setItem('user', JSON.stringify(res));
        this.router.navigate(['/home']);
      },

      error: (err) => {
        console.log("LOGIN ERROR:", err);
      }
    });
  }
}