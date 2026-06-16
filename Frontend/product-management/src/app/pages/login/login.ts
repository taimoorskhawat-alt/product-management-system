import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/authservice';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterLink],
  templateUrl:'./login.html',
  styleUrl:'./login.css',
})
export class Login {

  form: FormGroup;
isLoading = false;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private toastr:ToastrService
  ) {

    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

 login() {

  if (this.form.invalid) {
    this.toastr.error('Please fill all fields', 'Validation Error');
    return;
  }
 this.isLoading = true;
  this.auth.login(this.form.value).subscribe({
    next: (res: any) => {

      this.isLoading = false;
      localStorage.setItem('token', res.token);
      localStorage.setItem('user', JSON.stringify(res));

      this.toastr.success('Login successful', 'Welcome');

      this.router.navigate(['/home']);
    },

    error: (err) => {
 this.isLoading = false;
      console.log( err);

      this.toastr.error(
        err.error || 'Invalid email or password',
        'Login Failed'
      );
    }
  });
}
}