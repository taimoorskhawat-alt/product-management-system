import { Component } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/authservice';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [CommonModule,ReactiveFormsModule,RouterLink,FormsModule],
  standalone: true,
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
    form;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {

    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  ngOnInit() {
  console.log("REGISTER COMPONENT LOADED");
}

 register() {

  console.log("REGISTER CALLED");

  this.auth.register(this.form.value as any)
    .subscribe({

      next: (res) => {
        console.log("SUCCESS RESPONSE:", res);
        alert('Registration Successful');
        this.router.navigate(['/login']);
      },

      error: (err) => {
        console.log("ERROR RESPONSE:", err);
        alert("Registration failed");
      }

    });

}
 
}
